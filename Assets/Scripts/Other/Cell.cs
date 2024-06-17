using UnityEngine;

/// <summary>
/// 각 셀 관리
/// </summary>
public class Cell : MonoBehaviour, ISelect {
    //셀에 건설된 타워
    public TowerBase Tower { get;  set; }
    //셀 world ui 캔버스
    private SpriteRenderer spriteRenderer;
    //셀 선택 표기용 마크
    private SelectArrow selectArrow;
    //셀이 선택중인가?
    public bool IsSelected { get; set; }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
    }

    /// <summary>
    /// 셀 선택 해제 및 색 변경
    /// 타워가 있다면, 타워 선택 해제
    /// </summary>
    public void DeSelect() {
        UI_TowerDescription.Instance.DeActivation();

        if (IsUse()) {
            Tower.DeSelect();
            return;
        }
        selectArrow.DeActivation();
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// 이 셀로이동 하려고 하면, 셀 색 변경
    /// </summary>
    public void MovementSelect() {
        if (IsSelected)
            return;

        if (spriteRenderer.color != Color.green) {
            spriteRenderer.color = Color.green;
            selectArrow.Activation();
        }
    }

    /// <summary>
    /// 이 셀로 이동을 취소하면, 셀 색 변경
    /// </summary>
    public void MovementDeSelect() {
        if (spriteRenderer.color != Color.white) {
            spriteRenderer.color = Color.white;
            selectArrow.DeActivation();
        }
    }

    /// <summary>
    /// 셀 선택 및 색 변경
    /// 타워가 있다면, 타워 선택
    /// </summary>
    public void Select() {
        if (IsSelected)
            return;

        UI_TowerDescription.Instance.Activation(this);

        if (IsUse()) {
            Tower.Select();
            return;
        }
        selectArrow.Activation();
        spriteRenderer.color = Color.green;
    }

    /// <summary>
    /// 셀이 사용중인가?
    /// </summary>
    public bool IsUse() {
        return Tower;
    }

    /// <summary>
    /// 셀에 타워 건설
    /// </summary>
    /// <param name="tower"></param>
    public void SetTower(TowerBase tower) {
        DeSelect();
        Tower = tower;

        if (!Tower)
            return;

        Tower.TowerCell = this;
        Tower.transform.position = transform.position + Managers.Data.DefineData.TOWER_CREATE_POSITION;
    }
}