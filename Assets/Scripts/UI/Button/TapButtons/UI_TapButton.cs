using UnityEngine;

/// <summary>
/// �� �� ��ư ���� Ŭ����
/// </summary>
public class UI_TapButton : UI_Button {
    //�� �� ��ư
    private UI_Button[] tapButtons;
    //�� ��ư�� ������ ������ �ǳ�
    [SerializeField] private GameObject contentPanel;

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        tapButtons = transform.parent.GetComponentsInChildren<UI_TapButton>();
        seletable = true;
    }

    /// <summary>
    /// ��ư ����
    /// �̹� ���õ��ִ� ���� ���ý� return
    /// ��� ���� ��Ȱ��ȭ ��, ������ ���� Ȱ��ȭ
    /// </summary>
    public override void Select() {
        if (contentPanel.activeInHierarchy)
            return;

        foreach (UI_TapButton button in tapButtons) {
            button.SetButtonColor(Color.white);
            button.contentPanel.SetActive(false);
        }

        SetButtonColor(Color.gray);
        contentPanel.SetActive(true);
    }

    /// <summary>
    /// Ȱ��ȭ �� ��Ȱ��ȭ ��ư �� ����
    /// </summary>
    /// <param name="color">������ ��</param>
    public void SetButtonColor(Color color) {
        button.image.color = color;
    }
}