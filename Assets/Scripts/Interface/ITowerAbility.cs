/// <summary>
/// 타워 적용 어빌리티 관리 인터페이스
/// </summary>
public interface ITowerAbility : IAbility {
    //지속적으로 적용되는 어빌리티
    void ExecuteSystemAbility(UI_Button button);
}