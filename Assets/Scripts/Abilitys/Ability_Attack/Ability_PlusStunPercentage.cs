using UnityEngine;

/// <summary>
/// ���� �����Ƽ ���� Ŭ����
/// ���ݽ� ���� Ȯ���� �� ����
/// </summary>
[Ability(Define.AbilityType.PlusStunPercentage)]
public class Ability_PlusStunPercentage : IAttackAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusStunPercentage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusStunPercentage, Managers.Data.DefineData);
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
        if (Managers.Data.DefineData.ABILITY_STUN_DEFAULT_CHANCE < ran)
            return;

        attackData.IsStun = true;
    }
}