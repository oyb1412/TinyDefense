using UnityEngine;
/// <summary>
/// �����ǳ� ��Ȱ��ȭ ��ư
/// </summary>
public class UI_ReturnButton : UI_Button {
    [SerializeField] private GameObject settingPanel;
    public override void Init() {
    }

    /// <summary>
    /// ��ư ���� ��
    /// ���� �ǳ� ��Ȱ��ȭ
    /// ���� �����
    /// </summary>
    public override void Select() {
        settingPanel.SetActive(false);
        if(Managers.Instance != null) 
            Managers.Game.IsPlaying = true;
    }
}