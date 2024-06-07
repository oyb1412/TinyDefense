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
    /// ��ų ���ӽð� �� �ڵ� ����(�κ�ũ�� ȣ��)
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
    /// ��ο� ���� �ڵ� �̵�
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