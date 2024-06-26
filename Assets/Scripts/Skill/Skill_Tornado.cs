using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_Tornado : MonoBehaviour {
    
    private Transform movePath;
   
    private SkillData.Skill skillData;
   
    private Coroutine moveCoroutine;
    private Coroutine attackCoroutine;
    private WaitForSeconds attackDelay;

    private HashSet<EnemyBase> containsEnemy;
    private void Start() {
        if(containsEnemy == null)
            containsEnemy = new HashSet<EnemyBase>(Managers.Data.DefineData.ENEMY_MAX_COUNT);

        if(movePath == null)
            movePath = GameObject.Find(Managers.Data.DefineData.ENEMY_MOVE_PATH).transform;

        if(attackDelay == null)
            attackDelay = new WaitForSeconds(0.1f);

        transform.position = Managers.Data.DefineData.SKILL_TORNADO_DEFAULT_POSITION;

        skillData = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Co_Move());

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(Co_Attack());

        Invoke("Ivk_DestroyTornado", skillData.SkillTime);
    }

  
    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        containsEnemy.Clear();
        Managers.Resources.Release(gameObject);
    }

   /// <summary>
   /// 주변 적 공격 코루틴
   /// </summary>
   /// <returns></returns>
    private IEnumerator Co_Attack() {
        while (true) {
            if (Managers.Enemy.EnemyList.Count > 0) {
                var enemyList = Managers.Enemy.EnemyList;
                for (int i = enemyList.Count - 1; i >= 0; i--) {
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
                    float enemyMaxHp = enemyList[i].EnemyStatus.MaxHp;
                    float skillDamage = Mathf.Min(enemyList[i].EnemyStatus.CurrentHp * skillData.SkillDamage, enemyMaxHp * 0.5f);
                    enemyList[i].EnemyStatus.SetHp(skillDamage);
                }
            }

            yield return attackDelay;
        }
    }

   
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
            yield return null;
        }
    }
}