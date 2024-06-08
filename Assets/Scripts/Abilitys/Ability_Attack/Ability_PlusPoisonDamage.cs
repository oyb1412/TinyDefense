
/// <summary>
/// ���� �����Ƽ ���� Ŭ����
/// ���ݿ� �� ����� �߰�
/// </summary>
[Ability(Define.AbilityType.PlusPoisonDamage)]
public class Ability_PlusPoisonDamage : IAttackAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusPoisonDamage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusPoisonDamage, Managers.Data.DefineData);
    }

    /// <summary>
    /// ��ų �߰���, �����Ǿ��ִ� ��� Ÿ���� ��ų �߰�
    /// </summary>
    public void SetAbility() {
        foreach(var item in Managers.Tower.TowerList) {
            item.SetDebuff(new PoisonDebuff(Managers.Data.DefineData.ABILITY_POISON_DEFAULT_DAMAGE * item.TowerStatus.AttackDamage,
            Managers.Data.DefineData.ABILITY_POISON_DEFAULT_TIME));
        }
    }

    /// <summary>
    /// ���� �� ���� �����Ƽ�� ���� ��,
    /// ���� �����Ƽ ����
    /// </summary>
    /// <param name="towerBase">������ Ÿ��</param>
    /// <param name="attackData">���� ������</param>
    public void ExecuteAtteckAbility(TowerBase towerBase, ref TowerBase.AttackData attackData) {
        towerBase.SetDebuff(new PoisonDebuff(Managers.Data.DefineData.ABILITY_POISON_DEFAULT_DAMAGE * towerBase.TowerStatus.AttackDamage,
            Managers.Data.DefineData.ABILITY_POISON_DEFAULT_TIME));
    }
}