public class UI_AbilityResetButton : UI_Button {
    private UI_SelectAbility selectAbility;
    public override void Init() {
        selectAbility = GetComponentInParent<UI_SelectAbility>();
    }

    public override void Select() {
        Managers.ADMob.ShowRewardedAd(selectAbility.ReActivation);
    }
}