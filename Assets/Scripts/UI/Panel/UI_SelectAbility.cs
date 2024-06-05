using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 어빌리티 선택 판넬
/// </summary>
public class UI_SelectAbility : MonoBehaviour {
    //어빌리티 목록
    private UI_AbilityButton[] abilityButtons;
    //어빌리티 판넬
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
        //스킬들이 모두 다르고, 모두 배우지 않은 스킬이 선택될때까지 반복
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
    /// 어빌리티 선택 활성화 및 비활성화
    /// </summary>
    /// <param name="trigger">활성화 여부</param>
    public void Activation() {
        if (!Managers.Game.CanGetAbility()) 
          return;

        abilitysPanel.SetActive(true);

        var conAbility = Managers.Ability.AbilityList;
      //스킬들이 모두 다르고, 모두 배우지 않은 스킬이 선택될때까지 반복
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