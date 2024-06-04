using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 마우스 클릭으로 오브젝트 선택
/// </summary>
public class SelectManager
{
    //현재 선택한 오브젝트
    private Cell currentSelect;
    private HashSet<Cell> selects;
    /// <summary>
    /// 지속적으로 마우스 클릭 체크
    /// </summary>
    /// 
    public void Init() {
        selects = new HashSet<Cell>(Define.CELL_COUNT);
        selects.AddRange(Object.FindObjectsByType<Cell>(FindObjectsSortMode.None));
    }

    public void OnUpdate() {
        if(Input.GetMouseButtonDown(0) &&
            !EventSystem.current.IsPointerOverGameObject()) {
            Select();
        }
    }

    public Cell SelectCell() {
        Vector2 inputPos = Vector2.zero;

#if UNITY_EDITOR || UNITY_STANDALONE
        
        inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
    
    if (Input.touchCount > 0) {
        Touch touch = Input.GetTouch(0);
        inputPos = Camera.main.ScreenToWorldPoint(touch.position);
    }
#endif

        foreach (var item in selects) {
            if (Vector2.Distance(inputPos, item.transform.position) < Define.MOUSE_CLICK_RANGE) {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// 오브젝트 선택
    /// </summary>
    private void Select() {
        //이미 선택한 오브젝트가 있다면 해제
        
        if (currentSelect != null) {
            if (currentSelect != SelectCell()) {
                currentSelect.DeSelect();
                currentSelect = null;
            }
        }

        currentSelect = SelectCell();

        //오브젝트가 선택되면 선택
        if (currentSelect != null)
            currentSelect.Select();
    }
}
