/// <summary>
/// 적에게 적용되는 어빌리티 관리 인터페이스
/// </summary>
public interface IEnemyAbility : IAbility {
    //지속적인 어빌리티 적용
    void ExecuteEnemyAbility(EnemyBase enemyBase);
}