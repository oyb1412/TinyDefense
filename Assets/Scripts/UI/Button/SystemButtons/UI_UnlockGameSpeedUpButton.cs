public class UI_UnlockGameSpeedUpButton : UI_Button {
    private UI_GameSpeedUpButton uI_GameSpeedUp;
    public override void Init() {
        uI_GameSpeedUp = GetComponentInParent<UI_GameSpeedUpButton>();
    }

    public override void Select() {
        Managers.ADMob.ShowRewardedAd(SetButton);
    }

    private void SetButton() {
        uI_GameSpeedUp.SetButton();
        gameObject.SetActive(false);
    }
}