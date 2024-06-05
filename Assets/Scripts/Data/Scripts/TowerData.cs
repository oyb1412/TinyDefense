
/// <summary>
/// 기본적인 타워 데이터
/// </summary>
[System.Serializable]
public class TowerData  {
    
    [System.Serializable]
    public class TowerLevelData {
        //타워 레벨
        public int Level;
        //타워 판매 비용
        public int SellReward;
        //레벨업시 증가하는 타워 판매 비용
        public int SellRewardUpValue;
        //타워 공격력
        public float AttackDamage;
        //레벨업시 증가하는 타워 공격력
        public float AttackDamageUpValue;
        //타워 공격 간격
        public float AttackDelay;
        //레벨업시 감소하는 타워 공격 간격
        public float AttackDelayDownValue;
        //타워 사거리
        public float AttackRange;
        //레벨업시 증가하는 타워 사거리
        public float AttackRangeUpValue;
        //타워 버프량(버프를 시전하는 타워 전용)
        public float BuffValue;
        //레벨업시 증가하는 버프량
        public float BuffValueUpValue;
        //타워 버프 시간
        public float BuffTime;
        //레벨업시 증가하는 버프 시간
        public float BuffTimeUpValue;
        //타워 슬로우 밸류(슬로우를 거는 타워 전용)
        public float SlowValue;
        //레벨업시 증가하는 슬로우 밸류
        public float SlowValueUpValue;
        //타워 슬로우 시간
        public float SlowTime;
        //레벨업시 증가하는 슬로우 시간
        public float SlowTimeUpValue;
        //타워 데미지 디버프 밸류(데미지 디버프를 거는 타워 전용)
        public float FireValue;
        //레벨업시 증가하는 데미지 디버프 밸류
        public float FireValueUpValue;
        //데미지 디버프 시간
        public float FireTime;
        //레벨업시 증가하는 데미지 디버프 시간
        public float FireTimeUpValue;
    }

    //타워 타입으로 분류
    public TowerLevelData[] Towers;

    public TowerData() {
        Towers = new TowerLevelData[(int)Define.TowerType.Count];
        for (int i = 0; i < Towers.Length; i++) {
            Towers[i] = new TowerLevelData();
        }
    }
}
