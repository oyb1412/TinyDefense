using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        var enemyList = Managers.Enemy.GetEnemyList();
        for (int i = enemyList.Length - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            float distance = Vector2.Distance(transform.position, enemyList[i].transform.position);

            //폭발 사거리 내의 적을 검출
            if (distance > Managers.Data.DefineData.TOWER_EXPLOSION_RADIUS)
                continue;

            //적이 아직 공격당하지 않았다면
            if (enemyHash.Contains(enemyList[i]))
                continue;

            //공격 및 재공격 불가
            enemyHash.Add(enemyList[i]);
            enemyList[i].EnemyStatus.SetHp(attackData.Damage, towerBase);
            //디버프 적용
            ExplosionAbility(enemyList[i]);
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