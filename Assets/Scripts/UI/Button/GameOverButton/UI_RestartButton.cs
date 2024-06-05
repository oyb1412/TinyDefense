/// <summary>
/// 게임 재시작 버튼
/// </summary>
public class UI_RestartButton : UI_Button {
    public override void Init() {

    }

    /// <summary>
    /// 현재 씬으로 재이동
    /// </summary>
    public override void Select() {
        UI_Fade.Instance.ActivationFade(Define.SceneType.Ingame);
        Managers.Instance.StopAllCoroutines();
        seletable = false;
    }
}