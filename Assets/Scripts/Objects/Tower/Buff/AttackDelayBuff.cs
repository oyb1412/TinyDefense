/// <summary>
/// ���� ������ ���� ����
/// </summary>
public class AttackDelayBuff : Buff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="buffValue">���� ���</param>
    /// <param name="buffTime">���� ���ӽð�</param>
    public AttackDelayBuff(float buffValue, float buffTime) {
        BuffValue = buffValue;
        BuffTime = buffTime;
        Type = Define.BuffType.AttackDelayDown;
    }
}