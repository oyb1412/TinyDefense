using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// 모든 애너미 관리 클래스
/// </summary>
public class EnemyBase : MonoBehaviour {

    //애너미 레벨
    protected int enemyLevel;
    //애너미 능력치
    public EnemyStatus EnemyStatus { get; private set; }
    //이동 경로(부모)
    private Transform movePath;
    //애너미 애니메이터
    private Animator animator;
    //애너미 상태머신
    public EnemyStateMachine StateMachine { get; private set; }
    //데미지 틱 wfs
    private WaitForSeconds wfs;   
    //스턴 시간 wfs
    private WaitForSeconds stunWfs;
    //이동 index
    private int moveIndex;
    //디버프 코루틴
    private Coroutine debuffCoroutine;

    public Coroutine MoveCoroutine { get; private set; }
    //자식오브젝트 스케일 비동기
    private ParentScaleEventHandler parentScale;
    //애너미 디버프 관리
    public DebuffManager DebuffManager { get; private set; }

    public int EnemyHandle { get; set; }
    private void OnEnable() {
        debuffCoroutine = null;
        parentScale.ChangeScale(Define.Direction.Right, transform);
        Init();
    }

    private void OnDisable() {
        StopAllCoroutines();
        EnemyHandle = 0;
        DebuffManager.Debuffs.Clear();
        moveIndex = 0;
    }

    protected virtual void Awake() {
        movePath = GameObject.Find(Define.ENEMY_MOVE_PATH).transform;
        EnemyStatus = new EnemyStatus();
        DebuffManager = new DebuffManager();

        parentScale = GetComponent<ParentScaleEventHandler>();
        animator = GetComponent<Animator>();
        wfs = new WaitForSeconds(Define.DEBUFF_DAMAGE_DEFAULT_TICK);
        stunWfs = new WaitForSeconds(Define.ABILITY_STUN_DEFAULT_TIME);
        StateMachine = new EnemyStateMachine(this);
    }

    /// <summary>
    /// 데미지 디버프가 존재하면
    /// 틱 데미지 계산
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_DamageDebuff() {
        while(true) {
            if (DebuffManager != null && DebuffManager.Debuffs.Count > 0) {
                foreach (var item in DebuffManager.Debuffs.Where(d => d.Bundle == Define.DebuffBundle.Damage)) {
                    if(item.IsActive) {
                        EnemyStatus.SetHp(item.DebuffValue * Define.DEBUFF_DAMAGE_DEFAULT_TICK);
                    }
                }
            }
            yield return wfs;
        }
    }

    public void SetAnimation(string paremeter) {
        animator.SetTrigger(paremeter);
    }

    public void SetAnimation(string paremeterm, bool trigger) {
        animator.SetBool(paremeterm, trigger);
    }

    /// <summary>
    /// 애너미 사망 시 애니메이션 발동(콜백으로 호출)
    /// </summary>
    public void EnemyDeadEvent() {
        Managers.Resources.Destroy(gameObject);
    }

    /// <summary>
    /// 생성 및 재사용 초기화
    /// </summary>
    protected virtual void Init() {
        enemyLevel = Managers.Game.CurrentGameLevel - 1;
        transform.position = Define.DEFAULT_CREATE_POSITION;
        EnemyStatus.Init(this, enemyLevel);
        EnemyStatus.IsLive = true;

    }

    /// <summary>
    /// 이동 시작
    /// </summary>
    public void StartMove() {
        if (EnemyStatus.CurrentHp <= 0)
            return;

        if (MoveCoroutine != null) {
            StopCoroutine(MoveCoroutine);
            MoveCoroutine = null;
        }

        MoveCoroutine = StartCoroutine(Co_Move());


        if (debuffCoroutine == null)
            debuffCoroutine = StartCoroutine(Co_DamageDebuff());
    }

    /// <summary>
    /// 경로에 맞춰 자동 이동
    /// </summary>
    private IEnumerator Co_Move() {
        Vector3 targetPosition = movePath.GetChild(moveIndex).position;
        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, EnemyStatus.MoveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, movePath.GetChild(moveIndex).position);

            if (distance <= Define.PERMISSION_RANGE) {
                moveIndex++;

                if (moveIndex >= movePath.childCount)
                    moveIndex = 0;

                targetPosition = movePath.GetChild(moveIndex).position;

                Define.Direction dir;

                if (transform.position.x < targetPosition.x)
                    dir = Define.Direction.Right;
                else if (transform.position.x > targetPosition.x)
                    dir = Define.Direction.Left;
                else
                    dir = Define.Direction.None;

                parentScale.ChangeScale(dir, transform);
            }
            yield return null;
        }
    }
}