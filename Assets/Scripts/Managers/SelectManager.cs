using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���콺 Ŭ������ ������Ʈ ����
/// </summary>
public class SelectManager
{
    //���� ������ ������Ʈ
    private Cell currentSelect;
    private HashSet<Cell> selects;
    
    public void Init() {
        selects = new HashSet<Cell>(Define.CELL_COUNT);
        selects.AddRange(Object.FindObjectsByType<Cell>(FindObjectsSortMode.None));
    }

    /// <summary>
    /// ���������� ���콺 Ŭ�� üũ
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
    /// �� ����
    /// ��ġ�� ���� ���� ����� ���� ����
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
    /// ������Ʈ ����
    /// </summary>
    private void Select() {
        //�̹� ������ ������Ʈ�� �ִٸ� ����
        
        if (currentSelect != null) {
            if (currentSelect != SelectCell()) {
                currentSelect.DeSelect();
                currentSelect = null;
            }
        }

        currentSelect = SelectCell();

        //������Ʈ�� ���õǸ� ����
        if (currentSelect != null)
            currentSelect.Select();
    }
}
