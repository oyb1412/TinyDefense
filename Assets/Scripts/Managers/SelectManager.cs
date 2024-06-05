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
    
    public void Init() {
        selects = new HashSet<Cell>(Define.CELL_COUNT);
        selects.AddRange(Object.FindObjectsByType<Cell>(FindObjectsSortMode.None));
    }

    /// <summary>
    /// 지속적으로 마우스 클릭 체크
    /// </summary>
    public void OnUpdate() {
#if UNITY_ANDROID
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                Select();
            }
        }
#else
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Select();
        }
#endif
    }

    /// <summary>
    /// 셀 선택
    /// 터치한 곳과 가장 가까운 셀을 선택
    /// </summary>
    /// <returns></returns>
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
