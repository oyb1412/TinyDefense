using UnityEngine;

/// <summary>
/// 랭킹 판넬 활성화 버튼
/// </summary>
public class UI_RankingButton : UI_Button {
    //랭킹 판넬
    [SerializeField] private UI_RankingPanel rankingPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// 랭킹 판넬 활성화
    /// </summary>
    public override void Select() {
        rankingPanel.Activation();
    }
}