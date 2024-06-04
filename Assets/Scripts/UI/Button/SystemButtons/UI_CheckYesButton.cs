using UnityEngine.Events;

public class UI_CheckYesButton : UI_Button {
    private UnityAction action;
    private UI_CheckPanel checkPanel;
    public void Activation(UnityAction action, UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
        this.action = action;
    }
    public override void Init() {
        
    }

    public override void Select() {
        action?.Invoke();
        checkPanel.DeActivation();
    }
}