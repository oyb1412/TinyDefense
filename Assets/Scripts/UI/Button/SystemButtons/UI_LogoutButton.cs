using UnityEngine;
public class UI_LogoutButton : UI_Button {
    //yes,no ��ư �ǳ�
    [SerializeField] private UI_CheckPanel checkPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    public override void Select() {
        checkPanel.Activation("������ �α׾ƿ� �Ͻðڽ��ϱ�?", Logout);
    }

    private void Logout() {
        if (!Managers.Data.DeleteData("Logindata.json"))
            return;

        Managers.Auth.Logout();
        UI_Fade.Instance.ActivationFade(Define.SceneType.Main);
    }
}