/// <summary>
/// ���� ����� ��ư
/// </summary>
public class UI_RestartButton : UI_Button {
    public override void Init() {

    }

    /// <summary>
    /// ���� ������ ���̵�
    /// </summary>
    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Ingame);
        Managers.Instance.StopAllCoroutines();
        seletable = false;
    }
}