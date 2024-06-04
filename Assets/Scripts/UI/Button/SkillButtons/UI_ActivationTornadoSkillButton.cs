using DG.Tweening;
using UnityEngine;

public class UI_ActivationTornadoSkillButton : UI_ActivationSkillButton {
    private GameObject tornadoPrefab;
    public override void Init() {
        base.Init();
        tornadoPrefab = Resources.Load<GameObject>(Define.SKILL_TORNADO_PATH);
        Managers.Skill.SetSkillAction += ((type) => {
            if (type == Define.SkillType.Tornado) {
                seletable = true;
                button.interactable = true;
            }
        });
    }
    protected override void OnSelect() {
        base.OnSelect();

        //������ ��� �ߵ�
        //��ų ��� �� ��Ÿ�� ����
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);
        UseSkill(skill.SkillCoolTime);


        Managers.Resources.Instantiate(tornadoPrefab);
    }
}