
/// <summary>
/// 시스템적 어빌리티
/// 타워 생성 코스트 영구 감소
/// </summary>
[Ability(Define.AbilityType.MinusCost)]
public class Ability_MinusCost : ITowerInitAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_MinusCost()
    {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.MinusCost, Managers.Data.DefineData);
    }

    /// <summary>
    /// 어빌리티 추가 시 즉시 효과 적용이 필요한 경우 사용
    /// </summary>
    public void SetAbility() {
        UI_TowerDescription.Instance.CreateButton.SetCreateCost(
            Managers.Data.DefineData.MINUS_TOWER_CREATE_COST);
    }

    /// <summary>
    /// 어빌리티의 지속적인 적용이 필요한 경우 사용
    /// </summary>
    /// <param name="button"></param>
    public void ExecuteSystemAbility(UI_Button button) {
       
    }
}