using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ����̵� ��ų ��ü Ŭ����
/// </summary>
public class Skill_Tornado : MonoBehaviour {
    //�̵� ���(�θ�)
    private Transform movePath;
    //������ �� ����� �ؽü�
    private HashSet<EnemyBase> enteredEnemies = new HashSet<EnemyBase>();
    //��ų ������
    private SkillData.Skill skillData;
    private void Start() {
        transform.position = Define.SKILL_TORNADO_DEFAULT_POSITION;
        movePath = GameObject.Find(Define.ENEMY_MOVE_PATH).transform;

        StartCoroutine(Co_Move());
        skillData = Managers.Skill.GetSkillValue(Define.SkillType.Tornado);

        Invoke("Ivk_DestroyTornado", skillData.SkillTime);
    }

    /// <summary>
    /// ��ų ���ӽð� �� �ڵ� ����(�κ�ũ�� ȣ��)
    /// </summary>
    private void Ivk_DestroyTornado() {
        StopAllCoroutines();
        Managers.Resources.Destroy(gameObject);
        enteredEnemies.Clear();
    }

    /// <summary>
    /// ��ο� ���� �ڵ� �̵�
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