using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using static SkillData;

[System.Serializable]
public class EnhanceData  {
    

    [System.Serializable]
    public class EnhanceLevelData {
        public int Level;
        public float EnhanceValue;
        public float EnhanceUpValue;
        public int EnhanceCost;
        public int EnhanceCostUpValue;
    }

    //번들로 관리
    public EnhanceLevelData Enhances;

    public EnhanceData() {
        Enhances = new EnhanceLevelData();
        
    }
}