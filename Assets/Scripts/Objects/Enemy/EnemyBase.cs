using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// ��� �ֳʹ� ���� Ŭ����
/// </summary>
public class EnemyBase : MonoBehaviour {

    //�ֳʹ� ����
    protected int enemyLevel;
    //�ֳʹ� �ɷ�ġ
    public EnemyStatus EnemyStatus { get; private set; }
    //�̵� ���(�θ�)
    private Transform movePath;
    //�ֳʹ� �ִϸ�����
    private Animator animator;
    //�ֳʹ� ���¸ӽ�
    public EnemyStateMachine StateMachine { get; private set; }
    //������ ƽ wfs
    private WaitForSeconds wfs;   
    //���� �ð� wfs
    private WaitForSeconds stunWfs;
    //�̵� index
    private int moveIndex;
    //����� �ڷ�ƾ
    private Coroutine debuffCoroutine;

    public Coroutine MoveCoroutine { get; private set; }
    //�ڽĿ�����Ʈ ������ �񵿱�
    private ParentScaleEventHandler parentScale;
    //�ֳʹ� ����� ����
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
    /// ������ ������� �����ϸ�
    /// ƽ ������ ���
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
    /// �ֳʹ� ��� �� �ִϸ��̼� �ߵ�(�ݹ����� ȣ��)
    /// </summary>
    public void EnemyDeadEvent() {
        Managers.Resources.Destroy(gameObject);
    }

    /// <summary>
    /// ���� �� ���� �ʱ�ȭ
    /// </summary>
    protected virtual void Init() {
        enemyLevel = Managers.Game.CurrentGameLevel - 1;
        transform.position = Define.DEFAULT_CREATE_POSITION;
        EnemyStatus.Init(this, enemyLevel);
        EnemyStatus.IsLive = true;

    }

    /// <summary>
    /// �̵� ����
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
    /// ��ο� ���� �ڵ� �̵�
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