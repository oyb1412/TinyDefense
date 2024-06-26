using UnityEngine;

/// <summary>
/// 2차 충돌이 있는 발사체 관리 클래스
/// </summary>
public class ExplosionProjectile : ProjectileBase {
    /// <summary>
    /// 투사체 적중시 폭발 이펙트 생성
    /// </summary>
    protected override void CreateExplosion() {
        ExplosionProjectileEffect go = Managers.Resources.Activation(explosionEffect).GetComponent<ExplosionProjectileEffect>();
        go.Init(towerBase, attackData, transform.position);
    }
}