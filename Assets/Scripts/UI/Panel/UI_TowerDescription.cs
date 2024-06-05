using UnityEngine;

/// <summary>
/// Ÿ�� ���� �� ����,
/// Ÿ�� ����, �Ǹ�, ���׷��̵� ���� ĵ����
/// </summary>
public class UI_TowerDescription : MonoBehaviour 
{
    public static UI_TowerDescription Instance;

    //������Ʈ �ǳ�
    [SerializeField] private GameObject DescriptionPanel;
    //�ڵ� �ǳ�
    [SerializeField] private UI_AutoPanel autoPanel;
    //Ÿ�� ���� ��ư
    public UI_CreateButton CreateButton { get; private set; }
    //Ÿ�� ����,�Ǹ�,���׷��̵� ��ư
    private UI_TowerBuild towerBuild;
    //Ÿ�� ���� �ǳ�
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
    /// �ǳ� Ȱ��ȭ
    /// </summary>
    /// <param name="cell">������ ��</param>
    public void Activation(Cell cell) {
        DescriptionPanel.SetActive(true);
        autoPanel.DeActivation();
        towerBuild.Activation(cell);
        towerInformation.Activation(cell);
    }

    /// <summary>
    /// �ǳ� ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        towerBuild.DeActivation(() => DescriptionPanel.SetActive(false));
        towerInformation.DeActivation();
        autoPanel.Activation();
    }
}
