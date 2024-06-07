using UnityEngine;

/// <summary>
/// ��ŷ �ǳ� Ȱ��ȭ ��ư
/// </summary>
public class UI_RankingButton : UI_Button {
    //��ŷ �ǳ�
    [SerializeField] private UI_RankingPanel rankingPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// ��ŷ �ǳ� Ȱ��ȭ
    /// </summary>
    public override void Select() {
        rankingPanel.Activation();
    }
}