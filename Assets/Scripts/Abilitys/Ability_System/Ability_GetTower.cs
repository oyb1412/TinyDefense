using UnityEngine;

[Ability(Define.AbilityType.GetTower)]
public class Ability_GetTower : IAbility {
    public Define.AbilityValue AbilityValue { get; private set; }
    public Ability_GetTower() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.GetTower, Managers.Data.DefineData);
    }
    public void SetAbility() {
        var scene = Managers.Scene.CurrentScene as GameScene;
        if(scene.UI_AutoCreateButton.transform.parent.gameObject.activeInHierarchy) {
            scene.UI_AutoCreateButton.GetOneTower();
        }
        else {
            scene.UI_AutoCreateButton.transform.parent.gameObject.SetActive(true);
            scene.UI_AutoCreateButton.GetOneTower();
            scene.UI_AutoCreateButton.transform.parent.gameObject.SetActive(false);
        }
    }
}