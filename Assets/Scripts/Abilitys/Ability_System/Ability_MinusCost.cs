
/// <summary>
/// �ý����� �����Ƽ
/// Ÿ�� ���� �ڽ�Ʈ ���� ����
/// </summary>
[Ability(Define.AbilityType.MinusCost)]
public class Ability_MinusCost : ITowerInitAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_MinusCost()
    {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.MinusCost, Managers.Data.DefineData);
    }

    /// <summary>
    /// �����Ƽ �߰� �� ��� ȿ�� ������ �ʿ��� ��� ���
    /// </summary>
    public void SetAbility() {
        UI_TowerDescription.Instance.CreateButton.SetCreateCost(
            Managers.Data.DefineData.MINUS_TOWER_CREATE_COST);
    }

    /// <summary>
    /// �����Ƽ�� �������� ������ �ʿ��� ��� ���
    /// </summary>
    /// <param name="button"></param>
    public void ExecuteSystemAbility(UI_Button button) {
       
    }
}