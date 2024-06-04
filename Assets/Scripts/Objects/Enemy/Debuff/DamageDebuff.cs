/// <summary>
/// 적 모든 지속데미지 디버프 관리 클래스
/// 타워가 컴포넌트로 보유
/// </summary>
public class DamageDebuff : IDebuff {
    //디버프 타입
    public Define.DebuffType Type { get; protected set; }
    //디버프 번들(지속데미지)
    public Define.DebuffBundle Bundle => Define.DebuffBundle.Damage;
    //디버프 밸류
    public float DebuffValue { get; protected set; }
    //디버프 적용 여부
    public bool IsActive { get; private set; }
    //디버프 지속시간
    public float DebuffTime { get; protected set; }

    /// <summary>
    /// 디버프 적용
    /// </summary>
    public void ApplyDebuff(EnemyBase enemy) {
        IsActive = true;
    }

    /// <summary>
    /// 디버프 해제
    /// </summary>
    public void RemoveDebuff(EnemyBase enemy) {
        IsActive = false;
    }

    /// <summary>
    /// 디버프를 통한 이동속도 감소 
    /// </summary>
    /// <param name="baseSpeed">원본 속도</param>
    public float ModifyMoveSpeed(float baseSpeed) {
        return baseSpeed;
    }
}