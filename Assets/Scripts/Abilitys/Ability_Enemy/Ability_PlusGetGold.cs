
using System.Linq;


[Ability(Define.AbilityType.PlusGetGold)]
public class Ability_PlusGetGold : IEnemyAbility {
    
    public float PlusGold { get; private set; }
    
    public Define.AbilityValue AbilityValue { get; private set; }

    
    public Ability_PlusGetGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusGetGold, Managers.Data.DefineData);
        PlusGold = Managers.Data.DefineData.ABILITY_PLUS_ENEMY_REWARD;
    }

   
    public void SetAbility() {
        var enemyList = Managers.Enemy.EnemyList;
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].EnemyStatus.SetReward(this);
        }
    }

    
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetReward(this);
    }
}