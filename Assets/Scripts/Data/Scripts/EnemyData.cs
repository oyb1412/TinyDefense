/// <summary>
/// 기본적인 애너미 데이터
/// </summary>
[System.Serializable]
public class EnemyData {
    [System.Serializable]
    public class EnemyStatusData {
        //레벨
        public int Level; 
        //보상
        public int Reward;
        //최대 체력
        public float MaxHp;
        //이동 속도
        public float MoveSpeed;
        //레벨업에 따른 최대체력 증가량
        public float MaxHpUpVolume;
    }

    public EnemyStatusData Enemys;

    public EnemyData() {
        Enemys = new EnemyStatusData();
    }
} 