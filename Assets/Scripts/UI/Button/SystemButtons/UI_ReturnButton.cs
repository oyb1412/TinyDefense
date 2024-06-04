using UnityEngine;
public class UI_ReturnButton : UI_Button {
    [SerializeField] private GameObject settingPanel;
    public override void Init() {
    }

    public override void Select() {
        settingPanel.SetActive(false);
        if(Managers.Instance != null) 
            Managers.Game.IsPlaying = true;
    }
}