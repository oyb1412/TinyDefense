[Ability(Define.AbilityType.PlusSellGold)]
/// <summary>
/// 시스템적 어빌리티
/// 타워 판매 비용 영구 증가
/// </summary>
public class Ability_PlusSellGold : ITowerPostAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusSellGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusSellGold, Managers.Data.DefineData);
    }

    /// <summary>
    /// 존재중인 모든 타워의 판매비용 조정
    /// </summary>
    public void SetAbility() {
        var towerList = Managers.Tower.TowerList;
        if (towerList.Count == 0)
            return;

        foreach (var tower in towerList) {
            tower.TowerStatus.SetReward(Managers.Data.DefineData.ABILITY_PLUS_TOWER_REWARD);
        }
    }

    /// <summary>
    /// 새롭게 탄생하는 타워의 판매비용 조정
    /// </summary>
    /// <param name="button"></param>
    public void ExecuteSystemAbility(UI_Button button) {
        if(button is UI_CreateButton createButton) {
            createButton.TowerObj.GetComponent<TowerBase>().TowerStatus.SetReward(Managers.Data.DefineData.ABILITY_PLUS_TOWER_REWARD);
        }
    }
}