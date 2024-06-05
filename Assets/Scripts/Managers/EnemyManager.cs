using System;
using System.Collections.Generic;

/// <summary>
/// ��� �� ���� �Ŵ���
/// </summary>
public class EnemyManager {
    //������ ��� �� ����Ʈ
    public List<EnemyBase> EnemyList { get; private set; }
    //�ʿ� �����ϴ� �� ���� ����Ǹ� ȣ��
    public Action<int> EnemyNumberAction;
    public EnemyData.EnemyStatusData EnemyData { get; private set; }

    /// <summary>
    /// �� ����Ʈ �ʱ�ȭ
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
    /// ����Ʈ�� �� �߰� �� �̺�Ʈ ȣ��
    /// </summary>
    /// <param name="enemy">�߰��� ��</param>
    /// 
    public void AddEnemy(EnemyBase enemy) {
        if (EnemyList.Contains(enemy))
            return;

        EnemyList.Add(enemy);
        EnemyNumberAction?.Invoke(EnemyList.Count);
    }

    /// <summary>
    /// ����Ʈ���� �� ���� �� �̺�Ʈ ȣ��
    /// </summary>
    /// <param name="enemy">������ ��</param>
    public void RemoveEnemy(EnemyBase enemy) {
        if (!EnemyList.Contains(enemy))
            return;

        EnemyList.Remove(enemy);
        EnemyNumberAction?.Invoke(EnemyList.Count);
    }
}