/// <summary>
/// 적에게 적용되는 어빌리티
/// 적 이동속도 영구적 감소
/// </summary>
[Ability(Define.AbilityType.MinusEnemyMoveSpeed)]
public class Ability_MinusEnemyMoveSpeed : IEnemyAbility {
    //감소시킬 이동속도 %
    public float MoveSpeedValue { get; private set; }
    //어빌리티 정보
    public Define.AbilityValue AbilityValue{get;private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_MinusEnemyMoveSpeed() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.MinusEnemyMoveSpeed, Managers.Data.DefineData);
        MoveSpeedValue = Managers.Data.DefineData.ABILITY_ENEMY_MOVESPEED_MINUS;
    }

    /// <summary>
    /// 스킬 습득 시, 생성된 모든 적에게 스킬 적용
    /// </summary>
    public void SetAbility() {
        var enemyList = Managers.Enemy.GetEnemyArray();
        for (int i = enemyList.Length - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].EnemyStatus.SetMoveSpeed(this);
        }
    }

    /// <summary>
    /// 애너미 생성 시, 스킬을 습득한 상태면
    /// 생성된 애너미에게 스킬 적용
    /// </summary>
    /// <param name="enemyBase">스킬을 적용할 애너미</param>
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetMoveSpeed(this);
    }
}