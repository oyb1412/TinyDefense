using TMPro;
using UnityEngine;

/// <summary>
/// 각 스킬 레벨업 버튼 관리 클래스
/// </summary>
public class UI_SkillButton : UI_Button
{
    //스킬 타입
    [SerializeField] private Define.SkillType skillType;
    //스킬 레벨업 비용 표기 텍스트
    [SerializeField] private TextMeshProUGUI costText;
    //스킬 설명 표기 텍스트
    [SerializeField] private TextMeshProUGUI descriptionText;

    /// <summary>
    /// 초기화
    /// 기본 텍스트 대입
    /// 버튼 활성화 여부 계산
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        Managers.Game.CurrentGoldAction += ButtonActive;
        ButtonActive(Managers.Game.CurrentGold);

        descriptionText.text = GetDescriptionText(0);
    }

    /// <summary>
    /// 인핸스 골드 부족시 버튼 비활성화
    /// </summary>
    /// <param name="gold">현재 골드</param>
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
    /// 스킬 설명 텍스트 계산
    /// </summary>
    /// <param name="level">레벨</param>
    private string GetDescriptionText(int level) {
        return string.Format(Managers.Data.DefineData.MENT_SKILL_DESCRIPTION[(int)skillType], level);
    }

    /// <summary>
    /// 버튼 선택(인핸스)
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
