using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static TowerBase;
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
        var enemyList = Managers.Enemy.EnemyList;
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].DebuffManager.AddDebuff(new StunDebuff(skill.SkillTime), enemyList[i]);
            float enemyMaxHp = enemyList[i].EnemyStatus.MaxHp;
            float skillDamage = Mathf.Min(enemyList[i].EnemyStatus.CurrentHp * skill.SkillDamage, enemyMaxHp * 0.5f);
            enemyList[i].EnemyStatus.SetHp(skillDamage);
        }
        
        //ī�޶� ����ũ
        Camera.main.transform.DOShakePosition(3f, .2f).SetEase(Ease.Linear);
    }
}