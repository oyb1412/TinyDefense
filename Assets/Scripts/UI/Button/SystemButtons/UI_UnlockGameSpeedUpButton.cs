/// <summary>
/// ���� ��� ��� ��ư(����)
/// </summary>
public class UI_UnlockGameSpeedUpButton : UI_Button {
    private UI_GameSpeedUpButton uI_GameSpeedUp;
    public override void Init() {
        uI_GameSpeedUp = GetComponentInParent<UI_GameSpeedUpButton>();
    }

    /// <summary>
    /// ��ư ���� ��, ���� ȣ��
    /// </summary>
    public override void Select() {
        Managers.ADMob.ShowRewardedAd(SetButton);
    }

    /// <summary>
    /// ���� ��û �Ϸ� ��, ȣ���� �ݹ��Լ�
    /// </summary>
    private void SetButton() {
        uI_GameSpeedUp.SetButton();
        gameObject.SetActive(false);
    }
}