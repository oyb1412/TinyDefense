using UnityEngine;
/// <summary>
/// �� ���ӵ����� �����
/// </summary>
public class PoisonDebuff : DamageDebuff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="damage">����� ������</param>
    /// <param name="time">����� ���ӽð�</param>
    public PoisonDebuff(float damage, float time) {
        Type = Define.DebuffType.Poison;
        DebuffValue = damage;
        DebuffTime = time;
    }
}