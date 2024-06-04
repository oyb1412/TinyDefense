using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptble Object/SkillData")]
public class SkillData : ScriptableObject {
    
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

    public void Init() {
        Skills = new Skill[(int)Define.SkillType.Count];
        for(int i = 0; i< Skills.Length; i++) {
            Skills[i] = new Skill();
            Skills[i].SkillCost = 50;
            Skills[i].SkillCostUpValue= 50;

            if (i == (int)Define.SkillType.PowerUp) {
                Skills[i].SkillValue = 0.2f;
                Skills[i].SkillValueUpValue = 0.1f;

                Skills[i].SkillTime = 5f;
                Skills[i].SkillTimeUpValue = 2f;

                Skills[i].SkillCoolTime = 40f;
                Skills[i].SkillCoolTimeDownValue = 2f;
            }
            else if(i == (int)Define.SkillType.Stun) {
                Skills[i].SkillValue = 0f;
                Skills[i].SkillValueUpValue = 0f;

                Skills[i].SkillDamage = 200f;

                Skills[i].SkillTime = 3f;
                Skills[i].SkillTimeUpValue = .5f;

                Skills[i].SkillCoolTime = 60f;
                Skills[i].SkillCoolTimeDownValue = 2f;
            }
            else if(i == (int)Define.SkillType.Torando) {
                Skills[i].SkillValue = .5f;
                Skills[i].SkillValueUpValue = 0.05f;

                Skills[i].SkillDamage = 200f;

                Skills[i].SkillTime = 10f;
                Skills[i].SkillTimeUpValue = 1f;

                Skills[i].SkillCoolTime = 30f;
                Skills[i].SkillCoolTimeDownValue = 1f;
            }
        }
    }
}