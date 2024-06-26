
[Ability(Define.AbilityType.MinusEnemyMoveSpeed)]
public class Ability_MinusEnemyMoveSpeed : IEnemyAbility {
    
    public float MoveSpeedValue { get; private set; }
   
    public Define.AbilityValue AbilityValue{get;private set; }

    
    public Ability_MinusEnemyMoveSpeed() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.MinusEnemyMoveSpeed, Managers.Data.DefineData);
        MoveSpeedValue = Managers.Data.DefineData.ABILITY_ENEMY_MOVESPEED_MINUS;
    }

    
    public void SetAbility() {
        var enemyList = Managers.Enemy.EnemyList;
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].EnemyStatus.SetMoveSpeed(this);
        }
    }

    
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetMoveSpeed(this);
    }
}