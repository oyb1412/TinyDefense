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
    private void Start() {
        transform.position = Define.SKILL_TORNADO_DEFAULT_POSITION;
        movePath = GameObject.Find(Define.ENEMY_MOVE_PATH).transform;

        if(moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Co_Move());

        skillData = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);

        Invoke("Ivk_DestroyTornado", skillData.SkillTime);
    }

    /// <summary>
    /// 스킬 지속시간 후 자동 삭제(인보크로 호출)
    /// </summary>
    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        Managers.Resources.Release(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c) {
        if (!c.CompareTag(Define.TAG_ENEMY))
            return;

        EnemyBase enemy = c.GetComponent<EnemyBase>();

        if (Util.IsEnemyNull(enemy))
            return;

        enemy.DebuffManager.AddDebuff(new SlowDebuff(skillData.SkillValue, skillData.SkillTime), enemy);
        enemy.EnemyStatus.SetHp(skillData.SkillDamage);
    }

    /// <summary>
    /// 경로에 맞춰 자동 이동
    /// </summary>
    private IEnumerator Co_Move() {
        int moveIndex = movePath.childCount - 1;
        Vector3 targetPosition = movePath.GetChild(moveIndex).position;
        var enemys = Managers.Enemy.EnemyList.ToHashSet();
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
}