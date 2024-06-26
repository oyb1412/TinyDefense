
/// <summary>
/// 공격 어빌리티 관리 클래스
/// 공격에 독 디버프 추가
/// </summary>
[Ability(Define.AbilityType.PlusPoisonDamage)]
public class Ability_PlusPoisonDamage : IAttackAbility {
    //어빌리티 정보
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// 어빌리티 타입을 바탕으로 초기화
    /// 타입, 이름, 설명, 아이콘 스프라이트
    /// </summary>
    public Ability_PlusPoisonDamage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusPoisonDamage, Managers.Data.DefineData);
    }

    /// <summary>
    /// 스킬 추가시, 생성되어있는 모든 타워에 스킬 추가
    /// </summary>
    public void SetAbility() {
        foreach(var item in Managers.Tower.TowerList) {
            item.SetDebuff(new PoisonDebuff(Managers.Data.DefineData.ABILITY_POISON_DEFAULT_DAMAGE * item.TowerStatus.AttackDamage,
            Managers.Data.DefineData.ABILITY_POISON_DEFAULT_TIME));
        }
    }

    /// <summary>
    /// 공격 시 현재 어빌리티를 보유 시,
    /// 현재 어빌리티 적용
    /// </summary>
    /// <param name="towerBase">공격한 타워</param>
    /// <param name="attackData">공격 데이터</param>
    public void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData) {
        towerBase.SetDebuff(new PoisonDebuff(Managers.Data.DefineData.ABILITY_POISON_DEFAULT_DAMAGE * towerBase.TowerStatus.AttackDamage,
            Managers.Data.DefineData.ABILITY_POISON_DEFAULT_TIME));
    }
}