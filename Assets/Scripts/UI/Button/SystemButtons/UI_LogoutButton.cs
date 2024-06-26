using UnityEngine;
public class UI_LogoutButton : UI_Button {
    //yes,no 버튼 판넬
    [SerializeField] private UI_CheckPanel checkPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    public override void Select() {
        checkPanel.Activation("정말로 로그아웃 하시겠습니까?", Logout);
    }

    private void Logout() {
        if (!Managers.Data.DeleteData("Logindata.json"))
            return;

        Managers.Auth.Logout();
        UI_Fade.Instance.ActivationFade(Define.SceneType.Main);
    }
}