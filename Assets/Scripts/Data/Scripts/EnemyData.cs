/// <summary>
/// �⺻���� �ֳʹ� ������
/// </summary>
[System.Serializable]
public class EnemyData {
    [System.Serializable]
    public class EnemyStatusData {
        //����
        public int Level; 
        //����
        public int Reward;
        //�ִ� ü��
        public float MaxHp;
        //�̵� �ӵ�
        public float MoveSpeed;
        //�������� ���� �ִ�ü�� ������
        public float MaxHpUpVolume;
    }

    public EnemyStatusData Enemys;

    public EnemyData() {
        Enemys = new EnemyStatusData();
    }
}