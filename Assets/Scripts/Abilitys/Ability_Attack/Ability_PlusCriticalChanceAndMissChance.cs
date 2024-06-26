using UnityEngine;

/// <summary>
/// 공격 어빌리티 관리 클래스
/// 높은 확률로 치명타 발생, 낮은 확률로 빗나감
/// </summary>
[Ability(Define.AbilityType.PlusCriticalChanceAndMissChance)]
public class Ability_PlusCriticalChanceAndMissChance : IAttackAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusCriticalChanceAndMissChance() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusCriticalChanceAndMissChance, Managers.Data.DefineData);
    }

    /// <summary>
    /// 어빌리티 추가 시 즉시 효과 적용이 필요한 경우 사용
    /// </summary>
    public void SetAbility() {
        
    }

    /// <summary>
    /// 공격 시 현재 어빌리티를 보유 시,
    /// 현재 어빌리티 적용
    /// </summary>
    /// <param name="towerBase">공격한 타워</param>
    /// <param name="attackData">공격 데이터</param>
    public void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData) {
        //확률계산으로 70%면 데미지 두배, 30%면 미스(데미지0)
        float ran = Random.Range(0f, 1f);
        if (Managers.Data.DefineData.ABILITY_DEFAULT_CRITICAL_CHANCE > ran) {
            attackData.Damage *= (1 + Managers.Data.DefineData.ABILITY_DEFAULT_CRITICAL_DAMAGE);
            attackData.IsCritical = true;
        } else {
            attackData.Damage = 0;
            attackData.IsMiss = true;
        }
    }
}