using UnityEngine.Events;

/// <summary>
/// �ý����� �����Ƽ
/// ������Ÿ�� Ÿ�� ����Ȯ�� ����
/// </summary>
[Ability(Define.AbilityType.PlusMageTowerPercentage)]
public class Ability_PlusMageTowerPercentage : ITowerPreAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusMageTowerPercentage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusMageTowerPercentage, Managers.Data.DefineData);
    }

    /// <summary>
    /// �����Ƽ �߰� �� ��� ȿ�� ������ �ʿ��� ��� ���
    /// </summary>
    public void SetAbility() {

    }

    /// <summary>
    /// ���� ��ų ���� ��, Ÿ�� ������ ����ġ �ο�
    /// </summary>
    public void ExecuteSystemAbility(UI_Button button) {
        if (button is UI_CreateButton createButton) {
            createButton.AdjustWeights(Define.TowerType.Firemage);
        }
    }
}