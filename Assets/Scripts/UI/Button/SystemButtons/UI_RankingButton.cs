using UnityEngine;
using UnityEngine.UI;

public class UI_RankingButton : UI_Button {
    [SerializeField] private UI_RankingPanel rankingPanel;
    public override void Init() {
        
    }

    public override void Select() {
        rankingPanel.Activation();
    }
}