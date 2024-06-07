using TMPro;
using UnityEngine;

/// <summary>
/// 타워 판매 버튼 클래스
/// </summary>
public class UI_SellButton : UI_Button, IUI_TowerButton {
    //판매할 타워 저장
    public TowerBase Tower { get; private set; }
    //타워 판매 비용 표기 tmp
    [SerializeField]private TextMeshProUGUI sellRewardTMP;
    //모든 자식 tmp및 버튼 관리 컴포넌트
    private TMPandButton tmpAndButton;
    //타워 판매 이펙트
    private GameObject buildEffect;
    //타워 판매시 골드 프리펩
    private GameObject goldObject;

    public override void Init() {
        buttonSfxType = Define.SFXType.SelectTowerUIButton;
    }

    protected override void Awake() {
        base.Awake();
        tmpAndButton = GetComponent<TMPandButton>();
        if(buildEffect == null) 
            buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);

        if(goldObject == null)
            goldObject = Resources.Load<GameObject>(Define.OBJECT_REWARD_PATH);
    }

    /// <summary>
    /// 버튼 선택시 타워 판매
    /// </summary>
    public override void Select() {
        UI_GoldObject gold = Managers.Resources.Activation(goldObject).GetComponent<UI_GoldObject>();
        gold.Init(Tower.TowerCell.transform.position, Tower.TowerStatus.SellReward, true);

        GameObject effect = Managers.Resources.Activation(buildEffect);
        effect.transform.position = Tower.TowerCell.transform.position;
        Managers.Game.CurrentGold += Tower.TowerStatus.SellReward;
        Tower.DeSelect();
        Managers.Tower.RemoveTower(Tower);
        Managers.Resources.Release(Tower.gameObject);
        Tower.TowerCell.SetTower(null);
        UI_TowerDescription.Instance.DeActivation();
    }

    /// <summary>
    /// 버튼 비활성화
    /// </summary>
    public void DeActivation() {
        Tower = null;
        seletable = false;
    }

    /// <summary>
    /// 버튼 활성화
    /// </summary>
    /// <param name="cell">선택한 셀</param>
    public void Activation(Cell cell) {
        if (!cell.IsUse()) {
            sellRewardTMP.text = string.Format(Define.MENT_SELL_REWARD, 0);
            tmpAndButton.DeActivation();
            return;
        }

        tmpAndButton.Activation();
        seletable = true;

        Tower = cell.Tower;

        sellRewardTMP.text = string.Format(Define.MENT_SELL_REWARD, Tower.TowerStatus.SellReward);

       
    }
}