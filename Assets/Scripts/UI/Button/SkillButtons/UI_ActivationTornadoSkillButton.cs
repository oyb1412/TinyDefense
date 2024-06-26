using UnityEngine;

/// <summary>
/// 토네이도 스킬 버튼 클래스
/// </summary>
public class UI_ActivationTornadoSkillButton : UI_ActivationSkillButton {
    //토네이도 객체 프리펩
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
    /// 토네이도 스킬 실행
    /// </summary>
    protected override void OnSelect() {
        base.OnSelect();

        //스턴은 즉시 발동
        //스킬 사용 후 쿨타임 시작
        base.OnSelect();
        var skill = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);
        UseSkill(skill.SkillCoolTime);

        //토네이도 생성
        Managers.Resources.Activation(tornadoPrefab);
    }
}