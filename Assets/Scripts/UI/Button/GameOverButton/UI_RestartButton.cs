public class UI_RestartButton : UI_Button {
    public override void Init() {

    }

    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Ingame);
        Managers.Instance.StopAllCoroutines();
        //모든 데이터 초기화 필요
        seletable = false;
    }
}