using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 스턴 스킬 클래스
/// </summary>
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

    /// <summary>
    /// 스턴 스킬 선택
    /// </summary>
    protected override void OnSelect() {
        //스턴은 즉시 발동
        //스킬 사용 후 쿨타임 시작
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Stun);
        UseSkill(skill.SkillCoolTime);

        var enemys = Managers.Enemy.EnemyList;

        //모든 적 순회
        //모든 적에게 디버프 적용 및 데미지
        for (int i = 0; i< enemys.Count; i++) {
            if (enemys[i] == null)
                continue;

            enemys[i].DebuffManager.AddDebuff(new StunDebuff(skill.SkillTime), enemys[i]);
            enemys[i].EnemyStatus.SetHp(skill.SkillDamage);
        }

        //카메라 쉐이크
        Camera.main.transform.DOShakePosition(3f, .2f).SetEase(Ease.Linear);
    }
}