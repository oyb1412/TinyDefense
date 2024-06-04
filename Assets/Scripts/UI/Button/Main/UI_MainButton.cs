public class UI_MainButton : UI_Button {
    public override void Init() {
        
    }

    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Login);
        seletable = false;
    }
}