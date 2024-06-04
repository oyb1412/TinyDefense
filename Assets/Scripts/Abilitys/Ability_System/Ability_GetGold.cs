[Ability(Define.AbilityType.GetGold)]
public class Ability_GetGold : IAbility {
    public Define.AbilityValue AbilityValue { get; private set; }

    public Ability_GetGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.GetGold);
    }
    public void SetAbility() {
        Managers.Game.CurrentGold += Define.ABILITY_GETGOLD;
    }
}