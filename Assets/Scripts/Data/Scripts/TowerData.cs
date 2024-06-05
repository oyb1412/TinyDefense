
/// <summary>
/// �⺻���� Ÿ�� ������
/// </summary>
[System.Serializable]
public class TowerData  {
    
    [System.Serializable]
    public class TowerLevelData {
        //Ÿ�� ����
        public int Level;
        //Ÿ�� �Ǹ� ���
        public int SellReward;
        //�������� �����ϴ� Ÿ�� �Ǹ� ���
        public int SellRewardUpValue;
        //Ÿ�� ���ݷ�
        public float AttackDamage;
        //�������� �����ϴ� Ÿ�� ���ݷ�
        public float AttackDamageUpValue;
        //Ÿ�� ���� ����
        public float AttackDelay;
        //�������� �����ϴ� Ÿ�� ���� ����
        public float AttackDelayDownValue;
        //Ÿ�� ��Ÿ�
        public float AttackRange;
        //�������� �����ϴ� Ÿ�� ��Ÿ�
        public float AttackRangeUpValue;
        //Ÿ�� ������(������ �����ϴ� Ÿ�� ����)
        public float BuffValue;
        //�������� �����ϴ� ������
        public float BuffValueUpValue;
        //Ÿ�� ���� �ð�
        public float BuffTime;
        //�������� �����ϴ� ���� �ð�
        public float BuffTimeUpValue;
        //Ÿ�� ���ο� ���(���ο츦 �Ŵ� Ÿ�� ����)
        public float SlowValue;
        //�������� �����ϴ� ���ο� ���
        public float SlowValueUpValue;
        //Ÿ�� ���ο� �ð�
        public float SlowTime;
        //�������� �����ϴ� ���ο� �ð�
        public float SlowTimeUpValue;
        //Ÿ�� ������ ����� ���(������ ������� �Ŵ� Ÿ�� ����)
        public float FireValue;
        //�������� �����ϴ� ������ ����� ���
        public float FireValueUpValue;
        //������ ����� �ð�
        public float FireTime;
        //�������� �����ϴ� ������ ����� �ð�
        public float FireTimeUpValue;
    }

    //Ÿ�� Ÿ������ �з�
    public TowerLevelData[] Towers;

    public TowerData() {
        Towers = new TowerLevelData[(int)Define.TowerType.Count];
        for (int i = 0; i < Towers.Length; i++) {
            Towers[i] = new TowerLevelData();
        }
    }
}
