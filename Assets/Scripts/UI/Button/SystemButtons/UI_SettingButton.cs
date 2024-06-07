using UnityEngine;
public class UI_SettingButton : UI_Button {
    [SerializeField] private GameObject settingPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    public override void Select() {
        settingPanel.SetActive(true);
        if(Managers.Instance != null ) 
            Managers.Game.IsPlaying = false;
    }
}