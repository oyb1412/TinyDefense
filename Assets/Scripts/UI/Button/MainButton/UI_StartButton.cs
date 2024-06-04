public class UI_StartButton : UI_Button {
    public override void Init() {
        
    }

    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Ingame);
    }
}