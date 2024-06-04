using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �������� ����Ʈ ����
/// </summary>
public abstract class ExplosionProjectileEffect : ProjectileEffectBase {
    //�߻��� Ÿ��
    protected TowerBase towerBase;
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();
    /// <summary>
    /// ����Ʈ ���� �� �ʱ�ȭ
    /// </summary>
    /// <param name="towerBase">�߻��� Ÿ��</param>
    /// <param name="attackData">�߻��� Ÿ���� ������</param>
    /// <param name="pos">����Ʈ ���� ��ġ</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        this.towerBase = towerBase;
        base.Init(towerBase, attackData, pos);

        var list = Managers.Enemy.enemyList.ToList();
        foreach (var item in list) {
            float distance = Vector2.Distance(transform.position, item.transform.position);
            if (distance <= Define.TOWER_EXPLOSION_RADIUS) {
                if (!enemyHash.Contains(item)) {
                    enemyHash.Add(item);
                    item.EnemyStatus.SetHp(attackData.Damage, towerBase);
                    ExplosionAbility(item);
                }
            }
        }
    }

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