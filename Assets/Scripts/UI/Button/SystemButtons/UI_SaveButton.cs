using UnityEngine;
public class UI_SaveButton : UI_Button {
    [SerializeField] private UI_CheckPanel checkPanel;
    private string ment;
    public override void Init() {
        ment = Define.MENT_BUTTON_SAVE;
    }

    public override void Select() {
        checkPanel.Activation(ment, Save);
    }

    private void Save() {

    }
}