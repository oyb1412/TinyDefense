/// <summary>
/// 로그인 씬으로 이동 버튼
/// </summary>
public class UI_ContinueButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// 버튼 선택시 로그인 씬으로 이동
    /// </summary>
    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Login);
        Managers.Instance.StopAllCoroutines();

        seletable = false;
    }
}