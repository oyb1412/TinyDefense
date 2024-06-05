using TMPro;
using UnityEngine;

/// <summary>
/// �� ��ȭ ���� ��ư Ŭ����
/// </summary>
public class UI_EnhanceButton : UI_Button
{
    //������Ʈ�� Ÿ�� ���� Ÿ��
    [SerializeField] private Define.TowerBundle bundleType;
    //������Ʈ�� ��ȭ Ÿ��
    [SerializeField] private Define.EnhanceType enhanceType;
    //��ȭ ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI levelText;
    //��ȭ�� �ʿ��� �ڽ�Ʈ ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI costText;

    /// <summary>
    /// �ʱ�ȭ
    /// ��ư Ȱ��ȭ ���� �Ǵ�
    /// �� �ؽ�Ʈ�� �⺻�� ����
    /// </summary>
    public override void Init() {
        Managers.Game.CurrentGoldAction += ButtonActive;
        ButtonActive(Managers.Game.CurrentGold);

        levelText.text = GetLevelText(0);
    }

    /// <summary>
    /// ���ڽ� ��� ������ ��ư ��Ȱ��ȭ
    /// </summary>
    /// <param name="gold">���� ���</param>
    private void ButtonActive(int gold) {
        if (Managers.Enhance == null)
            return;

        if (Managers.Enhance.EnhanceData == null)
            return;

        var enhance = Managers.Enhance.GetEnhanceValue(bundleType, enhanceType);

        if (enhance == null)
            return;

        if (gold < enhance.EnhanceCost) {
            button.interactable = false;
            seletable = false;
        } else {
            button.interactable = true;
            seletable = true;
        }
    }

    /// <summary>
    /// ���ڽ� ���� + ���� �ؽ�Ʈ ����
    /// </summary>
    /// <param name="level">����</param>
    private string GetLevelText(int level) {
        return string.Format(Define.MENT_TOWER_ENHANCE_LEVEL[(int)enhanceType], level);
    }

    /// <summary>
    /// ��ư ����(���ڽ�)
    /// ���ڽ� ���� ���� �� �ؽ�Ʈ ����
    /// ��� ����
    /// </summary>
    public override void Select() {
        var enhance = Managers.Enhance.GetEnhanceValue(bundleType, enhanceType);
        Managers.Game.CurrentGold -= enhance.EnhanceCost;
        Managers.Enhance.SetEnhanceValue(bundleType, enhanceType);

        levelText.text = GetLevelText(enhance.Level);
        costText.text = string.Format(Define.MENT_GOLD, enhance.EnhanceCost);

        ButtonActive(Managers.Game.CurrentGold);
    }
}
