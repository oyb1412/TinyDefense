using System.Collections.Generic;

/// <summary>
/// 모든 인핸스 관리 매니저
/// </summary>
public class EnhanceManager {
    //현재 인핸스
    public EnhanceData.EnhanceLevelData[] EnhanceData { get; private set; }
    /// <summary>
    /// 인핸스 초기화
    /// </summary>
    public void Init() {
        EnhanceData = new EnhanceData.EnhanceLevelData[(int)Define.TowerBundle.Count * (int)Define.EnhanceType.Count];

        for(int i = 0; i< EnhanceData.Length; i++) {
            EnhanceData[i] = new EnhanceData.EnhanceLevelData();
            EnhanceData[i].EnhanceCost = Managers.Data.GameData.EnhanceDatas.Enhances.EnhanceCost;
            EnhanceData[i].EnhanceCostUpValue = Managers.Data.GameData.EnhanceDatas.Enhances.EnhanceCostUpValue;
            EnhanceData[i].EnhanceValue = Managers.Data.GameData.EnhanceDatas.Enhances.EnhanceValue;
            EnhanceData[i].EnhanceUpValue = Managers.Data.GameData.EnhanceDatas.Enhances.EnhanceUpValue;
        }
    }

    /// <summary>
    /// 인핸스 레벨업
    /// </summary>
    /// <param name="bundle">레벨업할 인핸스의 타워 번들 타입</param>
    /// <param name="enhance">레벨업할 인핸스 타입</param>
    public void SetEnhanceValue(Define.TowerBundle bundle, Define.EnhanceType enhance) {
        int index = (int)bundle * 2 + (int)enhance;
        EnhanceData[index].Level++;
        EnhanceData[index].EnhanceValue = EnhanceData[index].EnhanceUpValue * EnhanceData[index].Level;
        EnhanceData[index].EnhanceCost = EnhanceData[index].EnhanceCostUpValue * EnhanceData[index].Level;
    }

    /// <summary>
    /// 인핸스 반환
    /// </summary>
    /// <param name="bundle">반환할 인핸스의 타워 번들 타입</param>
    /// <param name="enhance">반환할 인핸스 타입</param>
    /// <returns></returns>
    public EnhanceData.EnhanceLevelData GetEnhanceValue(Define.TowerBundle bundle, Define.EnhanceType enhance) {
        int index = (int)bundle * 2 + (int)enhance;
        return EnhanceData[index];
    }
}