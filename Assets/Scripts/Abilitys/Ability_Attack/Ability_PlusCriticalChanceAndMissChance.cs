using UnityEngine;

/// <summary>
/// ���� �����Ƽ ���� Ŭ����
/// ���� Ȯ���� ġ��Ÿ �߻�, ���� Ȯ���� ������
/// </summary>
[Ability(Define.AbilityType.PlusCriticalChanceAndMissChance)]
public class Ability_PlusCriticalChanceAndMissChance : IAttackAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusCriticalChanceAndMissChance() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusCriticalChanceAndMissChance, Managers.Data.DefineData);
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
        //Ȯ��������� 70%�� ������ �ι�, 30%�� �̽�(������0)
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