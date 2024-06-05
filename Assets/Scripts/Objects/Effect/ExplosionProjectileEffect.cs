using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �������� ����Ʈ ����
/// </summary>
public class ExplosionProjectileEffect : ProjectileEffectBase {
    //�߻��� Ÿ��
    protected TowerBase towerBase;
    //��Ÿ������ ���� ��ü�
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();

    /// <summary>
    /// ����Ʈ ���� �� �ʱ�ȭ �� ����
    /// </summary>
    /// <param name="towerBase">�߻��� Ÿ��</param>
    /// <param name="attackData">�߻��� Ÿ���� ������</param>
    /// <param name="pos">����Ʈ ���� ��ġ</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        this.towerBase = towerBase;
        base.Init(towerBase, attackData, pos);

        var enemy = Managers.Enemy.EnemyList;
        for(int i = 0; i< enemy.Count; i++) {
            if (enemy[i] == null)
                continue;

            float distance = Vector2.Distance(transform.position, enemy[i].transform.position);
            //���� ��Ÿ� ���� ���� ����
            if (distance <= Define.TOWER_EXPLOSION_RADIUS) {
                //���� ���� ���ݴ����� �ʾҴٸ�
                if (!enemyHash.Contains(enemy[i])) {
                    //���� �� ����� �Ұ�
                    enemyHash.Add(enemy[i]);
                    enemy[i].EnemyStatus.SetHp(attackData.Damage, towerBase);
                    //����� ����
                    ExplosionAbility(enemy[i]);
                }
            }
        }
        
    }

    /// <summary>
    /// ����Ʈ ����(�ݹ����� ȣ��)
    /// </summary>
    public override void DestroyEvent() {
        enemyHash.Clear();
        base.DestroyEvent();
    }

    /// <summary>
    /// ���� �浹 ��, �������� ����� ����
    /// </summary>
    /// <param name="enemy">������� �����ų ��</param>
    private void ExplosionAbility(EnemyBase enemy) {
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
    }
}