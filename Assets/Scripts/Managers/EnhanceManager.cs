using System.Collections.Generic;

/// <summary>
/// ��� ���ڽ� ���� �Ŵ���
/// </summary>
public class EnhanceManager {
    //���� ���ڽ�
    public EnhanceData.EnhanceLevelData[] EnhanceData { get; private set; }
    /// <summary>
    /// ���ڽ� �ʱ�ȭ
    /// </summary>
    public void Init() {
        EnhanceData = new EnhanceData.EnhanceLevelData[(int)Define.TowerBundle.Count * (int)Define.EnhanceType.Count];

        for(int i = 0; i< EnhanceData.Length; i++) {
            EnhanceData[i] = new EnhanceData.EnhanceLevelData();
            EnhanceData[i].EnhanceCost = Managers.Data.GameData.EnhancesLevelDatas.Enhances[i].EnhanceCost;
            EnhanceData[i].EnhanceCostUpValue = Managers.Data.GameData.EnhancesLevelDatas.Enhances[i].EnhanceCostUpValue;
            EnhanceData[i].EnhanceValue = Managers.Data.GameData.EnhancesLevelDatas.Enhances[i].EnhanceValue;
            EnhanceData[i].EnhanceUpValue = Managers.Data.GameData.EnhancesLevelDatas.Enhances[i].EnhanceUpValue;
        }
    }

    /// <summary>
    /// ���ڽ� ������
    /// </summary>
    /// <param name="bundle">�������� ���ڽ��� Ÿ�� ���� Ÿ��</param>
    /// <param name="enhance">�������� ���ڽ� Ÿ��</param>
    public void SetEnhanceValue(Define.TowerBundle bundle, Define.EnhanceType enhance) {
        int index = (int)bundle * 2 + (int)enhance;
        EnhanceData[index].Level++;
        EnhanceData[index].EnhanceValue = EnhanceData[index].EnhanceUpValue * EnhanceData[index].Level;
        EnhanceData[index].EnhanceCost = EnhanceData[index].EnhanceCostUpValue * EnhanceData[index].Level;
    }

    /// <summary>
    /// ���ڽ� ��ȯ
    /// </summary>
    /// <param name="bundle">��ȯ�� ���ڽ��� Ÿ�� ���� Ÿ��</param>
    /// <param name="enhance">��ȯ�� ���ڽ� Ÿ��</param>
    /// <returns></returns>
    public EnhanceData.EnhanceLevelData GetEnhanceValue(Define.TowerBundle bundle, Define.EnhanceType enhance) {
        int index = (int)bundle * 2 + (int)enhance;
        return EnhanceData[index];
    }
}