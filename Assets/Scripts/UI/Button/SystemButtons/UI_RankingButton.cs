using UnityEngine;

/// <summary>
/// ·©Å· ÆÇ³Ú È°¼ºÈ­ ¹öÆ°
/// </summary>
public class UI_RankingButton : UI_Button {
    //·©Å· ÆÇ³Ú
    [SerializeField] private UI_RankingPanel rankingPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// ·©Å· ÆÇ³Ú È°¼ºÈ­
    /// </summary>
    public override void Select() {
        rankingPanel.Activation();
    }
}