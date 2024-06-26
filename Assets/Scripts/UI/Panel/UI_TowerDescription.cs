using UnityEngine;

/// <summary>
/// 타워 정보 및 설명,
/// 타워 생성, 판매, 업그레이드 관리 캔버스
/// </summary>
public class UI_TowerDescription : MonoBehaviour 
{
    public static UI_TowerDescription Instance;

    //오브젝트 판넬
    [SerializeField] private GameObject DescriptionPanel;
    //자동 판넬
    [SerializeField] private UI_AutoPanel autoPanel;
    //타워 생성 버튼
    public UI_CreateButton CreateButton { get; private set; }
    //타워 생성,판매,업그레이드 버튼
    private UI_TowerBuild towerBuild;
    //타워 정보 판넬
    private UI_TowerInformation towerInformation;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);
    }

    private void Start() {
        DescriptionPanel.SetActive(true);
        CreateButton = GetComponentInChildren<UI_CreateButton>();
        towerBuild = GetComponentInChildren<UI_TowerBuild>();
        towerInformation = GetComponentInChildren<UI_TowerInformation>();
        DescriptionPanel.SetActive(false);
    }

    /// <summary>
    /// 판넬 활성화
    /// </summary>
    /// <param name="cell">선택한 셀</param>
    public void Activation(Cell cell) {
        DescriptionPanel.SetActive(true);
        autoPanel.DeActivation();
        towerBuild.Activation(cell);
        towerInformation.Activation(cell);
    }

    /// <summary>
    /// 판넬 비활성화
    /// </summary>
    public void DeActivation() {
        towerBuild.DeActivation(() => DescriptionPanel.SetActive(false));
        towerInformation.DeActivation();
        autoPanel.Activation();
    }
}
