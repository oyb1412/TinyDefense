using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 토네이도 스킬 객체 클래스
/// </summary>
public class Skill_Tornado : MonoBehaviour {
    //이동 경로(부모)
    private Transform movePath;
    //공격한 적 저장용 해시셋
    private HashSet<EnemyBase> enteredEnemies = new HashSet<EnemyBase>();
    //스킬 데이터
    private SkillData.Skill skillData;
    private void Start() {
        transform.position = Define.SKILL_TORNADO_DEFAULT_POSITION;
        movePath = GameObject.Find(Define.ENEMY_MOVE_PATH).transform;

        StartCoroutine(Co_Move());
        skillData = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);

        Invoke("Ivk_DestroyTornado", skillData.SkillTime);
    }

    /// <summary>
    /// 스킬 지속시간 후 자동 삭제(인보크로 호출)
    /// </summary>
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
        var enemys = Managers.Enemy.EnemyList;
        while (true) {
            for(int i = 0; i< enemys.Count; i++) {
                if (enemys[i] == null)
                    continue;

                float dir = Vector2.Distance(transform.position, enemys[i].transform.position);
                if (dir <= Define.SKILL_TORNADO_RANGE && !enteredEnemies.Contains(enemys[i])) {
                    enteredEnemies.Add(enemys[i]);
                    enemys[i].DebuffManager.AddDebuff(new SlowDebuff(skillData.SkillValue, skillData.SkillTime), enemys[i]);
                    enemys[i].EnemyStatus.SetHp(skillData.SkillDamage);
                } else if (dir > Define.SKILL_TORNADO_RANGE && enteredEnemies.Contains(enemys[i])) {
                    enteredEnemies.Remove(enemys[i]);
                }
            }

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
}