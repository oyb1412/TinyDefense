using UnityEngine;

/// <summary>
/// 공격 어빌리티 관리 클래스
/// 공격시 낮은 확률로 적 기절
/// </summary>
[Ability(Define.AbilityType.PlusStunPercentage)]
public class Ability_PlusStunPercentage : IAttackAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusStunPercentage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusStunPercentage, Managers.Data.DefineData);
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
        float ran = Random.Range(0f, 1f);
        if (Managers.Data.DefineData.ABILITY_STUN_DEFAULT_CHANCE < ran)
            return;

        attackData.IsStun = true;
    }
}