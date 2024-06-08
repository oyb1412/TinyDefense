using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ����̵� ��ų ��ü Ŭ����
/// </summary>
public class Skill_Tornado : MonoBehaviour {
    //�̵� ���(�θ�)
    private Transform movePath;
    //��ų ������
    private SkillData.Skill skillData;
    //�̵� �ڷ�ƾ
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
    /// ��ų ���ӽð� �� �ڵ� ����(�κ�ũ�� ȣ��)
    /// </summary>
    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        containsEnemy.Clear();
        Managers.Resources.Release(gameObject);
    }

    /// <summary>
    /// ��ο� ���� �ڵ� �̵�
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