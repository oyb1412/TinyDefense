using UnityEngine;
public class UI_TutorialReturnButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.DeSelectUIButton;
    }

    public override void Select() {
        Managers.Game.IsPlaying = true;
        transform.parent.gameObject.SetActive(false);
    }
}