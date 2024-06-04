using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData {
    
    [System.Serializable]
    public class Skill {
        public int Level;

        public float SkillDamage;

        public float SkillValue;
        public float SkillValueUpValue;

        public float SkillTime;
        public float SkillTimeUpValue;

        public float SkillCoolTime;
        public float SkillCoolTimeDownValue;

        public int SkillCost;
        public int SkillCostUpValue;

    }

    public Skill[] Skills;

    public SkillData() {
        Skills = new Skill[(int)Define.SkillType.Count];
        for(int i =  0; i < Skills.Length; i++) {
            Skills[i] = new Skill();
        }
    }

}