using UnityEngine;
public class UI_HomeButton : UI_Button {
    [SerializeField] private UI_CheckPanel checkPanel;
    private string ment;
    public override void Init() {
        ment = Define.MENT_BUTTON_HOME;
    }

    public override void Select() {
        checkPanel.Activation(ment, GoHome);
    }

    private void GoHome() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}