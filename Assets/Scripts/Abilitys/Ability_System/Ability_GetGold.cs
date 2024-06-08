[Ability(Define.AbilityType.GetGold)]
public class Ability_GetGold : IAbility {
    public Define.AbilityValue AbilityValue { get; private set; }

    public Ability_GetGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.GetGold, Managers.Data.DefineData);
    }
    public void SetAbility() {
        Managers.Game.CurrentGold += Managers.Data.DefineData.ABILITY_GETGOLD;
    }
}