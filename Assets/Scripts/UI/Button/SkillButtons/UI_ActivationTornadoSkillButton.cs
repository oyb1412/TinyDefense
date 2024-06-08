using UnityEngine;

/// <summary>
/// ����̵� ��ų ��ư Ŭ����
/// </summary>
public class UI_ActivationTornadoSkillButton : UI_ActivationSkillButton {
    //����̵� ��ü ������
    private GameObject tornadoPrefab;
    public override void Init() {
        buttonSfxType = Define.SFXType.TornadoSkill;
        base.Init();
        if(tornadoPrefab == null ) 
            tornadoPrefab = Resources.Load<GameObject>(Managers.Data.DefineData.SKILL_TORNADO_PATH);

        Managers.Skill.SetSkillAction += ((type) => {
            if (type == Define.SkillType.Tornado) {
                seletable = true;
                button.interactable = true;
            }
        });
    }

    /// <summary>
    /// ����̵� ��ų ����
    /// </summary>
    protected override void OnSelect() {
        base.OnSelect();

        //������ ��� �ߵ�
        //��ų ��� �� ��Ÿ�� ����
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);
        UseSkill(skill.SkillCoolTime);

        //����̵� ����
        Managers.Resources.Activation(tornadoPrefab);
    }
}