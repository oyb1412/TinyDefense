using System.Collections;
using UnityEngine;

/// <summary>
/// 적 생성 매니저
/// </summary>
public class EnemySpawnManager {
    //생성 딜레이
    private WaitForSeconds spawnDelay;
    private GameObject[] enemyList;

    /// <summary>
    /// 생성 딜레이 초기화 및
    /// 생성 코루틴 시작
    /// </summary>
    public void SpawnStart() {
        spawnDelay = new WaitForSeconds(Managers.Data.DefineData.ENEMY_SPAWN_DELAY);
        if(enemyList == null) {

            enemyList = new GameObject[Managers.Data.DefineData.MAX_ENEMY_TYPE];
            for (int i = 0; i < Managers.Data.DefineData.MAX_ENEMY_TYPE; i++) {
                string path = string.Format(Managers.Data.DefineData.ENEMY_PREFAB_PATH, i);
                enemyList[i] = Resources.Load<GameObject>(path);
            }
        }
        //게임 레벨이 변경 될 때마다 자동으로 적 스폰 코루틴을 실행
        Managers.Game.CurrentGameLevelAction += ((level) => Managers.Instance.StartCoroutine(Co_Spawn()));
    }


    /// <summary>
    /// 애너미 생성 코루틴
    /// 생성 딜레이, 게임 레벨에 따라 애너미 생성
    /// </summary>
    private IEnumerator Co_Spawn() {
        int spawnCount = 0;
        int ranCreateType = Random.Range(0, Managers.Data.DefineData.MAX_ENEMY_TYPE);
        while (true) {
            GameObject go = Managers.Resources.Activation(enemyList[ranCreateType]);
            Managers.Enemy.AddEnemy(go.GetComponent<EnemyBase>());

            spawnCount++;

            if(spawnCount > Managers.Data.DefineData.ROUND_SPAWN_COUNT) {
                Managers.Game.PortalAnimator.enabled = false;
                break;
            }

            yield return spawnDelay;
        }
    }
}