public class UI_RestartButton : UI_Button {
    public override void Init() {

    }

    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Ingame);
        Managers.Instance.StopAllCoroutines();
        //��� ������ �ʱ�ȭ �ʿ�
        seletable = false;
    }
}