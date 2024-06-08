using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 토네이도 스킬 객체 클래스
/// </summary>
public class Skill_Tornado : MonoBehaviour {
    //이동 경로(부모)
    private Transform movePath;
    //스킬 데이터
    private SkillData.Skill skillData;
    //이동 코루틴
    private Coroutine moveCoroutine;

    private HashSet<EnemyBase> containsEnemy;
    private void Start() {
        if(containsEnemy == null)
            containsEnemy = new HashSet<EnemyBase>(Managers.Data.DefineData.ENEMY_MAX_COUNT);

        transform.position = Managers.Data.DefineData.SKILL_TORNADO_DEFAULT_POSITION;
        movePath = GameObject.Find(Managers.Data.DefineData.ENEMY_MOVE_PATH).transform;
        skillData = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Co_Move());


        Invoke("Ivk_DestroyTornado", skillData.SkillTime);
    }

    /// <summary>
    /// 스킬 지속시간 후 자동 삭제(인보크로 호출)
    /// </summary>
    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        containsEnemy.Clear();
        Managers.Resources.Release(gameObject);
    }

    /// <summary>
    /// 경로에 맞춰 자동 이동
    /// </summary>
    private IEnumerator Co_Move() {
        int moveIndex = movePath.childCount - 1;
        Vector3 targetPosition = movePath.GetChild(moveIndex).position;
        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                Managers.Data.DefineData.SKILL_TORNADO_MOVESPEED * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, movePath.GetChild(moveIndex).position);

            if (distance <= Managers.Data.DefineData.PERMISSION_RANGE) {
                moveIndex--;

                if (moveIndex < 0)
                    moveIndex = movePath.childCount - 1;

                targetPosition = movePath.GetChild(moveIndex).position;
            }

            if(Managers.Enemy.EnemyList.Count > 0) {
                var enemyList = Managers.Enemy.GetEnemyList();
                Debug.Log(enemyList.Length);
                for (int i = enemyList.Length - 1; i >= 0; i--) {
                    if (Util.IsEnemyNull(enemyList[i]))
                        continue;

                    if (Vector2.Distance(transform.position, enemyList[i].transform.position) > Managers.Data.DefineData.SKILL_TORNADO_RANGE) {
                        if (containsEnemy.Contains(enemyList[i]))
                            containsEnemy.Remove(enemyList[i]);

                        continue;
                    }

                    if (containsEnemy.Contains(enemyList[i]))
                        continue;

                    containsEnemy.Add(enemyList[i]);
                    enemyList[i].DebuffManager.AddDebuff(new SlowDebuff(skillData.SkillValue, skillData.SkillTime), enemyList[i]);
                    enemyList[i].EnemyStatus.SetHp(skillData.SkillDamage);
                }
            }
            
            yield return null;
        }
    }
}