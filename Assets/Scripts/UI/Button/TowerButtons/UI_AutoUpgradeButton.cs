using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AutoUpgradeButton : UI_Button {
    [SerializeField]private UI_MergeButton mergeButton;

    public override void Init() {
        
    }

    public override void Select() {
        mergeButton.CheckAndMerge();
    }
}
