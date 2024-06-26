using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyManager {
    
    public List<EnemyBase> EnemyList { get; private set; }
    
    public Action<int> EnemyNumberAction;
    public EnemyData.EnemyStatusData EnemyData { get; private set; }

    public float CurrentLevelEnemyHp { get; private set; }
   
    public void Clear() {
        EnemyNumberAction = null;
        EnemyList = new List<EnemyBase>(Managers.Data.DefineData.ENEMY_MAX_COUNT);
    }

    public void Init() {
        EnemyData = new EnemyData.EnemyStatusData();
        EnemyData = Managers.Data.GameData.EnemyDatas.Enemys;

        Managers.Game.CurrentGameLevelAction += SetCurrentLevelEnemyHp;
    }


    private void SetCurrentLevelEnemyHp(int level) {
        if (level == 0) {
            CurrentLevelEnemyHp = Managers.Enemy.EnemyData.MaxHp;
        } else {
            CurrentLevelEnemyHp = Managers.Enemy.EnemyData.MaxHp * Mathf.Pow(1 + Managers.Enemy.EnemyData.MaxHpUpVolume, level);
        }
    }

    
    public void AddEnemy(EnemyBase enemy) {
        if (EnemyList.Contains(enemy))
            return;

        EnemyList.Add(enemy);
        EnemyNumberAction?.Invoke(EnemyList.Count);
    }

   
    public void RemoveEnemy(EnemyBase enemy) {

        if (EnemyList.Remove(enemy))
            EnemyNumberAction?.Invoke(EnemyList.Count);
    }
}