/// <summary>
/// ��� ���� ���� Ŭ����
/// </summary>
public abstract class Buff : IBuff {
    //���� ���
    public float BuffValue { get; protected set; }
    //���� ���ӽð�
    public float BuffTime { get; protected set; }
    //���� Ÿ��
    public Define.BuffType Type { get; protected set; }
    //���� Ȱ��ȭ ����
    public bool IsActive { get; private set; }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ApplyBuff(TowerBase tower) {
        IsActive = true;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void RemoveBuff(TowerBase tower) {
        IsActive = false;
    }

    /// <summary>
    /// ������ ����� ��� ��� �� ��ȯ
    /// </summary>
    /// <param name="baseValue">���� ���</param>
    /// <returns>���� ���</returns>
    public float ModifyValue(float baseValue) {
        if (Type == Define.BuffType.AttackDamageUp)
            return baseValue * (BuffValue + 1f);
        else
            return baseValue - (baseValue * BuffValue);
    }
}