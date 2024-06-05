using UnityEngine;

/// <summary>
/// Ÿ�� �ڵ����� ��ư
/// </summary>
public class UI_AutoCreateButton : UI_Button {
    //���� �����ϴ� ��� ��
    private Cell[] cells;
    //Ÿ�� ���� �����Ƽ ���� ��, Ÿ�� ���� ����
    private bool isGetTower;
    //Ÿ�� ���� ��ư
    [SerializeField]private UI_CreateButton createButton;
    public override void Init() {
        cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
    }

    /// <summary>
    /// Ÿ�� �ڵ����� ��ư ���ý�
    /// </summary>
    public override void Select() {
        foreach (Cell cell in cells) {
            if(!cell.IsUse()) {
                //��尡 �����ϸ� ���
                if (createButton.CreateCost > Managers.Game.CurrentGold)
                    return;

                //Ÿ�� �ڵ�����
                createButton.CellCreate(cell);
            }
        }
    }

    /// <summary>
    /// �����Ƽ(Ÿ�� ȹ��)���ý� ȣ��
    /// </summary>
    public void GetOneTower() {
        //���� ��ü�� ��Ȱ��ȭ �����Ͻ� �Ͻ��� Ȱ��ȭ
        if (!transform.parent.gameObject.activeInHierarchy) {
            transform.parent.gameObject.SetActive(true);
            isGetTower = true;
        }

        //��������� �ʴ� ���� ã��
        //ã���� �� ���� Ÿ�� ���� �� ���׷��̵�
        foreach (Cell cell in cells) {
            if (!cell.IsUse()) {
                createButton.CreateAndRandomUpgrade(cell);
                break;
            }
        }

        //��ü�� �ٽ� ��Ȱ��ȭ
        if(isGetTower) {
            transform.parent.gameObject.SetActive(false);
            isGetTower = false;
        }
    }
}