public class UI_ContinueButton : UI_Button {
    public override void Init() {
        
    }

    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Login);
        Managers.Instance.StopAllCoroutines();

        //��� ������ �ʱ�ȭ �ʿ�
        seletable = false;
    }
}