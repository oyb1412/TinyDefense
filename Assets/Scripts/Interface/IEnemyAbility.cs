/// <summary>
/// ������ ����Ǵ� �����Ƽ ���� �������̽�
/// </summary>
public interface IEnemyAbility : IAbility {
    //�������� �����Ƽ ����
    void ExecuteEnemyAbility(EnemyBase enemyBase);
}