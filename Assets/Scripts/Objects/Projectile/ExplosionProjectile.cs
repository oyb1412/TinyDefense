using UnityEngine;

/// <summary>
/// 2�� �浹�� �ִ� �߻�ü ���� Ŭ����
/// </summary>
public class ExplosionProjectile : ProjectileBase {
    /// <summary>
    /// ����ü ���߽� ���� ����Ʈ ����
    /// </summary>
    protected override void CreateExplosion() {
        ExplosionProjectileEffect go = Managers.Resources.Instantiate(explosionEffect).GetComponent<ExplosionProjectileEffect>();
        go.Init(towerBase, attackData, transform.position);
    }
}