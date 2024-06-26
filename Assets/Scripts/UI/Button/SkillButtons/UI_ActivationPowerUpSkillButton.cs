/// <summary>
/// 파워업 스킬 버튼
/// </summary>
public class UI_ActivationPowerUpSkillButton : UI_ActivationSkillButton {
    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.PowerUpSkill;
        base.Init();
        Managers.Skill.SetSkillAction += ((type) => {
            if(type == Define.SkillType.PowerUp) {
                seletable = true;
                button.interactable = true;
            }
        });
    }

    /// <summary>
    /// 파워업 스킬 선택
    /// </summary>
    protected override void OnSelect() {
        base.OnSelect();

        var skill = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp);
        UseSkill(skill.SkillCoolTime);

        var towers = Managers.Tower.TowerList;
        float skillValue = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp).SkillValue;
        float skilltime = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp).SkillTime;

        //모든 타워 순회
        //모든 타워에 버프 적용
        foreach (var tower in towers) {
            tower.BuffManager.AddBuff(
                new AttackDamageBuff(skillValue, skilltime),tower);
            tower.BuffManager.AddBuff(
                new AttackDelayBuff(skillValue * .5f, skilltime),tower);
        }
    }
}