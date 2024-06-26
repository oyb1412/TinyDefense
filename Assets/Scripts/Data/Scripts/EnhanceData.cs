/// <summary>
/// 기본적인 인핸스 데이터
/// </summary>
[System.Serializable]
public class EnhanceData  {
    

    [System.Serializable]
    public class EnhanceLevelData {
        //레벨
        public int Level;
        //인핸스 밸류
        public float EnhanceValue;
        //인핸스 레벨업시 증가하는 밸류
        public float EnhanceUpValue;
        //인핸스 레벨업 비용
        public int EnhanceCost;
        //인핸스 레벨업시 증가하는 레벨업 비용
        public int EnhanceCostUpValue;
    }

    public EnhanceLevelData Enhances;

    public EnhanceData() {
        Enhances = new EnhanceLevelData();
        
    }
}