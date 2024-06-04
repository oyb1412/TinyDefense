using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptble Object/TowerData")]
public class TowerData : ScriptableObject {
    
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

    public void Init() {
        Towers = new TowerLevelData[(int)Define.TowerType.Count];
        for (int i = 0; i < Towers.Length; i++) {
            Towers[i] = new TowerLevelData();
            Towers[i].SellReward =  7;
            Towers[i].SellRewardUpValue =  10;
            Towers[i].AttackDamageUpValue = 2.5f;
            Towers[i].AttackDelayDownValue = 0.1f;
            Towers[i].AttackRangeUpValue = 0.05f;

            if (i == (int)Define.TowerType.Knight) {
                Towers[i].AttackDamage = 60f;
                Towers[i].AttackDelay = 3f;
                Towers[i].AttackRange = 5f;
            } else if (i == (int)Define.TowerType.Warrior) {
                Towers[i].AttackDamage = 100f;
                Towers[i].AttackDelay = 4f;
                Towers[i].AttackRange = 5f;
            } else if (i == (int)Define.TowerType.Rogue) {
                Towers[i].AttackDamage = 40f;
                Towers[i].AttackDelay = 2f;
                Towers[i].AttackRange = 5f;
            } else if (i == (int)Define.TowerType.Hunter) {
                Towers[i].AttackDamage = 40f;
                Towers[i].AttackDelay = 2f;
                Towers[i].AttackRange = 6f;
            } else if (i == (int)Define.TowerType.Ranger) {
                Towers[i].AttackDamage = 20f;
                Towers[i].AttackDelay = 1f;
                Towers[i].AttackRange = 6f;
            } else if (i == (int)Define.TowerType.Sniper) {
                Towers[i].AttackDamage = 140f;
                Towers[i].AttackDelay = 5f;
                Towers[i].AttackRange = 6f;
            } else if (i == (int)Define.TowerType.Firemage) {
                Towers[i].AttackDamage = 60f;
                Towers[i].AttackDelay = 5f;
                Towers[i].FireValue = .2f;
                Towers[i].FireTime = 1.5f;
                Towers[i].FireValueUpValue = .1f;
                Towers[i].FireTimeUpValue = .5f;
                Towers[i].AttackRange = 4.5f;
            } else if (i == (int)Define.TowerType.Icemage) {
                Towers[i].AttackDamage = 60f;
                Towers[i].AttackDelay = 5f;
                Towers[i].SlowValue = .1f;
                Towers[i].SlowTime = 1f;
                Towers[i].SlowValueUpValue = .1f;
                Towers[i].SlowTimeUpValue = .5f;
                Towers[i].AttackRange = 4.5f;
            } else if (i == (int)Define.TowerType.Sister) {
                Towers[i].AttackDamage = 40f;
                Towers[i].AttackDelay = 6f;
                Towers[i].BuffValue = .2f;
                Towers[i].BuffTime = 3f;
                Towers[i].BuffValueUpValue = .1f;
                Towers[i].BuffTimeUpValue = 1f;
                Towers[i].AttackRange = 4.5f;
            }
        }
    }
}
