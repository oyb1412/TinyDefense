using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// 모든 타워 관리
/// </summary>
public abstract class TowerBase : MonoBehaviour {
    //타워 종류
    public Define.TowerType TowerType { get; protected set; }
    //타워 장르
    public Define.TowerBundle TowerBundle { get; protected set; }
    //타워 레벨
    public int TowerLevel { get; protected set; }
    //타워 능력치
    public TowerStatus TowerStatus { get; private set; }
    //타워가 위치한 셀
    public Cell TowerCell { get; set; }
    //타워 애니메이터
    private Animator animator;
    //공격 대상 애너미
    public EnemyBase TargetEnemy { get; private set; }
    //타워 상태머신
    public TowerStateMachine StateMachine { get;private set; }
    //추가 투사체 딜레이 yield
    private WaitForSeconds addWfs;
    //타워 레벨 표기 renderer
    private SpriteRenderer runeWard;
    //적 서치 클래스
    private EnemySearchSystem enemySearchSystem;
    //공격 코루틴
    private Coroutine attackCoroutine;
    //보유중인 모든  디버프 저장
    public List<IDebuff> Debuffs { get; private set; }
    //자식오브젝트 스케일
    private ParentScaleEventHandler parentScale;
    //타워 버프 매니저
    public BuffManager BuffManager { get; private set; }

    protected TowerData towerData;

    public int TowerHandle { get; set; }
    public struct AttackData {
        public float Damage;
        public bool IsStun;
        public bool IsCritical;
        public bool IsMiss;
        public Vector3 targetPos;
    }

    private void OnEnable() {
        BuffManager = new BuffManager();
        Debuffs = new List<IDebuff>();
        Init();

        if (animator.enabled == false)
            animator.enabled = true;
    }

    private void OnDisable() {
        StopAllCoroutines();
        attackCoroutine = null;
        TowerHandle = 0;
    }

    private void Awake() {
        towerData = Managers.Data.GameData.TowersLevelDatas;
        animator = GetComponent<Animator>();
        parentScale = GetComponent<ParentScaleEventHandler>();
        runeWard = GetComponentsInChildren<SpriteRenderer>()[1];
        enemySearchSystem = GetComponentInChildren<EnemySearchSystem>();
        TowerStatus = new TowerStatus();
        StateMachine = new TowerStateMachine(this);
        addWfs = new WaitForSeconds(Define.ABILITY_PROJECTILE_DEFAULT_DELAY);
    }

    /// <summary>
    /// 생성 및 재생성시 초기화
    /// </summary>
    protected virtual void Init() {
        TowerLevel = 0;
        TowerStatus.Init(this);
        runeWard.color = Define.COLOR_TOWERLEVEL[TowerLevel];
    }

    /// <summary>
    /// 디버프 추가
    /// </summary>
    /// <param name="debuff"></param>
    public void SetDebuff(IDebuff debuff) {
        if(Debuffs.Find(x => x.Type == debuff.Type) == null) {
            Debuffs.Add(debuff);
        }
    }


    /// <summary>
    /// 공격 대상 애너미 감지시 상태 변경
    /// </summary>
    /// <param name="targetEnemy">공격할 대상</param>
    public void SetTargetEnemy(EnemyBase targetEnemy) {
        TargetEnemy = targetEnemy;
        if(attackCoroutine == null)
            attackCoroutine = StartCoroutine(Co_Attack());
    }

    /// <summary>
    /// 공격 속도에 맞춰 공격
    /// </summary>
    private IEnumerator Co_Attack() {
        while(true) {
            if (Util.IsEnemyNull(TargetEnemy) || StateMachine.GetState() == Define.TowerState.Movement) {
                yield return null;
                continue;
            }

            Define.Direction dir;

            if (transform.position.x < TargetEnemy.transform.position.x)
                dir = Define.Direction.Right;
            else if (transform.position.x > TargetEnemy.transform.position.x)
                dir = Define.Direction.Left;
            else
                dir = Define.Direction.None;

            parentScale.ChangeScale(dir, transform);

            StateMachine.ChangeState(Define.TowerState.Attack);
            yield return new WaitForSeconds(TowerStatus.AttackDelay);
        }
    }

    /// <summary>
    /// 공격 애니메이션 이벤트 콜백으로 호출
    /// </summary>
    public virtual void FireProjectile() {
        //공격 대상 적 사망 시 상태 변경
        if (Util.IsEnemyNull(TargetEnemy)) {
            TargetEnemy = enemySearchSystem.GetRandomEnemy();
            if(Util.IsEnemyNull(TargetEnemy)) {
                StateMachine.ChangeState(Define.TowerState.Idle);
                return;
            }
        }

        AttackData attackData = new AttackData {
            Damage = TowerStatus.AttackDamage
        };

        foreach(var item in Managers.Ability.GetAbilitysOfType<IAttackAbility>()) {
            item.ExecuteAtteckAbility(this, ref attackData);
        }

        //발사체 발사
        Fire(attackData);
    }

    /// <summary>
    /// 발사체 발사 or 발사 예약
    /// </summary>
    /// <param name="attackData">데이터</param>
    /// <param name="time">예약 시간</param>
    public void Fire(AttackData attackData, float time = 0f) {
        if(time == 0f) {
            ProjectileBase projectile = Managers.Resources.Instantiate(TowerStatus.Projectile).GetComponent<ProjectileBase>();
            projectile.Init(this, attackData);
        }
        else {
            StartCoroutine(Co_Fire(attackData));
        }
    }

    /// <summary>
    /// 예약된 발사체 발사
    /// </summary>
    /// <param name="attackData">데이터</param>
    /// <param name="time">예약 시간</param>
    private IEnumerator Co_Fire(AttackData attackData) {
        yield return addWfs;
        ProjectileBase projectile = Managers.Resources.Instantiate(TowerStatus.Projectile).GetComponent<ProjectileBase>();
        projectile.Init(this, attackData);
    }

    /// <summary>
    /// 애니메이션 변경
    /// </summary>
    public void SetAnimation(string paremeter) {
        animator.SetTrigger(paremeter);
    }

    public void SetAnimation(string paremeter, bool trigger) {
        animator.SetBool(paremeter, trigger);
    }



    //타워 선택 해제
    public void DeSelect() {
        enemySearchSystem.DeActivation();
    }

    //타워 선택
    public void Select() {
        MovementArrow.Instance.DrawArrow(TowerCell.transform.position, this);
        enemySearchSystem.Activation();
    }

    public void ChangeCell(Cell cell) {
        StartCoroutine(Co_Movement(cell, true));
    }

    public void ChangeTower(TowerBase tower) {
        StartCoroutine(Co_Movement(tower.TowerCell, false));
        tower.StartCoroutine(tower.Co_Movement(TowerCell, false));
    }

    public IEnumerator Co_Movement(Cell cell, bool changeCell) {
        Vector3 pos = cell.transform.position + Define.TOWER_CREATE_POSITION;
        StateMachine.ChangeState(Define.TowerState.Movement);
        if (pos.x < transform.position.x)
            parentScale.ChangeScale(Define.Direction.Left, transform);
        else if(pos.x > transform.position.x)
            parentScale.ChangeScale(Define.Direction.Right, transform);

        DeSelect();

        if (changeCell) {
            TowerCell.Tower = null;
            TowerCell = null;
        }

        cell.IsSelected = true;


        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, pos, Define.TOWER_MOVESPEED * Time.deltaTime);

            if (Vector2.Distance(transform.position, pos) < Define.PERMISSION_RANGE) {
                cell.IsSelected = false;
                TowerCell = cell;
                TowerCell.Tower = this;

                transform.position = pos;
                StateMachine.ChangeState(Define.TowerState.Idle);


                break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 타워 레벨 상승 및 색 변경
    /// 능력치 재조정
    /// </summary>
    public virtual void TowerLevelup() {
        TowerLevel++;
        runeWard.color = Define.COLOR_TOWERLEVEL[TowerLevel];
        TowerStatus.LevelUpStatus();

        var debuff = Debuffs.Find(x => x.Type == Define.DebuffType.Poison);

        if (debuff == null)
            return;

        Debuffs.Remove(debuff);
    }


    /// <summary>
    /// 재료로 사용된 타워 제거
    /// </summary>
    public void DestroyTower() {
        StateMachine.ChangeState(Define.TowerState.Idle);
        animator.Play(Define.TAG_Idle);
        TowerCell.SetTower(null);
        Managers.Tower.RemoveTower(this);
        StartCoroutine(DisableNextFrame(() => Managers.Resources.Destroy(gameObject)));
    }

    private IEnumerator DisableNextFrame(UnityAction callBack) {
        yield return null;
        callBack?.Invoke();
    }
}
