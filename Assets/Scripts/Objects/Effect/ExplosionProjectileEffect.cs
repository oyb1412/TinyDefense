using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 광역공격 이펙트 관리
/// </summary>
public abstract class ExplosionProjectileEffect : ProjectileEffectBase {
    //발사한 타워
    protected TowerBase towerBase;
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();
    /// <summary>
    /// 이펙트 생성 시 초기화
    /// </summary>
    /// <param name="towerBase">발사한 타워</param>
    /// <param name="attackData">발사한 타워의 데이터</param>
    /// <param name="pos">이펙트 생성 위치</param>
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
    /// 적과 충돌 시, 보유중인 디버프 적용
    /// </summary>
    /// <param name="enemy">디버프를 적용시킬 적</param>
    private void ExplosionAbility(EnemyBase enemy) {
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
    }
}