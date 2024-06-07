using UnityEngine;

/// <summary>
/// 2차 충돌이 없는 기본 발사체
/// </summary>
public class NormalProjectile : ProjectileBase {
    /// <summary>
    /// 적과 충돌 시
    /// 1.데미지 가함
    /// 2.디버프 적용
    /// 3.발사체 삭제
    /// </summary>
    /// <param name="enemy">충돌한 적</param>
    protected override void Collison(EnemyBase enemy) {
        enemy.EnemyStatus.SetHp(attackData.Damage, towerBase);
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
        base.Collison(enemy);
    }

    /// <summary>
    /// 투사체 적중시 폭발 이펙트 생성
    /// </summary>
    protected override void CreateExplosion() {
        NormalProjectileEffect go = Managers.Resources.Activation(explosionEffect).GetComponent<NormalProjectileEffect>();
        go.Init(towerBase, attackData, transform.position);
    }
}