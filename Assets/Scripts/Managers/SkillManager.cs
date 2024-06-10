using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// ��� ��ų ���� �Ŵ���
/// </summary>
public class SkillManager {
    //��ų ���
    public SkillData.Skill[] SkillData;
    public Action<Define.SkillType> SetSkillAction;

    public SkillManager() {
        SetSkillAction = null;
    }

    /// <summary>
    /// ��ų ������ �ʱ�ȭ
    /// </summary>
    public void Init() {
        SkillData = new SkillData.Skill[(int)Define.SkillType.Count];

        for (int i = 0; i < SkillData.Length; i++) {
            SkillData[i] = new SkillData.Skill();   
            SkillData[i].SkillValue = Managers.Data.GameData.SkillDatas.Skills[i].SkillValue;
            SkillData[i].SkillTime = Managers.Data.GameData.SkillDatas.Skills[i].SkillTime;
            SkillData[i].SkillCost = Managers.Data.GameData.SkillDatas.Skills[i].SkillCost;
            SkillData[i].SkillCostUpValue = Managers.Data.GameData.SkillDatas.Skills[i].SkillCostUpValue;
            SkillData[i].SkillCoolTime = Managers.Data.GameData.SkillDatas.Skills[i].SkillCoolTime;
            SkillData[i].SkillDamage = Managers.Data.GameData.SkillDatas.Skills[i].SkillDamage;

            SkillData[i].SkillValueUpValue = Managers.Data.GameData.SkillDatas.Skills[i].SkillValueUpValue;
            SkillData[i].SkillTimeUpValue = Managers.Data.GameData.SkillDatas.Skills[i].SkillTimeUpValue;
            SkillData[i].SkillCoolTimeDownValue = Managers.Data.GameData.SkillDatas.Skills[i].SkillCoolTimeDownValue;
        }
    }

    /// <summary>
    /// ��ų ������ �� ������ �����
    /// </summary>
    /// <param name="skill">�������� ��ų Ÿ��</param>
    public void SetSkillValue(Define.SkillType skill) {
        SkillData[(int)skill].Level++;

        if(SkillData[(int)skill].Level == 1)
            SetSkillAction?.Invoke(skill);

        SkillData[(int)skill].SkillValue += SkillData[(int)skill].SkillValueUpValue;
        SkillData[(int)skill].SkillTime += SkillData[(int)skill].SkillTimeUpValue;
        SkillData[(int)skill].SkillCoolTime -= SkillData[(int)skill].SkillCoolTimeDownValue;
        SkillData[(int)skill].SkillCost = SkillData[(int)skill].Level * SkillData[(int)skill].SkillCostUpValue;
        SkillData[(int)skill].SkillDamage += SkillData[(int)skill].SkillDamage;

    }

    /// <summary>
    /// ��ų ��ȯ
    /// </summary>
    /// <param name="skill">��ȯ�� ��ų Ÿ��</param>
    public SkillData.Skill GetSkillValue(Define.SkillType skill) {
        return SkillData[(int)skill];
    }
}