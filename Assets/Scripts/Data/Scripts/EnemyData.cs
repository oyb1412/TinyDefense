using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptble Object/EnemyData")]
public class EnemyData : ScriptableObject {
    
    

    [System.Serializable]
    public class EnemyStatusData {
        public int Level;
        public int Reward;
        public float MaxHp;
        public float MoveSpeed;
        public float MaxHpUpVolume;
    }

    public EnemyStatusData Enemys;

    public void Init() {
        Enemys = new EnemyStatusData();
        Enemys.MaxHp = 60;
        Enemys.MoveSpeed = 0.5f;
        Enemys.Reward = 3;

        Enemys.MaxHpUpVolume = 0.2f;
    }
}