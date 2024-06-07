using UnityEngine;
/// <summary>
/// ���� ��� ��� ��ư(����)
/// </summary>
public class UI_UnlockGameSpeedUpButton : UI_Button {
    [SerializeField]private UI_GameSpeedUpButton uI_GameSpeedUp;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
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