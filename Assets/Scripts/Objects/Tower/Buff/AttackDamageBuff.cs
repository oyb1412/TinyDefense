/// <summary>
/// ������ ���� ����
/// </summary>
public class AttackDamageBuff : Buff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="buffValue">���� ���</param>
    /// <param name="buffTime">���� ���ӽð�</param>
    public AttackDamageBuff(float buffValue, float buffTime) {
        BuffValue = buffValue;
        BuffTime = buffTime;
        Type = Define.BuffType.AttackDamageUp;
    }
}