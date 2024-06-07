using UnityEngine;

/// <summary>
/// 광역공격이 없는 일반적인 투사체 충돌 이펙트
/// </summary>
public class NormalProjectileEffect : ProjectileEffectBase {

    /// <summary>
    /// 이펙트 생성 시 초기화
    /// </summary>
    /// <param name="towerBase">발사한 타워</param>
    /// <param name="attackData">공격 데이터</param>
    /// <param name="pos">이펙트 생성 위치</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        SoundManager.Instance.PlaySfx(Define.SFXType.HitProjectile);
        base.Init(towerBase, attackData, pos);
    }
}