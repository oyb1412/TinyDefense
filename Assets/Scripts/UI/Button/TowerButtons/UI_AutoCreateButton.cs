using UnityEngine;

public class UI_AutoCreateButton : UI_Button {
    private Cell[] cells;
    private bool isGetTower;
    [SerializeField]private UI_CreateButton createButton;
    public override void Init() {
        cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
    }

    public override void Select() {
        foreach (Cell cell in cells) {
            if(!cell.IsUse()) {
                if (createButton.CreateCost > Managers.Game.CurrentGold)
                    return;

                createButton.CallCreate(cell);
            }
        }
    }

    public void GetOneTower() {
        if (!transform.parent.gameObject.activeInHierarchy) {
            transform.parent.gameObject.SetActive(true);
            isGetTower = true;
        }

        foreach (Cell cell in cells) {
            if (!cell.IsUse()) {
                createButton.CreateAndRandomUpgrade(cell);
                break;
            }
        }

        if(isGetTower) {
            transform.parent.gameObject.SetActive(false);
            isGetTower = false;
        }
    }
}