using UnityEngine;
/// <summary>
/// ���ο� �����
/// </summary>
public class SlowDebuff : MovementDebuff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="debuffValue">����� ���</param>
    /// <param name="debuffTime">����� ���ӽð�</param>
    public SlowDebuff(float debuffValue, float debuffTime) {
        Type = Define.DebuffType.Slow;
        DebuffValue = debuffValue;
        DebuffTime = debuffTime;
    }

    /// <summary>
    /// ������� ������ �̵��ӵ� ��� �� ��ȯ
    /// </summary>
    /// <param name="baseSpeed">���� �̵��ӵ�</param>
    /// <returns>���� �̵��ӵ�</returns>
    public override float ModifyMoveSpeed(float baseSpeed) {
        return baseSpeed * (1 - DebuffValue);
    }
}