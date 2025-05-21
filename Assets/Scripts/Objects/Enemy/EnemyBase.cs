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
    private WaitForSeconds debuffTick;   
    //이동 index
    private int moveIndex;
    //자식오브젝트 스케일 비동기
    private ParentScaleEventHandler parentScale;
    //애너미 디버프 관리
    public DebuffManager DebuffManager { get; private set; }
    //트랜스폼 캐싱
    private Transform myTransform;

    /// <summary>
    /// 애너미 생성시 초기화
    /// </summary>
    private void OnEnable() {
        Init();
    }

    /// <summary>
    /// 애너미 비활성화시 초기화
    /// </summary>
    private void OnDisable() {
        DebuffManager.Debuffs.Clear();
        DebuffManager.debuffTimer = 0;
        moveIndex = 0;
        EnemyStatus.Clear();
    }

    /// <summary>
    /// 애너미 첫 생성시 초기화
    /// </summary>
    protected virtual void Awake() {
        myTransform = transform;
        movePath = GameObject.Find(Managers.Data.DefineData.ENEMY_MOVE_PATH).transform;
        EnemyStatus = new EnemyStatus();
        DebuffManager = new DebuffManager();

        parentScale = GetComponent<ParentScaleEventHandler>();
        animator = GetComponent<Animator>();
        debuffTick = new WaitForSeconds(Managers.Data.DefineData.DEBUFF_DAMAGE_DEFAULT_TICK);
        StateMachine = new EnemyStateMachine(this);
    }

    /// <summary>
    /// 애니메이션 적용(트리거)
    /// </summary>
    public void SetAnimation(string paremeter) {
        animator.SetTrigger(paremeter);
    }

    /// <summary>
    /// 애니메이션 적용(불)
    /// </summary>
    public void SetAnimation(string paremeterm, bool trigger) {
        animator.SetBool(paremeterm, trigger);
    }

    /// <summary>
    /// 애너미 사망 시 애니메이션 발동(콜백으로 호출)
    /// </summary>
    public void EnemyDeadEvent() {
        Managers.Resources.Release(gameObject);
    }

    /// <summary>
    /// 생성 및 재사용 초기화
    /// </summary>
    private void Init() {
        parentScale.ChangeScale(Define.Direction.Right, myTransform);
        enemyLevel = Managers.Game.CurrentGameLevel - 1;
        myTransform.position = Managers.Data.DefineData.DEFAULT_CREATE_POSITION;
        EnemyStatus.Init(this, enemyLevel);
        EnemyStatus.IsLive = true;
    }

    public void UpdateDebuff() {
        if (Util.IsEnemyNull(this))
            return;

        if (DebuffManager == null || DebuffManager.Debuffs.Count == 0)
            return;

        DebuffManager.debuffTimer += Time.deltaTime;

        if (DebuffManager.debuffTimer >= Managers.Data.DefineData.DEBUFF_DAMAGE_DEFAULT_TICK) {
            DebuffManager.debuffTimer = 0f;

            foreach (var item in DebuffManager.Debuffs) {
                if (item.Bundle == Define.DebuffBundle.Damage && item.IsActive) {
                    EnemyStatus.SetHp(item.DebuffValue * Managers.Data.DefineData.DEBUFF_DAMAGE_DEFAULT_TICK);
                }
            }
        }
    }

    public void UpdateMove() {
        if (Util.IsEnemyNull(this))
            return;

        if (!EnemyStatus.IsMove)
            return;

        Vector3 targetPosition = movePath.GetChild(moveIndex).position;

        myTransform.position = Vector3.MoveTowards(
            myTransform.position,
            targetPosition,
            EnemyStatus.MoveSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(myTransform.position, targetPosition);

        if (distance <= Managers.Data.DefineData.PERMISSION_RANGE) {
            moveIndex++;

            if (moveIndex >= movePath.childCount)
                moveIndex = 0;

            targetPosition = movePath.GetChild(moveIndex).position;

            Define.Direction dir;

            if (myTransform.position.x < targetPosition.x)
                dir = Define.Direction.Right;
            else if (myTransform.position.x > targetPosition.x)
                dir = Define.Direction.Left;
            else
                dir = Define.Direction.None;

            parentScale.ChangeScale(dir, myTransform);
        }
    }
   
}