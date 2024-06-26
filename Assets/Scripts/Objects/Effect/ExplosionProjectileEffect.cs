using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileEffect : ProjectileEffectBase {
   
    protected TowerBase towerBase;
    
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();
   
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        if (towerBase.TowerType == Define.TowerType.Icemage)
            SoundManager.Instance.PlaySfx(Define.SFXType.IceExplosion);
        else
            SoundManager.Instance.PlaySfx(Define.SFXType.FireExplosion);

        this.towerBase = towerBase;
        base.Init(towerBase, attackData, pos);

        var enemyList = Managers.Enemy.EnemyList;

        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            float distance = Vector2.Distance(transform.position, enemyList[i].transform.position);

            
            if (distance > Managers.Data.DefineData.TOWER_EXPLOSION_RADIUS)
                continue;

           
            if (enemyHash.Contains(enemyList[i]))
                continue;

           
            enemyHash.Add(enemyList[i]);
           

            ExplosionAbility(enemyList[i]);
            enemyList[i].EnemyStatus.SetHp(attackData.Damage, towerBase);
        }
    }

    
    public override void DestroyEvent() {
        enemyHash.Clear();
        base.DestroyEvent();
    }

   
    private void ExplosionAbility(EnemyBase enemy) {
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
    }
}