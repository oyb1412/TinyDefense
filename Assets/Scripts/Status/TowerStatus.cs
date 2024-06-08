using UnityEngine;

/// <summary>
/// Ÿ�� �ɷ�ġ
/// </summary>
public class TowerStatus : StatusBase {
    /// <summary>
    /// Ÿ�� ���� �� ������� �ʱ�ȭ
    /// </summary>
    /// <param name="tower">��� Ÿ��</param>
    public void Init(TowerBase tower) {
        if (Managers.Tower == null)
            return;

        if (Managers.Tower.TowerData == null)
            return;

        TowerData = Managers.Tower.TowerData[(int)tower.TowerType];

        if (TowerData == null)
            return;

        Level = tower.TowerLevel;
        TowerType = tower.TowerType;
        TowerBundle = tower.TowerBundle;
        towerBase = tower;
        TowerKills = 0;

        SellReward = TowerData.SellReward;
        SetStatus();

        if(Projectile == null)
            Projectile = Resources.Load<GameObject>(Managers.Data.DefineData.PROJECTILE_PATH[(int)TowerType]);

        enhance = Managers.Enhance;
    }

    /// <summary>
    /// ���� ���� �� ��ȭ��ġ�� ����� ���� ������ ��ȯ
    /// </summary>
    public float AttackDamage {
        get {
            float finalDamage = attackDamage;
            if (enhance != null && enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDamage).Level > 0) {
                finalDamage *= (1 + enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDamage).EnhanceValue);
            }
            if (towerBase != null && towerBase.BuffManager != null) {
                finalDamage = towerBase.BuffManager.CalculateAttackDamage(finalDamage);
            }
            return finalDamage;
        }
        set {
            if (attackDamage == value)
                return;

            attackDamage = value;
        }
    }

    /// <summary>
    /// ���� ���� �� ��ȭ��ġ�� ����� ���� ������ ��ȯ
    /// </summary>
    public float AttackDelay {
        get {
            float finalSpeed = attackDelay;

            if (enhance != null && enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDelay).Level > 0) {
                finalSpeed *= (1 - enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDelay).EnhanceValue * 0.5f);
            }
            if (towerBase != null && towerBase.BuffManager != null) {
                finalSpeed = towerBase.BuffManager.CalculateAttackDelay(finalSpeed);
            }

            if(finalSpeed <= 0)
                return 0;

            return finalSpeed;
        }
        set {
            if (attackDelay == value)
                return;

            attackDelay = value;
        }
    }
    //������ �����̳�
    public TowerData.TowerLevelData TowerData { get; private set; }

    //��ȭ Ŭ����
    private EnhanceManager enhance;
    //���� ��Ÿ�
    public float AttackRange { get; private set; }
    //�Ǹ� ���
    public int SellReward { get; private set; }
    //�߻�ü ���
    public GameObject Projectile { get; private set; }
    //Ÿ�� Ÿ��
    public Define.TowerType TowerType { get; private set; }
    //Ÿ�� ����
    public Define.TowerBundle TowerBundle { get; private set; }
    //���� ������
    private float attackDelay;
    //���� ������
    private float attackDamage;
    //��� Ÿ��
    private TowerBase towerBase;
    //�� óġ ��
    public int TowerKills { get; private set; }

    /// <summary>
    /// �Ǹź�� ����
    /// </summary>
    /// <param name="value"></param>
    public void SetReward(float value = 0) {
        SellReward = TowerData.SellRewardUpValue * (Level + 2);
        SellReward = Mathf.RoundToInt(SellReward * (1 + value));
    }

    /// <summary>
    /// ������ �� �ɷ�ġ ������
    /// </summary>
    /// <param name="bundleType">���׷��̵� �� Ÿ�� �帣</param>
    public void SetStatus() {
        SetReward();
        AttackDamage = TowerData.AttackDamage;
        AttackDelay = TowerData.AttackDelay;
        AttackRange = TowerData.AttackRange;
        SellReward = TowerData.SellReward;
    }

    public void LevelUpStatus(int killCount = 0) {
        Level++;
        SetReward();
        TowerKills += killCount;
        AttackDamage = attackDamage * (TowerData.AttackDamageUpValue);
        AttackRange = TowerData.AttackRange + (TowerData.AttackRangeUpValue * (Level));
        AttackDelay = TowerData.AttackDelay * (1 - TowerData.AttackDelayDownValue * (Level));
    }


    /// <summary>
    /// �� óġ �� ų ���� �� �׼� ȣ��
    /// </summary>
    public void SetKill() {
        TowerKills++;
    }
}