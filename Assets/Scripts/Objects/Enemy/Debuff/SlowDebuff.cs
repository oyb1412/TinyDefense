using UnityEngine;
/// <summary>
/// 슬로우 디버프
/// </summary>
public class SlowDebuff : MovementDebuff {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="debuffValue">디버프 밸류</param>
    /// <param name="debuffTime">디버프 지속시간</param>
    public SlowDebuff(float debuffValue, float debuffTime) {
        Type = Define.DebuffType.Slow;
        DebuffValue = debuffValue;
        DebuffTime = debuffTime;
    }

    /// <summary>
    /// 디버프를 적용한 이동속도 계산 및 반환
    /// </summary>
    /// <param name="baseSpeed">원본 이동속도</param>
    /// <returns>계산된 이동속도</returns>
    public override float ModifyMoveSpeed(float baseSpeed) {
        return baseSpeed * (1 - DebuffValue);
    }
}