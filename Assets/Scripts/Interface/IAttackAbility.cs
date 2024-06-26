/// <summary>
/// 모든 공격 어빌리티 관리 인터페이스
/// </summary>
public interface IAttackAbility : IAbility {
    //지속적인 어빌리티 적용
    void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData);
}