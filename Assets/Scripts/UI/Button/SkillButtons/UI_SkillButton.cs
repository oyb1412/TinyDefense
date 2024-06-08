using TMPro;
using UnityEngine;

/// <summary>
/// �� ��ų ������ ��ư ���� Ŭ����
/// </summary>
public class UI_SkillButton : UI_Button
{
    //��ų Ÿ��
    [SerializeField] private Define.SkillType skillType;
    //��ų ������ ��� ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI costText;
    //��ų ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI descriptionText;

    /// <summary>
    /// �ʱ�ȭ
    /// �⺻ �ؽ�Ʈ ����
    /// ��ư Ȱ��ȭ ���� ���
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        Managers.Game.CurrentGoldAction += ButtonActive;
        ButtonActive(Managers.Game.CurrentGold);

        descriptionText.text = GetDescriptionText(0);
    }

    /// <summary>
    /// ���ڽ� ��� ������ ��ư ��Ȱ��ȭ
    /// </summary>
    /// <param name="gold">���� ���</param>
    private void ButtonActive(int gold) {
        if (Managers.Skill == null)
            return;

        if (Managers.Skill.SkillData == null)
            return;

        var skill = Managers.Skill.GetSkillValue(skillType);

        if (skill == null)
            return;

        if (gold < skill.SkillCost) {
            button.interactable = false;
            seletable = false;
        } else {
            button.interactable = true;
            seletable = true;
        }
    }

    /// <summary>
    /// ��ų ���� �ؽ�Ʈ ���
    /// </summary>
    /// <param name="level">����</param>
    private string GetDescriptionText(int level) {
        return string.Format(Managers.Data.DefineData.MENT_SKILL_DESCRIPTION[(int)skillType], level);
    }

    /// <summary>
    /// ��ư ����(���ڽ�)
    /// </summary>
    public override void Select() {
        var skill = Managers.Skill.GetSkillValue(skillType);
        Managers.Game.CurrentGold -= skill.SkillCost;
        Managers.Skill.SetSkillValue(skillType);
        descriptionText.text = GetDescriptionText(skill.Level);
        costText.text = string.Format(Managers.Data.DefineData.MENT_GOLD, skill.SkillCost);

        ButtonActive(Managers.Game.CurrentGold);
    }
}
