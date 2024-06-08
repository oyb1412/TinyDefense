using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ÿ�� ���� �ǳ�
/// </summary>
public class UI_TowerInformation : MonoBehaviour {
    //Ÿ�� ������ �̹���
    [SerializeField] private Image towerIcon;
    //Ÿ�� Ÿ�� ������ �̹���
    [SerializeField] private Image towerType;
    //Ÿ�� �̸� tmp
    [SerializeField] private TextMeshProUGUI towerName;
    //Ÿ�� ������ tmp
    [SerializeField] private TextMeshProUGUI towerDamage;
    //Ÿ�� ���ݼӵ� tmp
    [SerializeField] private TextMeshProUGUI towerDelay;
    //Ÿ�� ��Ÿ� tmp
    [SerializeField] private TextMeshProUGUI towerRange;
    //Ÿ�� ���� tmp
    [SerializeField] private TextMeshProUGUI towerLevel;
    //Ÿ�� ų tmp
    [SerializeField] private TextMeshProUGUI towerKill;
    //Ÿ�� ���� tmp
    [SerializeField] private TextMeshProUGUI towerDescription;
    //��� Ÿ�� ������ ��������Ʈ
    private Sprite[] towerIconSprites;
    //Ÿ�� Ÿ�� ������ ��������Ʈ
    [SerializeField]private Sprite[] towerTypeSprites;
    //���� ������ Ÿ��
    private TowerBase currentTower;
    //Ÿ�� �̵� Ŭ����
    private UI_Movement movement;
    private void Awake() {
        movement = GetComponent<UI_Movement>();
        towerIconSprites = Resources.LoadAll<Sprite>(Managers.Data.DefineData.TOWERICON_SPRITE_PATH);
    }

    /// <summary>
    /// Ȱ��ȭ
    /// �� ���ý� ȣ��
    /// </summary>
    /// <param name="cell">������ ��</param>
    public void Activation(Cell cell) {
        if(!cell.IsUse()) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        movement.Activation();

        currentTower = cell.Tower;

        TowerStatus status = currentTower.TowerStatus;
        towerName.text = Managers.Data.DefineData.TOWER_NAME[(int)status.TowerType];

        towerIcon.sprite = towerIconSprites[(int)status.TowerType];
        towerType.sprite = towerTypeSprites[(int)status.TowerBundle];
        towerDescription.text = Managers.Data.DefineData.TOWER_DESCRIPTION[(int)status.TowerType];
        
        towerRange.text = string.Format(Managers.Data.DefineData.MENT_TOWER_RANGE, status.AttackRange);
        towerLevel.text = string.Format(Managers.Data.DefineData.MENT_TOWER_LEVEL, status.Level);
        towerKill.text = string.Format(Managers.Data.DefineData.MENT_TOWER_KILL, status.TowerKills);

        StartCoroutine(Co_TowerInfomation());
    }

    /// <summary>
    /// ��Ȱ��ȭ
    /// Ÿ�� ���� ������ ȣ��
    /// </summary>
    public void DeActivation() {
        StopAllCoroutines();
        currentTower = null;
        movement.DeActivation(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// Ÿ�� ���� ǥ�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_TowerInfomation() {
        while(true) {
            towerDamage.text = string.Format(Managers.Data.DefineData.MENT_TOWER_DAMAGE, currentTower.TowerStatus.AttackDamage);

            if (currentTower.TowerStatus.AttackDelay <= 0)
                towerDelay.text = Managers.Data.DefineData.MENT_MAX_DELAY;
            else
                towerDelay.text = string.Format(Managers.Data.DefineData.MENT_TOWER_DELAY, currentTower.TowerStatus.AttackDelay);
            yield return null;
        }
    }
}