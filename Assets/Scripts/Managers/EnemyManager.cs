using System;
using System.Collections.Generic;

/// <summary>
/// ��� �� ���� �Ŵ���
/// </summary>
public class EnemyManager {
    //������ ��� �� ����Ʈ
    public HashSet<EnemyBase> enemyList { get; private set; } = new HashSet<EnemyBase>();
    //�ʿ� �����ϴ� �� ���� ����Ǹ� ȣ��
    public Action<int> EnemyNumberAction;
    private int enemyHandle = 1;
    public EnemyData.EnemyStatusData EnemyData { get; private set; }

    public void Init() {
        EnemyData = new EnemyData.EnemyStatusData();
        EnemyData = Managers.Data.GameData.EnemysLevelDatas.Enemys;
    }

    public List<EnemyBase> GetEnemyList() {
        return new List<EnemyBase>(enemyList);
    }

    /// <summary>
    /// ����Ʈ�� �� �߰� �� �̺�Ʈ ȣ��
    /// </summary>
    /// <param name="enemy">�߰��� ��</param>
    /// 
    public void AddEnemy(EnemyBase enemy) {
        if (enemyList.Contains(enemy))
            return;

        enemyList.Add(enemy);
        enemy.EnemyHandle = enemyHandle;
        enemyHandle++;
        EnemyNumberAction?.Invoke(enemyList.Count);
    }

    /// <summary>
    /// ����Ʈ���� �� ���� �� �̺�Ʈ ȣ��
    /// </summary>
    /// <param name="enemy">������ ��</param>
    public void RemoveEnemy(EnemyBase enemy) {
        if (!enemyList.Contains(enemy))
            return;

        enemyList.Remove(enemy);
        enemy.EnemyHandle = 0;
        EnemyNumberAction?.Invoke(enemyList.Count);
    }
}