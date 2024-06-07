/// <summary>
/// ·©Å· ÆÇ³Ú ºñÈ°¼ºÈ­ ¹öÆ°
/// </summary>
public class UI_RankingDeActivationButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.DeSelectUIButton;
    }

    /// <summary>
    /// ·©Å· ÆÇ³Ú ºñÈ°¼ºÈ­
    /// </summary>
    public override void Select() {
        transform.parent.gameObject.SetActive(false);
    }
}