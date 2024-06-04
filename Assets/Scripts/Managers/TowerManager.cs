using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� Ÿ�� ���� �Ŵ���
/// </summary>
public class TowerManager {
    //������ Ÿ�� ����Ʈ
    public HashSet<TowerBase> TowerList { get; private set; } = new HashSet<TowerBase>();

    private int towerHandle = 1;

    public TowerData.TowerLevelData[] TowerData { get; private set; }
    /// <summary>
    /// ���ڽ� �ʱ�ȭ
    /// </summary>
    public void Init() {
        TowerData = new TowerData.TowerLevelData[(int)Define.TowerBundle.Count];
        TowerData = Managers.Data.GameData.TowersLevelDatas.Towers;
    }

    /// <summary>
    /// ����Ʈ�� Ÿ�� �߰�
    /// Ÿ�� ������ ȣ��
    /// </summary>
    /// <param name="go">�߰��� Ÿ��</param>
    public void AddTower(TowerBase go) {
        if (TowerList.Contains(go))
            return;

        go.TowerHandle = towerHandle;
        towerHandle++;
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

        go.TowerHandle = 0;
        TowerList.Remove(go);
    }
}