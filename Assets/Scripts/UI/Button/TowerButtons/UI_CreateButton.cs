using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Ÿ�� ���� ��ư
/// </summary>
public class UI_CreateButton : UI_Button, IUI_TowerButton {
    //Ÿ�� ���� ���
    public int CreateCost { get; private set; }
    //���� ������ ��
    private Cell currentCell;
    //��� �ڽ� tmp �� button���� ������Ʈ
    private TMPandButton tmpAndButton;
    //���� Ÿ�� ���� ������Ʈ
    public TowerBase TowerObj { get; private set; }
    //Ÿ�� ���� ����ġ
    private int[] weights = new int[(int)Define.TowerType.Count];
    //Ÿ�� ���� ����Ʈ
    private GameObject buildEffect;
    //Ÿ�� ���� ��� �ؽ�Ʈ
    [SerializeField]private TextMeshProUGUI costTMP;
    //Ÿ�� ������ ������ ��� ������Ʈ
    private GameObject goldObject;
    //������ Ÿ�� ������ ����� ����
    private GameObject[] towerList;

    /// <summary>
    /// �ʱ�ȭ
    /// �⺻ ����ġ ����
    /// �⺻ ������� ����
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectTowerUIButton;
        Managers.Game.CurrentGoldAction += CaculationGold;
        for (int i = 0; i < weights.Length; i++) {
            weights[i] = 1;
        }

        if(buildEffect == null)
            buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);

        if(goldObject == null)
            goldObject = Resources.Load<GameObject>(Define.OBJECT_REWARD_PATH);

        if(towerList == null)
            towerList = new GameObject[(int)Define.TowerType.Count];

        for(int i = 0; i< towerList.Length; i++) {
            towerList[i] = Resources.Load<GameObject>(Define.TOWER_PREFAB_PATH[i]);
        }

        SetCreateCost();
    }

    protected override void Awake() {
        base.Awake();
        tmpAndButton = GetComponent<TMPandButton>();
    }

    /// <summary>
    /// ���� �׷��� ����ġ ����
    /// </summary>
    /// <param name="activeGroup">����ġ�� ���� �׷�</param>
    public void AdjustWeights(Define.TowerType? activeGroup) {
        int groupIndex = activeGroup.HasValue ? (int)activeGroup.Value / 3 : -1;

        for (int i = 0; i < weights.Length; i++) {
            if (i / 3 == groupIndex) {
                weights[i] = 2;
                print($"{i}�� Ÿ�� ����Ȯ�� 50%");
            } else {
                weights[i] = 1;
                print($"{i}�� Ÿ�� ����Ȯ�� 25%");
            }
        }

    }

    /// <summary>
    /// Ÿ�� ���� ��� ����
    /// </summary>
    /// <param name="value"></param>
    public void SetCreateCost(int value = 0) {
        CreateCost = Define.TOWER_CREATE_COST - value;
        costTMP.text = string.Format(Define.MENT_CREATE_COST, CreateCost);
    }

    /// <summary>
    /// ��ư Ȱ��ȭ
    /// �� ���ý� ȣ��
    /// </summary>
    /// <param name="cell">������ ��</param>
    public void Activation(Cell cell) {
        currentCell = cell;
        if (cell.IsUse()) {
            tmpAndButton.DeActivation();
            return;
        }
        tmpAndButton.Activation();
        seletable = true;
        CaculationGold(Managers.Game.CurrentGold);
    }

    /// <summary>
    /// ��ư ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        currentCell = null;
        seletable = false;
        tmpAndButton.DeActivation();
    }

    /// <summary>
    /// ����ġ�� ����� ������ ���� ��ȯ
    /// </summary>
    Define.TowerType SummonUnit() {
        int totalWeight = weights.Sum();
        int randomNumber = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < weights.Length; i++) {
            cumulativeWeight += weights[i];
            if (randomNumber < cumulativeWeight) {
                return (Define.TowerType)i;
            }
        }

        return Define.TowerType.Knight; 
    }

    /// <summary>
    /// ���� ��带 ����� ������ư Ȱ��ȭ �� ��Ȱ��ȭ
    /// �̺�Ʈ�� �ֱ������� Ȯ��
    /// </summary>
    /// <param name="gold">���� ���</param>
    private void CaculationGold(int gold) {
        if(currentCell != null &&  currentCell.IsUse()) {
            tmpAndButton.DeActivation();
            return;
        }

        bool trigger = (gold < CreateCost) ? false : true;
        button.interactable = trigger;
        seletable = trigger;
        if (trigger)
            tmpAndButton.Activation();
        else
            tmpAndButton.DeActivation();
    }

    /// <summary>
    /// Ÿ�� �ڵ�������, ȣ��
    /// </summary>
    /// <param name="cell"></param>
    public void CellCreate(Cell cell) {
        currentCell = cell;
        Select();
    }

    /// <summary>
    /// Ÿ�� ���� �����Ƽ ���ý� ȣ��
    /// Ÿ�� �ڵ����� �� ���� ���׷��̵�
    /// </summary>
    /// <param name="cell"></param>
    public void CreateAndRandomUpgrade(Cell cell) {
        currentCell = cell;
        foreach (var item in Managers.Ability.TowerAbilityList) {
            if(item is ITowerPreAbility)
                item.ExecuteSystemAbility(this);
        }

        TowerObj = Managers.Resources.Activation(towerList[(int)SummonUnit()]
                ).GetComponent<TowerBase>();

        int ranUpgrade = Random.Range(1, 3);

        for(int i = 0; i< ranUpgrade; i++) {
            TowerObj.TowerLevelup();
        }

        GameObject go = Managers.Resources.Activation(buildEffect);
        go.transform.position = currentCell.transform.position;

        UI_GoldObject gold = Managers.Resources.Activation(goldObject).GetComponent<UI_GoldObject>();
        gold.Init(currentCell.transform.position, -CreateCost, false);

        currentCell.SetTower(TowerObj);
        Managers.Tower.AddTower(TowerObj);

        TowerObj = null;
    }

    /// <summary>
    /// ��ư ���ý� ���� Ÿ�� ����
    /// Ÿ�� ��ų ������, ���� Ÿ�� 2���� ��
    /// </summary>
    public override void Select() {
        //Ÿ�� ���� �� ����ġ ���� ��ų�� ���翩�� Ȯ��
        //���� �� ����ġ ����
        foreach (var item in Managers.Ability.TowerAbilityList) {
            if (item is ITowerPreAbility)
                item.ExecuteSystemAbility(this);
        }

        //���� �ʹ� ���� ���� ����
        if(Managers.Game.CurrentGameLevel < 2) {
            var type = SummonUnit();
            if(type == Define.TowerType.Sister) {
                var ran = Random.Range(0, (int)Define.TowerType.Sister);
                TowerObj = Managers.Resources.Activation(towerList[ran]
                    ).GetComponent<TowerBase>();
            }
            else {
                TowerObj = Managers.Resources.Activation(towerList[(int)type]
                    ).GetComponent<TowerBase>();
            }
        }
        else {
            TowerObj = Managers.Resources.Activation(towerList[(int)SummonUnit()]
                    ).GetComponent<TowerBase>();
        }


        //Ÿ�� ���� �� ���� ���׷��̵� ��ų�� ���翩�� Ȯ��
        //���� �� ���� ���׷��̵�
        foreach (var item in Managers.Ability.TowerAbilityList) {
            if (item is ITowerPostAbility)
                item.ExecuteSystemAbility(this);
        }

        GameObject go = Managers.Resources.Activation(buildEffect);
        go.transform.position = currentCell.transform.position;

        UI_GoldObject gold = Managers.Resources.Activation(goldObject).GetComponent<UI_GoldObject>();
        gold.Init(currentCell.transform.position, -CreateCost, false);

        currentCell.SetTower(TowerObj);
        Managers.Game.CurrentGold -= CreateCost;
        Managers.Tower.AddTower(TowerObj);

        TowerObj = null;
    }
}