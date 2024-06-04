using DG.Tweening;
using UnityEngine;
public class UI_ActivationStunSkillButton : UI_ActivationSkillButton {

    public override void Init() {
        base.Init();
        Managers.Skill.SetSkillAction += ((type) => {
            if (type == Define.SkillType.Stun) {
                seletable = true;
                button.interactable = true;
            }
        });
    }
    protected override void OnSelect() {
        //스턴은 즉시 발동
        //스킬 사용 후 쿨타임 시작
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Stun);
        UseSkill(skill.SkillCoolTime);

        var enemys = Managers.Enemy.GetEnemyList();
        foreach (var enemy in enemys) {
            enemy.DebuffManager.AddDebuff(new StunDebuff(skill.SkillTime), enemy);
            enemy.EnemyStatus.SetHp(skill.SkillDamage);
        }

        Camera.main.transform.DOShakePosition(3f, .2f).SetEase(Ease.Linear);
    }
}