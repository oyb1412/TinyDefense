using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnhanceData", menuName = "Scriptble Object/EnhanceData")]
public class EnhanceData : ScriptableObject {
    

    [System.Serializable]
    public class EnhanceLevelData {
        public int Level;
        public float EnhanceValue;
        public float EnhanceUpValue;
        public int EnhanceCost;
        public int EnhanceCostUpValue;
    }


    //번들로 관리
    public EnhanceLevelData[] Enhances;
    
    public void Init() {
        Enhances = new EnhanceLevelData[(int)Define.TowerBundle.Count * (int)Define.EnhanceType.Count];
        for (int i = 0; i< Enhances.Length; i++) {
            Enhances[i] = new EnhanceLevelData();
            Enhances[i].EnhanceCost = 50;
            Enhances[i].EnhanceCostUpValue = 50;
            Enhances[i].EnhanceValue = 0.1f;
            Enhances[i].EnhanceUpValue = 0.1f;
        }
    }
}