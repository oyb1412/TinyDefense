using UnityEngine;

[Ability(Define.AbilityType.GetTower)]
public class Ability_GetTower : IAbility {
    public Define.AbilityValue AbilityValue { get; private set; }
    public Ability_GetTower() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.GetTower);
    }
    public void SetAbility() {
        Managers.Instance.UI_AutoCreateButton.GetOneTower();
    }
}