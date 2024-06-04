public class UI_CheckNoButton : UI_Button {
    private UI_CheckPanel checkPanel;

    public void Activation(UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
    }
    public override void Init() {
        
    }

    public override void Select() {
        checkPanel.DeActivation();
    }
}