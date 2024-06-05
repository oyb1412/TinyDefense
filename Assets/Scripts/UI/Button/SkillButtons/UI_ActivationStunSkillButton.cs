using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���� ��ų Ŭ����
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
    /// ���� ��ų ����
    /// </summary>
    protected override void OnSelect() {
        //������ ��� �ߵ�
        //��ų ��� �� ��Ÿ�� ����
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Stun);
        UseSkill(skill.SkillCoolTime);

        var enemys = Managers.Enemy.EnemyList;

        //��� �� ��ȸ
        //��� ������ ����� ���� �� ������
        for (int i = 0; i< enemys.Count; i++) {
            if (enemys[i] == null)
                continue;

            enemys[i].DebuffManager.AddDebuff(new StunDebuff(skill.SkillTime), enemys[i]);
            enemys[i].EnemyStatus.SetHp(skill.SkillDamage);
        }

        //ī�޶� ����ũ
        Camera.main.transform.DOShakePosition(3f, .2f).SetEase(Ease.Linear);
    }
}