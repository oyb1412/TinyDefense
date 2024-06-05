using System;
using System.Collections.Generic;

/// <summary>
/// 모든 적 관리 매니저
/// </summary>
public class EnemyManager {
    //생성된 모든 적 리스트
    public List<EnemyBase> EnemyList { get; private set; }
    //맵에 존재하는 적 수가 변경되면 호출
    public Action<int> EnemyNumberAction;
    public EnemyData.EnemyStatusData EnemyData { get; private set; }

    /// <summary>
    /// 적 리스트 초기화
    /// </summary>
    public void Clear() {
        EnemyNumberAction = null;
        EnemyList = new List<EnemyBase>();
    }

    public void Init() {
        EnemyData = new EnemyData.EnemyStatusData();
        EnemyData = Managers.Data.GameData.EnemyDatas.Enemys;
    }

    /// <summary>
    /// 리스트에 적 추가 및 이벤트 호출
    /// </summary>
    /// <param name="enemy">추가할 적</param>
    /// 
    public void AddEnemy(EnemyBase enemy) {
        if (EnemyList.Contains(enemy))
            return;

        EnemyList.Add(enemy);
        EnemyNumberAction?.Invoke(EnemyList.Count);
    }

    /// <summary>
    /// 리스트에서 적 제거 및 이벤트 호출
    /// </summary>
    /// <param name="enemy">제거할 적</param>
    public void RemoveEnemy(EnemyBase enemy) {
        if (!EnemyList.Contains(enemy))
            return;

        EnemyList.Remove(enemy);
        EnemyNumberAction?.Invoke(EnemyList.Count);
    }
}