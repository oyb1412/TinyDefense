using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����Ƽ ���� �ǳ�
/// </summary>
public class UI_SelectAbility : MonoBehaviour {
    //�����Ƽ ���
    private UI_AbilityButton[] abilityButtons;
    //�����Ƽ �ǳ�
    [SerializeField]private GameObject abilitysPanel;

    private HashSet<Define.AbilityType> freeAbilityList;
    private void Start() {
        abilitysPanel.SetActive(true);
        abilityButtons = GetComponentsInChildren<UI_AbilityButton>();
        Managers.Game.CurrentGameLevelAction += ((level) => Activation());
        DeActivation();

        freeAbilityList = new HashSet<Define.AbilityType>
        {
            Define.AbilityType.GetGold,
            Define.AbilityType.GetTower,
            Define.AbilityType.NextThreeTowerLevelThree
        };
    }

    public void DeActivation() {
        abilitysPanel.SetActive(false);
        foreach (UI_AbilityButton button in abilityButtons)
            button.gameObject.SetActive(false);
    }

    public void ReActivation() {
        
        foreach (UI_AbilityButton button in abilityButtons)
            button.gameObject.SetActive(false);

        var conAbility = Managers.Ability.AbilityList;

        int limit = 0;
        //��ų���� ��� �ٸ���, ��� ����� ���� ��ų�� ���õɶ����� �ݺ�
        while (true) {
            limit++;

            if (limit > 100)
                break;

            int ran1 = Random.Range(0, abilityButtons.Length);
            int ran2 = Random.Range(0, abilityButtons.Length);
            int ran3 = Random.Range(0, abilityButtons.Length);

            if (ran1 == ran2 || ran1 == ran3 || ran2 == ran3)
                continue;

            if ((!freeAbilityList.Contains((Define.AbilityType)ran1) && conAbility.ContainsKey((Define.AbilityType)ran1)) ||
                (!freeAbilityList.Contains((Define.AbilityType)ran2) && conAbility.ContainsKey((Define.AbilityType)ran2)) ||
                (!freeAbilityList.Contains((Define.AbilityType)ran3) && conAbility.ContainsKey((Define.AbilityType)ran3)))
                continue;

            if (ran1 != (int)Define.AbilityType.NextThreeTowerLevelThree &&
                ran2 != (int)Define.AbilityType.NextThreeTowerLevelThree &&
                ran3 != (int)Define.AbilityType.NextThreeTowerLevelThree)
            if (conAbility.ContainsKey((Define.AbilityType)ran1) ||
                conAbility.ContainsKey((Define.AbilityType)ran2) ||
                conAbility.ContainsKey((Define.AbilityType)ran3))
                continue;

            if (conAbility.ContainsKey(Define.AbilityType.PlusSoilderTowerPercentage)) {
                if (ran1 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran1 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusMageTowerPercentage)
                    continue;
            } else if (conAbility.ContainsKey(Define.AbilityType.PlusArcherTowerPercentage)) {
                if (ran1 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                    ran1 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusSoilderTowerPercentage)
                    continue;
            } else if (conAbility.ContainsKey(Define.AbilityType.PlusMageTowerPercentage)) {
                if (ran1 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                    ran1 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                    ran2 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                    ran3 == (int)Define.AbilityType.PlusSoilderTowerPercentage)
                    continue;
            }

            abilityButtons[ran1].gameObject.SetActive(true);
            abilityButtons[ran2].gameObject.SetActive(true);
            abilityButtons[ran3].gameObject.SetActive(true);
            break;
        }
    }

    /// <summary>
    /// �����Ƽ ���� Ȱ��ȭ �� ��Ȱ��ȭ
    /// </summary>
    /// <param name="trigger">Ȱ��ȭ ����</param>
    public void Activation() {
        if (!Managers.Game.CanGetAbility()) 
          return;

        abilitysPanel.SetActive(true);

        var conAbility = Managers.Ability.AbilityList;
      //��ų���� ��� �ٸ���, ��� ����� ���� ��ų�� ���õɶ����� �ݺ�
      while (true) {
          int ran1 = Random.Range(0, abilityButtons.Length);
          int ran2 = Random.Range(0, abilityButtons.Length);
          int ran3 = Random.Range(0, abilityButtons.Length);
          if (ran1 != ran2 && ran1 != ran3 && ran2 != ran3) {
              if (!conAbility.ContainsKey((Define.AbilityType)ran1) &&
                  !conAbility.ContainsKey((Define.AbilityType)ran2) &&
                  !conAbility.ContainsKey((Define.AbilityType)ran3)) {

                  if (conAbility.ContainsKey(Define.AbilityType.PlusSoilderTowerPercentage)) {
                      if (ran1 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran1 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusMageTowerPercentage)
                          continue;
                  }
                  else if (conAbility.ContainsKey(Define.AbilityType.PlusArcherTowerPercentage)) {
                      if (ran1 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusMageTowerPercentage ||
                          ran1 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusSoilderTowerPercentage)
                          continue;
                  }
                  else if (conAbility.ContainsKey(Define.AbilityType.PlusMageTowerPercentage)) {
                      if (ran1 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusArcherTowerPercentage ||
                          ran1 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                          ran2 == (int)Define.AbilityType.PlusSoilderTowerPercentage ||
                          ran3 == (int)Define.AbilityType.PlusSoilderTowerPercentage)
                          continue;
                  }

                  abilityButtons[ran1].gameObject.SetActive(true);
                  abilityButtons[ran2].gameObject.SetActive(true);
                  abilityButtons[ran3].gameObject.SetActive(true);
                  break;
              }
          }
      }
    }
}