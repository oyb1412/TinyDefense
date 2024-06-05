/// <summary>
/// 게임 배속 언락 버튼(광고)
/// </summary>
public class UI_UnlockGameSpeedUpButton : UI_Button {
    private UI_GameSpeedUpButton uI_GameSpeedUp;
    public override void Init() {
        uI_GameSpeedUp = GetComponentInParent<UI_GameSpeedUpButton>();
    }

    /// <summary>
    /// 버튼 선택 시, 광고 호출
    /// </summary>
    public override void Select() {
        Managers.ADMob.ShowRewardedAd(SetButton);
    }

    /// <summary>
    /// 광고 시청 완료 시, 호출할 콜백함수
    /// </summary>
    private void SetButton() {
        uI_GameSpeedUp.SetButton();
        gameObject.SetActive(false);
    }
}