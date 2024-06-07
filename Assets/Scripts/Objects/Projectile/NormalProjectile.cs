using UnityEngine;

/// <summary>
/// 2�� �浹�� ���� �⺻ �߻�ü
/// </summary>
public class NormalProjectile : ProjectileBase {
    /// <summary>
    /// ���� �浹 ��
    /// 1.������ ����
    /// 2.����� ����
    /// 3.�߻�ü ����
    /// </summary>
    /// <param name="enemy">�浹�� ��</param>
    protected override void Collison(EnemyBase enemy) {
        enemy.EnemyStatus.SetHp(attackData.Damage, towerBase);
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
        base.Collison(enemy);
    }

    /// <summary>
    /// ����ü ���߽� ���� ����Ʈ ����
    /// </summary>
    protected override void CreateExplosion() {
        NormalProjectileEffect go = Managers.Resources.Activation(explosionEffect).GetComponent<NormalProjectileEffect>();
        go.Init(towerBase, attackData, transform.position);
    }
}