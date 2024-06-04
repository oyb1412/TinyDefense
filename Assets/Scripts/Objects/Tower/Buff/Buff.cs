/// <summary>
/// 모든 버프 관리 클래스
/// </summary>
public abstract class Buff : IBuff {
    //버프 밸류
    public float BuffValue { get; protected set; }
    //버프 지속시간
    public float BuffTime { get; protected set; }
    //버프 타입
    public Define.BuffType Type { get; protected set; }
    //버프 활성화 여부
    public bool IsActive { get; private set; }

    /// <summary>
    /// 버프 적용
    /// </summary>
    public void ApplyBuff(TowerBase tower) {
        IsActive = true;
    }

    /// <summary>
    /// 버프 해제
    /// </summary>
    public void RemoveBuff(TowerBase tower) {
        IsActive = false;
    }

    /// <summary>
    /// 버프가 적용된 밸류 계산 및 반환
    /// </summary>
    /// <param name="baseValue">원본 밸류</param>
    /// <returns>계산된 밸류</returns>
    public float ModifyValue(float baseValue) {
        if (Type == Define.BuffType.AttackDamageUp)
            return baseValue * (BuffValue + 1f);
        else
            return baseValue - (baseValue * BuffValue);
    }
}