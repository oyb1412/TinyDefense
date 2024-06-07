/// <summary>
/// �α��� ������ �̵� ��ư
/// </summary>
public class UI_ContinueButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// ��ư ���ý� �α��� ������ �̵�
    /// </summary>
    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Login);
        Managers.Instance.StopAllCoroutines();

        seletable = false;
    }
}