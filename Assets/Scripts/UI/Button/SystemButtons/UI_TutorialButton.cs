using UnityEngine;
public class UI_TutorialButton : UI_Button {
    [SerializeField] private GameObject tutorialPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    public override void Select() {
        tutorialPanel.SetActive(true);
        Managers.Game.IsPlaying = false;
    }
}