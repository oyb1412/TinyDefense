[Ability(Define.AbilityType.PlusSellGold)]
/// <summary>
/// �ý����� �����Ƽ
/// Ÿ�� �Ǹ� ��� ���� ����
/// </summary>
public class Ability_PlusSellGold : ITowerPostAbility {
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusSellGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusSellGold);
    }

    /// <summary>
    /// �������� ��� Ÿ���� �Ǹź�� ����
    /// </summary>
    public void SetAbility() {
        var towerList = Managers.Tower.TowerList;
        foreach (var tower in towerList) {
            tower.TowerStatus.SetReward(Define.ABILITY_PLUS_TOWER_REWARD);
        }
    }

    /// <summary>
    /// ���Ӱ� ź���ϴ� Ÿ���� �Ǹź�� ����
    /// </summary>
    /// <param name="button"></param>
    public void ExecuteSystemAbility(UI_Button button) {
        if(button is UI_CreateButton createButton) {
            createButton.TowerObj.GetComponent<TowerBase>().TowerStatus.SetReward(Define.ABILITY_PLUS_TOWER_REWARD);
        }
    }
}