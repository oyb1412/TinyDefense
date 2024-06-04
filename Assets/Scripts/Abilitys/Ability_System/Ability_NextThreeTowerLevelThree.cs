
/// <summary>
/// �ý����� �����Ƽ
/// ���� �����ϴ� 3���� Ÿ�� ���� ����
/// </summary>
[Ability(Define.AbilityType.NextThreeTowerLevelThree)]
public class Ability_NextThreeTowerLevelThree : ITowerPostAbility {
    public int NextTowerUpgrade { get; private set; }
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue {get; private set;}

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_NextThreeTowerLevelThree() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.NextThreeTowerLevelThree);
    }

    /// <summary>
    /// ��ų�� ��ϵǸ� ��ȭ Ƚ�� �ʱ�ȭ
    /// </summary>
    public void SetAbility() {
        NextTowerUpgrade += 3;
    }

    /// <summary>
    /// ��ų ��Ͽ��� �� ��ų ����
    /// </summary>
    /// <param name="abilityManager">��ų �Ŵ���</param>
    public void DeleteAbility(AbilityManager abilityManager) {
        abilityManager.AbilityList.Remove(AbilityValue.Type);
        UI_AbilitysPanel.Instance.RemoveAbilityIcon(this);
    }

    /// <summary>
    /// Ÿ�� ���׷��̵� ��
    /// ��ȭ Ƚ�� ����� �����Ƽ ����
    /// </summary>
    /// <param name="tower"></param>
    public void UpgradeTower(TowerBase tower) {
        if (NextTowerUpgrade <= 0)
            return;

        tower.TowerLevelup();
        NextTowerUpgrade--;

        //if (NextTowerUpgrade <= 0) {
        //    DeleteAbility(Managers.Ability);
        //}
    }

    /// <summary>
    /// Ÿ�� ��ȭ Ƚ���� ����ϸ� Ÿ�� ��ȭ
    /// Ƚ���� ��� ���ϸ� ��ų ����
    /// </summary>
    public void ExecuteSystemAbility(UI_Button button) {
        if(button is UI_CreateButton createButton) {
            if(createButton.TowerObj)
                UpgradeTower(createButton.TowerObj.GetComponent<TowerBase>());
        }
    }
}