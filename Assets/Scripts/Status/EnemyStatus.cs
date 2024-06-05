using System;
using UnityEngine;

/// <summary>
/// �ֳʹ� �ɷ�ġ
/// </summary>
public class EnemyStatus : StatusBase {
    //���� �ֳʹ�
    private EnemyBase enemyBase;
    //���� ����
    private bool isLive;

    /// <summary>
    /// �ֳʹ� �̵��ӵ��� ������� �°� ����
    /// </summary>
    public float MoveSpeed {
        get {
            float finalSpeed = moveSpeed;

            if (enemyBase != null && enemyBase.DebuffManager != null)
                finalSpeed = enemyBase.DebuffManager.CalculateMoveSpeed(finalSpeed);
            
            return finalSpeed;
        }
        set {
            moveSpeed = value;
        }
    }
    //������ �����̳�
    public EnemyData EnemyData { get; private set; }
    //�̵� �ӵ�
    private float moveSpeed;
    //���� ü��
    public float CurrentHp { get; private set; }
    //�ִ� ü��
    public float MaxHp { get; private set; }
    //���� ���
    public int Reward { get; private set; }
    //ü�� ��ȭ�� ����� ȣ��
    public Action<float> SetHpAction;

    private GameObject goldObject;

    public void Clear() {
        SetHpAction = null;
    }

    /// <summary>
    /// �ֳʹ� �ʱ�ȭ �� ������� ��ġ �ʱ�ȭ
    /// </summary>
    public void Init(EnemyBase enemyBase, int level) {
        if (Managers.Data.GameData == null)
            return;

        if (Managers.Enemy.EnemyData == null)
            return;

        EnemyData = Managers.Data.GameData.EnemysLevelDatas;

        this.enemyBase = enemyBase;
        Level = level;

        MoveSpeed = Managers.Enemy.EnemyData.MoveSpeed;
        if(level == 0) {
            MaxHp = Managers.Enemy.EnemyData.MaxHp;
        }
        else {
            MaxHp = Managers.Enemy.EnemyData.MaxHp * Mathf.Pow(1 + Managers.Enemy.EnemyData.MaxHpUpVolume, level);
            Debug.Log($"�� ü�� {MaxHp}");
        }
        CurrentHp = MaxHp;
        SetHpAction?.Invoke(CurrentHp);
        Reward = Managers.Enemy.EnemyData.Reward + level / 10;

        if (goldObject == null)
            goldObject = Resources.Load<GameObject>(Define.OBJECT_REWARD_PATH);

        //�ֳʹ̿��� ����Ǵ� ��ų�� ������ �Ǵ���,
        //�����Ѵٸ� ��ų ����
        foreach(var item in Managers.Ability.GetAbilitysOfType<IEnemyAbility>()) {
            item.ExecuteEnemyAbility(enemyBase);
        }
    }

    /// <summary>
    /// �ֳʹ� ������ ����
    /// </summary>
    public void SetReward(Ability_PlusGetGold ability) {
        Reward += Mathf.RoundToInt(Reward * ability.PlusGold);
    }

    /// <summary>
    /// �ֳʹ� �̼� ����
    /// </summary>
    /// <param name="ability"></param>
    public void SetMoveSpeed(Ability_MinusEnemyMoveSpeed ability) {
        MoveSpeed *= ability.MoveSpeedValue;
    }

    /// <summary>
    /// �ֳʹ� ���� ���� 
    /// islive = true��, ������ ���� ü�� �ʱ�ȭ
    /// islive = false��, �ֳʹ� ������ ȹ�� �� ų �� �߰�, �ֳʹ� ��� �ִϸ��̼�
    /// �ֳʹ� ��� �ִϸ��̼ǿ��� �ݹ����� Destroy�� ����
    /// </summary>
    public bool IsLive {
        get { return isLive; }
        set {
            if(value) {
                CurrentHp = MaxHp;
                if(enemyBase)
                    enemyBase.StateMachine.ChangeState(Define.EnemyState.Run);
            } else {
                Managers.Game.CurrentKillNumber++;
                Managers.Game.CurrentGold += Reward;
                UI_GoldObject go = Managers.Resources.Instantiate(goldObject).GetComponent<UI_GoldObject>();
                go.Init(enemyBase.transform.position, Reward, true);
                enemyBase.StateMachine.ChangeState(Define.EnemyState.Dead);
                Managers.Enemy.RemoveEnemy(enemyBase);
            }
            isLive = value;
        }
    }


    /// <summary>
    /// ü�� ���� �� ü���� 0 ���ϰ� �ɽ� ���
    /// </summary>
    /// <param name="value">ü�� ���淮</param>
    public void SetHp(float value, TowerBase tower = null) {
        CurrentHp -= value;
        SetHpAction?.Invoke(CurrentHp);
        if (CurrentHp <= 0 && IsLive) {
            if(tower)
                tower.TowerStatus.SetKill();

            IsLive = false;
        }
    }   
}