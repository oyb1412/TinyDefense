using TMPro;
using UnityEngine;

/// <summary>
/// Ÿ�� �Ǹ� ��ư Ŭ����
/// </summary>
public class UI_SellButton : UI_Button, IUI_TowerButton {
    //�Ǹ��� Ÿ�� ����
    public TowerBase Tower { get; private set; }
    //Ÿ�� �Ǹ� ��� ǥ�� tmp
    private TextMeshProUGUI sellRewardTMP;
    //��� �ڽ� tmp�� ��ư ���� ������Ʈ
    private TMPandButton tmpAndButton;
    //Ÿ�� �Ǹ� ����Ʈ
    private GameObject buildEffect;
    //Ÿ�� �ǸŽ� ��� ������
    private GameObject goldObject;

    public override void Init() {
        
    }

    protected override void Awake() {
        base.Awake();
        sellRewardTMP = GetComponentInChildren<TextMeshProUGUI>();
        tmpAndButton = GetComponent<TMPandButton>();
        goldObject = Resources.Load<GameObject>(Define.OBJECT_REWARD_PATH);
    }

    /// <summary>
    /// ��ư ���ý� Ÿ�� �Ǹ�
    /// </summary>
    public override void Select() {
        UI_GoldObject gold = Managers.Resources.Instantiate(goldObject).GetComponent<UI_GoldObject>();
        gold.Init(Tower.TowerCell.transform.position, Tower.TowerStatus.SellReward, true);

        GameObject effect = Managers.Resources.Instantiate(buildEffect);
        effect.transform.position = Tower.TowerCell.transform.position;
        Managers.Game.CurrentGold += Tower.TowerStatus.SellReward;
        Tower.DeSelect();
        Managers.Tower.RemoveTower(Tower);
        Managers.Resources.Destroy(Tower.gameObject);
        Tower.TowerCell.SetTower(null);
        UI_TowerDescription.Instance.DeActivation();
    }

    /// <summary>
    /// ��ư ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        Tower = null;
        seletable = false;
    }

    /// <summary>
    /// ��ư Ȱ��ȭ
    /// </summary>
    /// <param name="cell">������ ��</param>
    public void Activation(Cell cell, GameObject buildEffect) {
        if (!cell.IsUse()) {
            sellRewardTMP.text = string.Format(Define.MENT_SELL_REWARD, 0);
            tmpAndButton.DeActivation();
            return;
        }

        tmpAndButton.Activation();
        seletable = true;

        Tower = cell.Tower;

        sellRewardTMP.text = string.Format(Define.MENT_SELL_REWARD, Tower.TowerStatus.SellReward);

        if (this.buildEffect == null)
            this.buildEffect = buildEffect;
    }
}