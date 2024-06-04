using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine;
using static SkillData;

[System.Serializable]
public class TowerData  {
    
    [System.Serializable]
    public class TowerLevelData {
        public int Level;

        public int SellReward;
        public int SellRewardUpValue;

        public float AttackDamage;
        public float AttackDamageUpValue;

        public float AttackDelay;
        public float AttackDelayDownValue;

        public float AttackRange;
        public float AttackRangeUpValue;

        public float BuffValue;
        public float BuffValueUpValue;

        public float BuffTime;
        public float BuffTimeUpValue;

        public float SlowValue;
        public float SlowValueUpValue;

        public float SlowTime;
        public float SlowTimeUpValue;

        public float FireValue;
        public float FireValueUpValue;

        public float FireTime;
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
