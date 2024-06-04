using UnityEngine;

/// <summary>
/// 각 셀 관리
/// </summary>
public class Cell : MonoBehaviour, ISelect {
    //셀에 건설된 타워
    public TowerBase Tower { get;  set; }
    //셀 world ui 캔버스
    private SpriteRenderer spriteRenderer;
    private SelectArrow selectArrow;
    public int CellHandle { get; private set; }

    public bool IsSelected { get; set; }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
    }

    public void Init(int handle, Vector3 pos) {
        CellHandle = handle;
        transform.localPosition = pos;
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

    public void MovementSelect() {
        if(spriteRenderer.color != Color.green) {
            spriteRenderer.color = Color.green;
            selectArrow.Activation();
        }
    }

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
        this.Tower = tower;

        if (!this.Tower)
            return;

        this.Tower.TowerCell = this;
        this.Tower.transform.position = transform.position + Define.TOWER_CREATE_POSITION;
    }
}