using System.Collections.Generic;

/// <summary>
/// ��� Ÿ�� ���� �Ŵ���
/// </summary>
public class TowerManager {
    //������ Ÿ�� ����Ʈ
    public HashSet<TowerBase> TowerList { get; private set; } 
    public TowerData.TowerLevelData[] TowerData { get; private set; }

    public void Clear() {
        TowerList = new HashSet<TowerBase>();
    }

    /// <summary>
    /// Ÿ�� �ʱ�ȭ
    /// </summary>
    public void Init() {
        TowerData = new TowerData.TowerLevelData[(int)Define.TowerBundle.Count];
        TowerData = Managers.Data.GameData.TowerDatas.Towers;
    }

    /// <summary>
    /// ����Ʈ�� Ÿ�� �߰�
    /// Ÿ�� ������ ȣ��
    /// </summary>
    /// <param name="go">�߰��� Ÿ��</param>
    public void AddTower(TowerBase go) {
        if (TowerList.Contains(go))
            return;

        TowerList.Add(go);
    }

    /// <summary>
    /// ����Ʈ���� Ÿ�� ����
    /// Ÿ�� ���Ž� ȣ��
    /// </summary>
    /// <param name="go">������ Ÿ��</param>
    public void RemoveTower(TowerBase go) {
        if (!TowerList.Contains(go))
            return;

        TowerList.Remove(go);
    }
}