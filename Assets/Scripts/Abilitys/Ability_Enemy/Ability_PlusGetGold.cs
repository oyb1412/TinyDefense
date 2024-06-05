
/// <summary>
/// 적에게 적용되는 어빌리티
/// 적 드랍 골드 영구 증가
/// </summary>
[Ability(Define.AbilityType.PlusGetGold)]
public class Ability_PlusGetGold : IEnemyAbility {
    //증가시킬 골드량
    public float PlusGold { get; private set; }
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusGetGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusGetGold);
        PlusGold = Define.ABILITY_PLUS_ENEMY_REWARD;
    }

    /// <summary>
    /// 생성되있는 모든 애너미 리워드 변경
    /// </summary>
    public void SetAbility() {
        var enemys = Managers.Enemy.EnemyList;
        for (int i = 0; i < enemys.Count; i++) {
            if (enemys[i] == null)
                continue;

            enemys[i].EnemyStatus.SetReward(this);
        }
    }

    /// <summary>
    /// 애너미 생성 시, 스킬을 습득한 상태면
    /// 생성된 애너미에게 스킬 적용
    /// </summary>
    /// <param name="enemyBase">스킬을 적용할 애너미</param>
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetReward(this);
    }
}