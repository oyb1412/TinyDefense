using UnityEngine.Events;

/// <summary>
/// 시스템적 어빌리티
/// 전사타입 타워 출현확률 증가
/// </summary>
[Ability(Define.AbilityType.PlusSoilderTowerPercentage)]
public class Ability_PlusSoilderTowerPercentage : ITowerPreAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusSoilderTowerPercentage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusSoilderTowerPercentage);
    }

    /// <summary>
    /// 어빌리티 추가 시 즉시 효과 적용이 필요한 경우 사용
    /// </summary>
    public void SetAbility() {

    }

    /// <summary>
    /// 현재 스킬 보유 시, 타워 생성시 가중치 부여
    /// </summary>
    public void ExecuteSystemAbility(UI_Button button) {
        if (button is UI_CreateButton createButton) {
            createButton.AdjustWeights(Define.TowerType.Knight);
        }
    }
}