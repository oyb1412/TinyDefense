/// <summary>
/// ��� ���� �����Ƽ ���� �������̽�
/// </summary>
public interface IAttackAbility : IAbility {
    //�������� �����Ƽ ����
    void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData);
}