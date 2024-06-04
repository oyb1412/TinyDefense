using UnityEngine;

/// <summary>
/// ���� �����Ƽ ���� Ŭ����
/// ���ݽ� ���� Ȯ���� ����ü �߰�
/// </summary>
[Ability(Define.AbilityType.PlusProjectile)]
public class Ability_PlusProjectile : IAttackAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusProjectile() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusProjectile);
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
        float ran = Random.Range(0f, 1f);
        if (Define.ABILITY_PROJECTILE_DEFAULT_CHANCE < ran)
            return;

        towerBase.Fire(attackData, Define.ABILITY_PROJECTILE_DEFAULT_DELAY);
    }

}