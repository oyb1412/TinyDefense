
/// <summary>
/// ���� �����Ƽ ���� Ŭ����
/// �� ���ڿ� ����� ���ݷ� ����
/// </summary>
[Ability(Define.AbilityType.PlusAttackDamageUpToManyEnemy)]
public class Ability_PlusAttackDamageUpToManyEnemy : IAttackAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue {get;private set;}

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusAttackDamageUpToManyEnemy() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusAttackDamageUpToManyEnemy);
    }

    /// <summary>
    /// �����Ƽ �߰� �� ��� ȿ�� ������ �ʿ��� ��� ���
    /// </summary>
    public void SetAbility() {
        
    }

    /// <summary>
    /// ���� �� ���� �����Ƽ�� ���� ��,
    /// ���� �����Ƽ ����
    /// </summary>
    /// <param name="towerBase">������ Ÿ��</param>
    /// <param name="attackData">���� ������</param>
    public void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData) {
        //�� ���ڿ� ����� ���ݷ� ����
        attackData.Damage *= (1 + Define.ABILITY_MANYENEMY_DAMAGE_UP * Managers.Enemy.EnemyList.Count);
    }
}