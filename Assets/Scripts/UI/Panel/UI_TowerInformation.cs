using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 타워 정보 판넬
/// </summary>
public class UI_TowerInformation : MonoBehaviour {
    //타워 아이콘 이미지
    [SerializeField] private Image towerIcon;
    //타워 타입 아이콘 이미지
    [SerializeField] private Image towerType;
    //타워 이름 tmp
    [SerializeField] private TextMeshProUGUI towerName;
    //타워 데미지 tmp
    [SerializeField] private TextMeshProUGUI towerDamage;
    //타워 공격속도 tmp
    [SerializeField] private TextMeshProUGUI towerDelay;
    //타워 사거리 tmp
    [SerializeField] private TextMeshProUGUI towerRange;
    //타워 레벨 tmp
    [SerializeField] private TextMeshProUGUI towerLevel;
    //타워 킬 tmp
    [SerializeField] private TextMeshProUGUI towerKill;
    //타워 정보 tmp
    [SerializeField] private TextMeshProUGUI towerDescription;
    //모든 타워 아이콘 스프라이트
    private Sprite[] towerIconSprites;
    //타워 타입 아이콘 스프라이트
    [SerializeField]private Sprite[] towerTypeSprites;
    //현재 선택한 타워
    private TowerBase currentTower;
    //타워 이동 클래스
    private UI_Movement movement;
    private void Awake() {
        movement = GetComponent<UI_Movement>();
        towerIconSprites = Resources.LoadAll<Sprite>(Define.TOWERICON_SPRITE_PATH);
    }

    /// <summary>
    /// 활성화
    /// 셀 선택시 호출
    /// </summary>
    /// <param name="cell">선택한 셀</param>
    public void Activation(Cell cell) {
        if(!cell.IsUse()) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        movement.Activation();

        currentTower = cell.Tower;

        TowerStatus status = currentTower.TowerStatus;
        towerName.text = Define.TOWER_NAME[(int)status.TowerType];

        towerIcon.sprite = towerIconSprites[(int)status.TowerType];
        towerType.sprite = towerTypeSprites[(int)status.TowerBundle];
        towerDescription.text = Define.TOWER_DESCRIPTION[(int)status.TowerType];
        
        towerRange.text = string.Format(Define.MENT_TOWER_RANGE, status.AttackRange);
        towerLevel.text = string.Format(Define.MENT_TOWER_LEVEL, status.Level);
        towerKill.text = string.Format(Define.MENT_TOWER_KILL, status.TowerKills);

        StartCoroutine(Co_TowerInfomation());
    }

    /// <summary>
    /// 비활성화
    /// 타워 선택 해제시 호출
    /// </summary>
    public void DeActivation() {
        StopAllCoroutines();
        currentTower = null;
        movement.DeActivation(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// 타워 정보 표기 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_TowerInfomation() {
        while(true) {
            towerDamage.text = string.Format(Define.MENT_TOWER_DAMAGE, currentTower.TowerStatus.AttackDamage);

            if (currentTower.TowerStatus.AttackDelay <= 0)
                towerDelay.text = Define.MENT_MAX_DELAY;
            else
                towerDelay.text = string.Format(Define.MENT_TOWER_DELAY, currentTower.TowerStatus.AttackDelay);
            yield return null;
        }
    }
}