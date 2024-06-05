/// <summary>
/// �⺻���� ���ڽ� ������
/// </summary>
[System.Serializable]
public class EnhanceData  {
    

    [System.Serializable]
    public class EnhanceLevelData {
        //����
        public int Level;
        //���ڽ� ���
        public float EnhanceValue;
        //���ڽ� �������� �����ϴ� ���
        public float EnhanceUpValue;
        //���ڽ� ������ ���
        public int EnhanceCost;
        //���ڽ� �������� �����ϴ� ������ ���
        public int EnhanceCostUpValue;
    }

    public EnhanceLevelData Enhances;

    public EnhanceData() {
        Enhances = new EnhanceLevelData();
        
    }
}