/// <summary>
/// 모든 디버프 관리 인터페이스
/// </summary>
public interface IDebuff {
    //디버프 밸류
    float DebuffValue { get; }
    //디버프 지속시간
    float DebuffTime { get; }
    //디버프 타입
    Define.DebuffType Type { get; }
    //디버프 번들 타입
    Define.DebuffBundle Bundle { get; }
    //디버프 적용
    void ApplyDebuff(EnemyBase enemy);
    //디버프 제거
    void RemoveDebuff(EnemyBase enemy);
    //디버프로 인한 이동속도 계산
    float ModifyMoveSpeed(float baseSpeed); 
    //디버프 적용 유무
    bool IsActive { get; }
}