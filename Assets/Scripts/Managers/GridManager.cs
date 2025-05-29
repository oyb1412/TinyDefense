using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 맵을 격자 형태로 관리
/// </summary>
public class GridManager {
    //각 격자는 키값으로 위치, 밸류값으로 적 객체 리스트를 지니고 있다.
    private Dictionary<Vector2Int, List<EnemyBase>> grid = new Dictionary<Vector2Int, List<EnemyBase>>();

    //각 격자의 크기(임의로 지정)
    public float cellSize = 1f;

    /// <summary>
    /// 현재 월드포지션을 매개변수로 받아 격자 위치를 반환
    /// </summary>
    /// <param name="worldPos">월드포지션</param>
    /// <returns></returns>
    public Vector2Int GetGridPos(Vector3 worldPos) {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / cellSize),
            Mathf.FloorToInt(worldPos.y / cellSize)
        );
    }

    /// <summary>
    /// 적 객체를 매개변수로 받아 격자에 등록
    /// </summary>
    /// <param name="enemy"></param>
    public void Register(EnemyBase enemy) {
        var pos = GetGridPos(enemy.transform.position);
        if (!grid.ContainsKey(pos))
            grid[pos] = new List<EnemyBase>();

        grid[pos].Add(enemy);
    }

    /// <summary>
    /// 적 객체와 그 위치를 매개변수로 받아 격자에서 해제
    /// </summary>
    /// <param name="enemy">적 객체</param>
    /// <param name="oldPos">이동 전 위치</param>
    public void Unregister(EnemyBase enemy, Vector2Int oldPos) {
        if (grid.TryGetValue(oldPos, out var list))
            list.Remove(enemy);
    }

    /// <summary>
    /// 적의 위치가 변경되었을 때, 원래 격자에서 해제하고 새 격자에 등록
    /// </summary>
    /// <param name="enemy">적 객체</param>
    /// <param name="oldPos">변경 전 위치</param>
    /// <param name="newPos">변경 후 위치</param>
    public void UpdateGridPos(EnemyBase enemy, Vector2Int oldPos, Vector2Int newPos) {
        if (oldPos == newPos) return;
        Unregister(enemy, oldPos);
        Register(enemy);
    }

    /// <summary>
    /// 타워의 위치와 사거리를 매개변수로 받아 사거리 내의 격자를 반환
    /// </summary>
    /// <param name="center">타워의 위치</param>
    /// <param name="range">타워의 사거리</param>
    /// <returns></returns>
    public List<Vector2Int> GetGridsInRange(Vector3 center, float range) {
        List<Vector2Int> result = new();
        int cellRange = Mathf.CeilToInt(range / cellSize);
        var centerGrid = GetGridPos(center);

        for (int x = -cellRange; x <= cellRange; x++) {
            for (int y = -cellRange; y <= cellRange; y++) {
                var offset = new Vector2Int(x, y);
                var gridPos = centerGrid + offset;
                if (grid.ContainsKey(gridPos))
                    result.Add(gridPos);
            }
        }
        return result;
    }

    /// <summary>
    /// 타워의 위치와 사거리를 매개변수로 받아 사거리 내 적 리스트를 반환
    /// </summary>
    /// <param name="center">타워의 위치</param>
    /// <param name="range">타워의 사거리</param>
    /// <returns></returns>
    public List<EnemyBase> GetEnemiesInRange(Vector3 center, float range) {
        var grids = GetGridsInRange(center, range);
        List<EnemyBase> result = new();
        float rangeSqr = range * range;

        foreach (var item in grids) {
            foreach (var enemy in grid[item]) {
                if (enemy == null) continue;
                if ((enemy.transform.position - center).sqrMagnitude <= rangeSqr)
                    result.Add(enemy);
            }
        }

        return result;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Clear() {
        grid.Clear();
    }
}