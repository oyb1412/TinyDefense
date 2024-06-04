/// <summary>
/// 모든 버프 관리 인터페이스
/// </summary>
public interface IBuff {
    //버프 밸류
    float BuffValue { get; }
    //버프 지속시간
    float BuffTime { get; }
    //버프 타입
    Define.BuffType Type { get; }
    //버프 적용
    void ApplyBuff(TowerBase tower);
    //버프 제거
    void RemoveBuff(TowerBase tower);
    //버프 밸류 계산
    float ModifyValue(float baseValue); 
    //버프 적용 유무
    bool IsActive { get; }
}