using UnityEngine;

/// <summary>
/// 타워 자동생성 버튼
/// </summary>
public class UI_AutoCreateButton : UI_Button {
    //씬에 존재하는 모든 셀
    private Cell[] cells;
    //타워 생성 어빌리티 선택 시, 타워 생성 유무
    private bool isGetTower;
    //타워 생성 버튼
    [SerializeField]private UI_CreateButton createButton;
    public override void Init() {
        cells = FindObjectsByType<Cell>(FindObjectsSortMode.None);
    }

    /// <summary>
    /// 타워 자동생성 버튼 선택시
    /// </summary>
    public override void Select() {
        foreach (Cell cell in cells) {
            if(!cell.IsUse()) {
                //골드가 부족하면 취소
                if (createButton.CreateCost > Managers.Game.CurrentGold)
                    return;

                //타워 자동생성
                createButton.CellCreate(cell);
            }
        }
    }

    /// <summary>
    /// 어빌리티(타워 획득)선택시 호출
    /// </summary>
    public void GetOneTower() {
        //만약 객체가 비활성화 상태일시 일시적 활성화
        if (!transform.parent.gameObject.activeInHierarchy) {
            transform.parent.gameObject.SetActive(true);
            isGetTower = true;
        }

        //사용중이지 않는 셀을 찾고
        //찾으면 그 셀에 타워 생성 및 업그레이드
        foreach (Cell cell in cells) {
            if (!cell.IsUse()) {
                createButton.CreateAndRandomUpgrade(cell);
                break;
            }
        }

        //객체를 다시 비활성화
        if(isGetTower) {
            transform.parent.gameObject.SetActive(false);
            isGetTower = false;
        }
    }
}