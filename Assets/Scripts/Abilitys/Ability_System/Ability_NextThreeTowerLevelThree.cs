
/// <summary>
/// 시스템적 어빌리티
/// 다음 생성하는 3기의 타워 레벨 증가
/// </summary>
[Ability(Define.AbilityType.NextThreeTowerLevelThree)]
public class Ability_NextThreeTowerLevelThree : ITowerPostAbility {
    public int NextTowerUpgrade { get; private set; }
    //어빌리티 정보
    public Define.AbilityValue AbilityValue {get; private set;}

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_NextThreeTowerLevelThree() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.NextThreeTowerLevelThree);
    }

    /// <summary>
    /// 스킬이 등록되면 강화 횟수 초기화
    /// </summary>
    public void SetAbility() {
        NextTowerUpgrade += 3;
    }

    /// <summary>
    /// 스킬 목록에서 이 스킬 제거
    /// </summary>
    /// <param name="abilityManager">스킬 매니저</param>
    public void DeleteAbility(AbilityManager abilityManager) {
        abilityManager.AbilityList.Remove(AbilityValue.Type);
        UI_AbilitysPanel.Instance.RemoveAbilityIcon(this);
    }

    /// <summary>
    /// 타워 업그레이드 및
    /// 강화 횟수 종료시 어빌리티 제거
    /// </summary>
    /// <param name="tower"></param>
    public void UpgradeTower(TowerBase tower) {
        if (NextTowerUpgrade <= 0)
            return;

        tower.TowerLevelup();
        NextTowerUpgrade--;

        //if (NextTowerUpgrade <= 0) {
        //    DeleteAbility(Managers.Ability);
        //}
    }

    /// <summary>
    /// 타워 강화 횟수가 충분하면 타워 강화
    /// 횟수가 모두 다하면 스킬 제거
    /// </summary>
    public void ExecuteSystemAbility(UI_Button button) {
        if(button is UI_CreateButton createButton) {
            if(createButton.TowerObj)
                UpgradeTower(createButton.TowerObj.GetComponent<TowerBase>());
        }
    }
}