using UnityEngine;
/// <summary>
/// ���̾� ���ӵ����� �����
/// </summary>
public class FireDebuff : DamageDebuff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="damage">����� ������</param>
    /// <param name="time">����� ���ӽð�</param>
    public FireDebuff(float damage, float time) {
        Type = Define.DebuffType.Fire;
        DebuffValue = damage;
        DebuffTime = time;
    }
}