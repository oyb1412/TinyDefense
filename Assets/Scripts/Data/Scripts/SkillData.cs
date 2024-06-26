
/// <summary>
/// 기본적인 스킬 데이터
/// </summary>
[System.Serializable]
public class SkillData {
    
    [System.Serializable]
    public class Skill {
        //레벨
        public int Level;

        //스킬 데미지
        public float SkillDamage;

        //스킬의 밸류(이동속도 저하, 공속 증가 등)
        public float SkillValue;
        //레벨업시 증가하는 밸류
        public float SkillValueUpValue;

        //스킬 지속시간
        public float SkillTime;
        //레벨업시 증가하는 스킬 지속시간
        public float SkillTimeUpValue;
        //스킬 쿨타임
        public float SkillCoolTime;
        //레벨업시 감소하는 스킬 지속시간
        public float SkillCoolTimeDownValue;
        //스킬 레벨업 비용
        public int SkillCost;
        //레벨업시 증가하는 스킬 레벨업 비용
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