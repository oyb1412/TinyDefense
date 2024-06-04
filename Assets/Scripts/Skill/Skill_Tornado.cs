using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill_Tornado : SkillBase {
    //이동 경로(부모)
    private Transform movePath;
    private HashSet<EnemyBase> enteredEnemies = new HashSet<EnemyBase>();
    private SkillData.Skill skillValue;
    private void Start() {
        transform.position = Define.SKILL_TORNADO_DEFAULT_POSITION;
        movePath = GameObject.Find(Define.ENEMY_MOVE_PATH).transform;

        StartCoroutine(Co_Move());
        skillValue = Managers.Skill.GetSkillValue(Define.SkillType.Torando);

        Invoke("Ivk_DestroyTornado", skillValue.SkillTime);
    }

    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        Managers.Resources.Destroy(gameObject);
        enteredEnemies.Clear();
    }

    /// <summary>
    /// 경로에 맞춰 자동 이동
    /// </summary>
    private IEnumerator Co_Move() {
        int moveIndex = movePath.childCount - 1;
        Vector3 targetPosition = movePath.GetChild(moveIndex).position;
        
        while (true) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,
                Define.SKILL_TORNADO_MOVESPEED * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, movePath.GetChild(moveIndex).position);

            if (distance <= Define.PERMISSION_RANGE) {
                moveIndex--;

                if (moveIndex < 0)
                    moveIndex = movePath.childCount - 1;

                targetPosition = movePath.GetChild(moveIndex).position;
            }
            yield return null;
        }
    }

    private void Update() {
        foreach(var item in Managers.Enemy.enemyList) {
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance <= Define.SKILL_TORNADO_RANGE && !enteredEnemies.Contains(item)) {
                enteredEnemies.Add(item);
                item.DebuffManager.AddDebuff(new SlowDebuff(skillValue.SkillValue, skillValue.SkillTime), item);
                item.EnemyStatus.SetHp(skillValue.SkillDamage);
            }
            else if(distance > Define.SKILL_TORNADO_RANGE && enteredEnemies.Contains(item)) {
                enteredEnemies.Remove(item);
            }
        }
    }
}