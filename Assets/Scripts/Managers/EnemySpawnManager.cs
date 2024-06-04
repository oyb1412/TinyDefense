using System.Collections;
using UnityEngine;

/// <summary>
/// �� ���� �Ŵ���
/// </summary>
public class EnemySpawnManager {
    //���� ������
    private WaitForSeconds wfs;
    private GameObject[] enemyList;
    /// <summary>
    /// ���� ������ �ʱ�ȭ ��
    /// ���� �ڷ�ƾ ����
    /// </summary>
    public void SpawnStart() {
        enemyList = new GameObject[Define.MAX_ENEMY_TYPE];
        wfs = new WaitForSeconds(Define.ENEMY_SPAWN_DELAY);

        for (int i = 0; i < Define.MAX_ENEMY_TYPE; i++) {
            string path = string.Format(Define.ENEMY_PREFAB_PATH, i);
            enemyList[i] = Resources.Load<GameObject>(path);
        }

        Managers.Game.CurrentGameLevelAction += ((level) => Managers.Instance.StartCoroutine(Co_Spawn()));
    }


    /// <summary>
    /// �ֳʹ� ���� �ڷ�ƾ
    /// ���� ������, ���� ������ ���� �ֳʹ� ����
    /// </summary>
    private IEnumerator Co_Spawn() {
        int spawnCount = 0;
        int ranCreateType = Random.Range(0, Define.MAX_ENEMY_TYPE);
        while (true) {
            GameObject go = Managers.Resources.Instantiate(enemyList[ranCreateType]);
            Managers.Enemy.AddEnemy(go.GetComponent<EnemyBase>());

            spawnCount++;

            if(spawnCount > Define.ROUND_SPAWN_COUNT)
                break;

            yield return wfs;
        }
    }
}