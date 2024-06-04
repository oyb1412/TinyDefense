using UnityEngine;
/// <summary>
/// ���� �����
/// </summary>
public class StunDebuff : MovementDebuff {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="debuffTime">����� ���ӽð�</param>
    public StunDebuff(float debuffTime) {
        DebuffTime = debuffTime;
        Type = Define.DebuffType.Stun;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public override void ApplyDebuff(EnemyBase enemy) {
        base.ApplyDebuff(enemy);
        enemy.StateMachine.ChangeState(Define.EnemyState.Stun);
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public override void RemoveDebuff(EnemyBase enemy) {
        base.RemoveDebuff(enemy);
        enemy.StateMachine.ChangeState(Define.EnemyState.Run);
    }

    /// <summary>
    /// ������� ������ �̵��ӵ� ��� �� ��ȯ
    /// </summary>
    /// <param name="baseSpeed">���� �̵��ӵ�</param>
    /// <returns>���� �̵��ӵ�</returns>
    public override float ModifyMoveSpeed(float baseSpeed) {
        return 0;
    }
}