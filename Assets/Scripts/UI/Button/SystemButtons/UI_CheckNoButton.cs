/// <summary>
/// '�ƴϿ�'��ư Ŭ����
/// </summary>
public class UI_CheckNoButton : UI_Button {
    private UI_CheckPanel checkPanel;

    /// <summary>
    /// Ȱ��ȭ
    /// ���ý� Ȱ��ȭ�� �ǳ� ����
    /// </summary>
    /// <param name="checkPanel"></param>
    public void Activation(UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
    }
    public override void Init() {
        
    }

    /// <summary>
    /// '�ƴϿ�' ���ý�, �����ص� �ǳ� Ȱ��ȭ
    /// </summary>
    public override void Select() {
        checkPanel.DeActivation();
    }
}