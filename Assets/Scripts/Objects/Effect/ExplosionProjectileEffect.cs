using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 광역공격 이펙트 관리
/// </summary>
public class ExplosionProjectileEffect : ProjectileEffectBase {
    //발사한 타워
    protected TowerBase towerBase;
    //단타공격을 위한 헤시셋
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();

    /// <summary>
    /// 이펙트 생성 시 초기화 및 공격
    /// </summary>
    /// <param name="towerBase">발사한 타워</param>
    /// <param name="attackData">발사한 타워의 데이터</param>
    /// <param name="pos">이펙트 생성 위치</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        if (towerBase.TowerType == Define.TowerType.Icemage)
            SoundManager.Instance.PlaySfx(Define.SFXType.IceExplosion);
        else
            SoundManager.Instance.PlaySfx(Define.SFXType.FireExplosion);

        this.towerBase = towerBase;
        base.Init(towerBase, attackData, pos);

        var enemy = Managers.Enemy.EnemyList.ToHashSet();
        foreach(var item in enemy) {
            if(Util.IsEnemyNull(item)) 
                continue;

            float distance = Vector2.Distance(transform.position, item.transform.position);
            //폭발 사거리 내의 적을 검출
            if (distance <= Define.TOWER_EXPLOSION_RADIUS) {
                //적이 아직 공격당하지 않았다면
                if (item != null && !enemyHash.Contains(item)) {
                    //공격 및 재공격 불가
                    enemyHash.Add(item);
                    item.EnemyStatus.SetHp(attackData.Damage, towerBase);
                    //디버프 적용
                    ExplosionAbility(item);
                }
            }
        }
    }

    /// <summary>
    /// 이펙트 제거(콜백으로 호출)
    /// </summary>
    public override void DestroyEvent() {
        enemyHash.Clear();
        base.DestroyEvent();
    }

    /// <summary>
    /// 적과 충돌 시, 보유중인 디버프 적용
    /// </summary>
    /// <param name="enemy">디버프를 적용시킬 적</param>
    private void ExplosionAbility(EnemyBase enemy) {
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
    }
}