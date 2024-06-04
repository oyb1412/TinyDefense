using UnityEngine;
/// <summary>
/// 적 모든 이동속도 관련 디버프 관리
/// 타워가 컴포넌트로 보유
/// </summary>
public abstract class MovementDebuff : IDebuff {
    //디버프 타입
    public Define.DebuffType Type { get; protected set; }
    //디버프 번들(이동)
    public Define.DebuffBundle Bundle => Define.DebuffBundle.Movement;
    //디버프 밸류
    public float DebuffValue { get; protected set; }
    //디버프 적용 여부
    public bool IsActive { get; private set; }
    //디버프 적용 시간
    public float DebuffTime { get; protected set; }

    /// <summary>
    /// 디버프 적용
    /// </summary>
    public virtual void ApplyDebuff(EnemyBase enemy) {
        IsActive = true;
    }

    /// <summary>
    /// 디버프 해제
    /// </summary>
    public virtual void RemoveDebuff(EnemyBase enemy) {
        IsActive = false;
    }

    /// <summary>
    /// 디버프로 인한 이동속도 계산 및 반환
    /// </summary>
    /// <param name="baseSpeed">원본 이동속도</param>
    /// <returns>계산된 이동속도</returns>
    public abstract float ModifyMoveSpeed(float baseSpeed);
}