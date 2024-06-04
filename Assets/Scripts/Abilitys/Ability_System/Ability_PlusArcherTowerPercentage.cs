using UnityEngine.Events;

/// <summary>
/// �ý����� �����Ƽ
/// �ü�Ÿ�� Ÿ�� ����Ȯ�� ����
/// </summary>
[Ability(Define.AbilityType.PlusArcherTowerPercentage)]
public class Ability_PlusArcherTowerPercentage : ITowerPreAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusArcherTowerPercentage() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusArcherTowerPercentage);
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
            createButton.AdjustWeights(Define.TowerType.Hunter);
        }
    }
}