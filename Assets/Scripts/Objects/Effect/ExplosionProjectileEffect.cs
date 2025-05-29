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

        float radius = Managers.Data.DefineData.TOWER_EXPLOSION_RADIUS;
        var enemyList = Managers.Grid.GetEnemiesInRange(myTransform.position, radius);

        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
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