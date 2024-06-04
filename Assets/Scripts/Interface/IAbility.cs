/// <summary>
/// 모든 어빌리티 관리 인터페이스
/// </summary>
public interface IAbility {
    //어빌리티 정보
    Define.AbilityValue AbilityValue { get; }
    //어빌리티 적용
    void SetAbility();
}