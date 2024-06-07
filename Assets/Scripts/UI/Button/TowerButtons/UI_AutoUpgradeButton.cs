using UnityEngine;

/// <summary>
/// Ÿ�� �ڵ� ���׷��̵� ��ư
/// </summary>
public class UI_AutoUpgradeButton : UI_Button {
    [SerializeField]private UI_MergeButton mergeButton;

    public override void Init() {
        buttonSfxType = Define.SFXType.SelectTowerUIButton;
    }

    /// <summary>
    /// ��ư ���ý�, ��� Ÿ�� ���׷��̵� ���� ���� �Ǵ�
    /// ���� �� ���׷��̵�
    /// </summary>
    public override void Select() {
        mergeButton.CheckAndMerge();
    }
}
