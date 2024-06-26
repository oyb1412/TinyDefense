using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 시스터 타워 클래스
/// </summary>
public class Sister : TowerBase {
    private List<TowerBase> towerList;
    /// <summary>
    /// 초기화
    /// </summary>
    protected override void Init() {
        TowerType = Define.TowerType.Sister;
        TowerBundle = Define.TowerBundle.Mage;

        if(towerList == null)
            towerList = new List<TowerBase>();

        base.Init();
    }

    /// <summary>
    /// 투사체 발사 및 버프 적용
    /// </summary>
    public override void FireProjectile() {
        TowerBuff();

        if (Util.IsEnemyNull(TargetEnemy)) {
            StateMachine.ChangeState(Define.TowerState.Idle);
            return;
        }

        base.FireProjectile();
    }

    /// <summary>
    /// 랜덤한 주변 타워에게 랜덤한 버프 적용
    /// </summary>
    private void TowerBuff() {
        int ran = Random.Range(0, 2);
        towerList.Clear();

        foreach (var item in Managers.Tower.TowerList) {
            if (!item.gameObject.activeInHierarchy)
                continue;

            if (item.TowerCell == null)
                continue;

            if (item == this)
                continue;

            if(Vector2.Distance(TowerCell.transform.position, item.TowerCell.transform.position) > TowerStatus.AttackRange * Managers.Data.DefineData.TOWER_RANGE)
                continue;
            
            if(item.BuffManager.Buffs.Count > 0)
            {
                towerList.Add(item);
                continue;
            }
            
            if(ran == 0) {
                if(item.BuffManager.Buffs.Where(x => x.Type == Define.BuffType.AttackDamageUp) != null)
                {
                    item.BuffManager.AddBuff(new AttackDelayBuff(SetBuffValue(), SetBuffTime()), item);
                    return;
                }
                    
                item.BuffManager.AddBuff(new AttackDamageBuff(SetBuffValue(), SetBuffTime()), item);
                return;
            }
            else {
                if(item.BuffManager.Buffs.Where(x => x.Type == Define.BuffType.AttackDelayDown) != null)
                {
                    item.BuffManager.AddBuff(new AttackDamageBuff(SetBuffValue(), SetBuffTime()), item);
                    return;
                }

                item.BuffManager.AddBuff(new AttackDelayBuff(SetBuffValue(), SetBuffTime()), item);
                return;
            }
        }

        int ranTower = Random.Range(0,towerList.Count);
        if(ran == 0) {
                if(towerList[ranTower].BuffManager.Buffs.Where(x => x.Type == Define.BuffType.AttackDamageUp) != null)
                {
                    towerList[ranTower].BuffManager.AddBuff(new AttackDelayBuff(SetBuffValue(), SetBuffTime()), towerList[ranTower]);
                    return;
                }
                    
                towerList[ranTower].BuffManager.AddBuff(new AttackDamageBuff(SetBuffValue(), SetBuffTime()), towerList[ranTower]);
                return;
            }
            else {
                if(towerList[ranTower].BuffManager.Buffs.Where(x => x.Type == Define.BuffType.AttackDelayDown) != null)
                {
                    towerList[ranTower].BuffManager.AddBuff(new AttackDamageBuff(SetBuffValue(), SetBuffTime()), towerList[ranTower]);
                    return;
                }

                towerList[ranTower].BuffManager.AddBuff(new AttackDelayBuff(SetBuffValue(), SetBuffTime()), towerList[ranTower]);
                return;
            }
    }

    private float SetBuffValue()
    {
        return Managers.Tower.TowerData[(int)TowerType].BuffValue + Managers.Tower.TowerData[(int)TowerType].BuffValueUpValue * TowerLevel;
    }

    private float SetBuffTime()
    {
        return Managers.Tower.TowerData[(int)TowerType].BuffTime + Managers.Tower.TowerData[(int)TowerType].BuffTimeUpValue * TowerLevel;
    }
}