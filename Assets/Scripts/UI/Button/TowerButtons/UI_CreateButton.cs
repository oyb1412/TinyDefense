using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// 타워 생성 버튼
/// </summary>
public class UI_CreateButton : UI_Button, IUI_TowerButton {
    //타워 생성 비용
    public int CreateCost { get; private set; }
    //현재 선택한 셀
    private Cell currentCell;
    //모든 자식 tmp 및 button관리 컴포넌트
    private TMPandButton tmpAndButton;
    //생성 타워 저장 오브젝트
    public TowerBase TowerObj { get; private set; }
    //타워 생성 가중치
    private int[] weights = new int[(int)Define.TowerType.Count];
    //타워 생성 이펙트
    private GameObject buildEffect;
    //타워 생성 비용 텍스트
    [SerializeField]private TextMeshProUGUI costTMP;
    //타워 생성시 생성할 골드 오브젝트
    private GameObject goldObject;
    //생성할 타워 프리펩 저장용 변수
    private GameObject[] towerList;

    /// <summary>
    /// 초기화
    /// 기본 가중치 적용
    /// 기본 생성비용 적용
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
    /// 유닛 그룹의 가중치 조정
    /// </summary>
    /// <param name="activeGroup">가중치를 높일 그룹</param>
    public void AdjustWeights(Define.TowerType? activeGroup) {
        int groupIndex = activeGroup.HasValue ? (int)activeGroup.Value / 3 : -1;

        for (int i = 0; i < weights.Length; i++) {
            if (i / 3 == groupIndex) {
                weights[i] = 2;
                print($"{i}번 타워 생성확률 50%");
            } else {
                weights[i] = 1;
                print($"{i}번 타워 생성확률 25%");
            }
        }

    }

    /// <summary>
    /// 타워 생성 비용 조정
    /// </summary>
    /// <param name="value"></param>
    public void SetCreateCost(int value = 0) {
        CreateCost = Define.TOWER_CREATE_COST - value;
        costTMP.text = string.Format(Define.MENT_CREATE_COST, CreateCost);
    }

    /// <summary>
    /// 버튼 활성화
    /// 셀 선택시 호출
    /// </summary>
    /// <param name="cell">선택할 셀</param>
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
    /// 버튼 비활성화
    /// </summary>
    public void DeActivation() {
        currentCell = null;
        seletable = false;
        tmpAndButton.DeActivation();
    }

    /// <summary>
    /// 가중치를 계산해 랜덤한 유닛 반환
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
    /// 보유 골드를 계산해 생성버튼 활성화 및 비활성화
    /// 이벤트로 주기적으로 확인
    /// </summary>
    /// <param name="gold">현재 골드</param>
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
    /// 타워 자동생성시, 호출
    /// </summary>
    /// <param name="cell"></param>
    public void CellCreate(Cell cell) {
        currentCell = cell;
        Select();
    }

    /// <summary>
    /// 타워 생성 어빌리티 선택시 호출
    /// 타워 자동생성 및 랜덤 업그레이드
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
    /// 버튼 선택시 랜덤 타워 생성
    /// 타워 스킬 보유시, 다음 타워 2레벨 업
    /// </summary>
    public override void Select() {
        //타워 생성 전 가중치 조절 스킬의 존재여부 확인
        //존재 시 가중치 조절
        foreach (var item in Managers.Ability.TowerAbilityList) {
            if (item is ITowerPreAbility)
                item.ExecuteSystemAbility(this);
        }

        //게임 초반 수녀 등장 억제
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


        //타워 생성 후 무료 업그레이드 스킬의 존재여부 확인
        //존재 시 무료 업그레이드
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