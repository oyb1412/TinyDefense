using System.Collections.Generic;

/// <summary>
/// 모든 타워 관리 매니저
/// </summary>
public class TowerManager {
    //생성된 타워 리스트
    public HashSet<TowerBase> TowerList { get; private set; } 
    public TowerData.TowerLevelData[] TowerData { get; private set; }

    public void Clear() {
        TowerList = new HashSet<TowerBase>();
    }

    /// <summary>
    /// 타워 초기화
    /// </summary>
    public void Init() {
        TowerData = new TowerData.TowerLevelData[(int)Define.TowerBundle.Count];
        TowerData = Managers.Data.GameData.TowerDatas.Towers;
    }

    /// <summary>
    /// 리스트에 타워 추가
    /// 타워 생성시 호출
    /// </summary>
    /// <param name="go">추가할 타워</param>
    public void AddTower(TowerBase go) {
        if (TowerList.Contains(go))
            return;

        TowerList.Add(go);
    }

    /// <summary>
    /// 리스트에서 타워 제거
    /// 타워 제거시 호출
    /// </summary>
    /// <param name="go">제거할 타워</param>
    public void RemoveTower(TowerBase go) {
        if (!TowerList.Contains(go))
            return;

        TowerList.Remove(go);
    }
}