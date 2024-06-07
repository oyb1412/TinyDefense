using UnityEngine;

/// <summary>
/// ���� ���� ��ư
/// </summary>
public class UI_HomeButton : UI_Button {
    //yes,no ��ư �ǳ�
    [SerializeField] private UI_CheckPanel checkPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// ��ư ���ý�
    /// yes, no ��ư �ǳ� Ȱ��ȭ
    /// </summary>
    public override void Select() {
        checkPanel.Activation(Define.MENT_BUTTON_HOME, GoHome);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void GoHome() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}