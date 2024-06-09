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
    /// �ֳʹ� �̵��ӵ��� ����� ����
    /// </summary>
    public float MoveSpeed {
        get {
            float finalSpeed = moveSpeed;

            if (enemyBase != null && enemyBase.DebuffManager != null && enemyBase.DebuffManager.Debuffs.Count > 0)
                finalSpeed = enemyBase.DebuffManager.CalculateMoveSpeed(finalSpeed);

            if (finalSpeed <= 0)
                return 0f;

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
    //�ֳʹ� ����� ȣ���� ��� ������Ʈ
    private GameObject goldObject;

    /// <summary>
    /// �ֳʹ� ��� �� �ʱ�ȭ
    /// </summary>
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

        EnemyData = Managers.Data.GameData.EnemyDatas;

        this.enemyBase = enemyBase;
        Level = level;

        MoveSpeed = Managers.Enemy.EnemyData.MoveSpeed;

        MaxHp = Managers.Enemy.CurrentLevelEnemyHp;

        CurrentHp = MaxHp;

        SetHpAction?.Invoke(CurrentHp);

        Reward = Managers.Enemy.EnemyData.Reward + level / 5;

        if (goldObject == null)
            goldObject = Resources.Load<GameObject>(Managers.Data.DefineData.OBJECT_REWARD_PATH);

        //�ֳʹ̿��� ����Ǵ� ��ų�� ������ �Ǵ���,
        //�����Ѵٸ� ��ų ����
        foreach(var item in Managers.Ability.EnemyAbilityList) {
            item.ExecuteEnemyAbility(enemyBase);
        }

        DebugWrapper.Log($"�� ü�� {MaxHp}");
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
            isLive = value;

            if (isLive) {
                if(CurrentHp <= 0)
                    CurrentHp = MaxHp;

                if(enemyBase)
                    enemyBase.StateMachine.ChangeState(Define.EnemyState.Run);

            } else {
                Managers.Game.CurrentKillNumber++;
                Managers.Game.CurrentGold += Reward;
                UI_GoldObject go = Managers.Resources.Activation(goldObject).GetComponent<UI_GoldObject>();
                go.Init(enemyBase.transform.position, Reward, true);
                enemyBase.StateMachine.ChangeState(Define.EnemyState.Dead);
                Managers.Enemy.RemoveEnemy(enemyBase);
            }

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