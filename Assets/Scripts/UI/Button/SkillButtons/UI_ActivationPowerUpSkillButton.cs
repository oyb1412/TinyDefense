using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class UI_ActivationPowerUpSkillButton : UI_ActivationSkillButton {
    public override void Init() {
        base.Init();
        Managers.Skill.SetSkillAction += ((type) => {
            if(type == Define.SkillType.PowerUp) {
                seletable = true;
                button.interactable = true;
            }
        });
    }

    protected override void OnSelect() {
        base.OnSelect();

        //스킬 사용 후 쿨타임 시작
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp);
        UseSkill(skill.SkillCoolTime);

        var towers = Managers.Tower.TowerList;
        float skillValue = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp).SkillValue;
        float skilltime = Managers.Skill.GetSkillValue(Define.SkillType.PowerUp).SkillTime;

        foreach (var tower in towers) {
            tower.BuffManager.AddBuff(
                new AttackDamageBuff(skillValue, skilltime),tower);
            tower.BuffManager.AddBuff(
                new AttackDelayBuff(skillValue * .5f, skilltime),tower);
        }
    }
}