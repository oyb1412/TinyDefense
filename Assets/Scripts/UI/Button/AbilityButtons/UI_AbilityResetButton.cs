/// <summary>
/// 어빌리티 초기화 버튼 클래스
/// </summary>
public class UI_AbilityResetButton : UI_Button {
    private UI_SelectAbility selectAbility;
    public override void Init() {
        selectAbility = GetComponentInParent<UI_SelectAbility>();
    }

    /// <summary>
    /// 어빌리티 초기화
    /// 광고 로드
    /// </summary>
    public override void Select() {
        Managers.ADMob.ShowRewardedAd(selectAbility.ReActivation);
    }
}