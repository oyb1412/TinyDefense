using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ���� ��ų Ŭ����
/// </summary>
public class UI_ActivationStunSkillButton : UI_ActivationSkillButton {

    public override void Init() {
        buttonSfxType = Define.SFXType.StunSkill;
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

        //��� �� ��ȸ
        //��� ������ ����� ���� �� ������
        var enemys = Managers.Enemy.EnemyList.ToHashSet();
        foreach (var enemy in enemys) {
            if(Util.IsEnemyNull(enemy)) 
                continue;

            enemy.DebuffManager.AddDebuff(new StunDebuff(skill.SkillTime), enemy);
            enemy.EnemyStatus.SetHp(skill.SkillDamage);
        }
        
        //ī�޶� ����ũ
        Camera.main.transform.DOShakePosition(3f, .2f).SetEase(Ease.Linear);
    }
}