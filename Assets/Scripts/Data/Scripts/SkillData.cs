
/// <summary>
/// �⺻���� ��ų ������
/// </summary>
[System.Serializable]
public class SkillData {
    
    [System.Serializable]
    public class Skill {
        //����
        public int Level;

        //��ų ������
        public float SkillDamage;

        //��ų�� ���(�̵��ӵ� ����, ���� ���� ��)
        public float SkillValue;
        //�������� �����ϴ� ���
        public float SkillValueUpValue;

        //��ų ���ӽð�
        public float SkillTime;
        //�������� �����ϴ� ��ų ���ӽð�
        public float SkillTimeUpValue;
        //��ų ��Ÿ��
        public float SkillCoolTime;
        //�������� �����ϴ� ��ų ���ӽð�
        public float SkillCoolTimeDownValue;
        //��ų ������ ���
        public int SkillCost;
        //�������� �����ϴ� ��ų ������ ���
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