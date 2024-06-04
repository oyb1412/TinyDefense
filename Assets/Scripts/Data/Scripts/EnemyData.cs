using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class EnemyData {
    [System.Serializable]
    public class EnemyStatusData {
        public int Level;
        public int Reward;
        public float MaxHp;
        public float MoveSpeed;
        public float MaxHpUpVolume;
    }

    public EnemyStatusData Enemys;

    public EnemyData() {
        Enemys = new EnemyStatusData();
    }
}