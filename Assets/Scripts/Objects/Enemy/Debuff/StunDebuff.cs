using UnityEngine;
/// <summary>
/// 스턴 디버프
/// </summary>
public class StunDebuff : MovementDebuff {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="debuffTime">디버프 지속시간</param>
    public StunDebuff(float debuffTime) {
        DebuffTime = debuffTime;
        Type = Define.DebuffType.Stun;
    }

    /// <summary>
    /// 디버프 적용
    /// </summary>
    public override void ApplyDebuff(EnemyBase enemy) {
        base.ApplyDebuff(enemy);
        enemy.StateMachine.ChangeState(Define.EnemyState.Stun);
    }

    /// <summary>
    /// 디버프 해제
    /// </summary>
    public override void RemoveDebuff(EnemyBase enemy) {
        base.RemoveDebuff(enemy);
        enemy.StateMachine.ChangeState(Define.EnemyState.Run);
    }

    /// <summary>
    /// 디버프를 적용한 이동속도 계산 및 반환
    /// </summary>
    /// <param name="baseSpeed">원본 이동속도</param>
    /// <returns>계산된 이동속도</returns>
    public override float ModifyMoveSpeed(float baseSpeed) {
        return 0;
    }
}