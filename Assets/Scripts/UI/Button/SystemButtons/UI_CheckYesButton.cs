using UnityEngine.Events;

/// <summary>
/// '��'��ư Ŭ����
/// </summary>
public class UI_CheckYesButton : UI_Button {
    //��ư ���ý� ������ ��������Ʈ
    private UnityAction action;
    //���ý� ��Ȱ��ȭ�� �ǳ� ����
    private UI_CheckPanel checkPanel;

    /// <summary>
    /// ��ư Ȱ��ȭ
    /// </summary>
    public void Activation(UnityAction action, UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
        this.action = action;
    }
    public override void Init() {
        
    }

    /// <summary>
    /// ��ư ����
    /// ��������Ʈ ���� �� ������ �ǳ� ��Ȱ��ȭ
    /// </summary>
    public override void Select() {
        action?.Invoke();
        checkPanel.DeActivation();
    }
}