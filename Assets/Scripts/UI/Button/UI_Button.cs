using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 건설, 판매, 합성, 강화 등 모든 버튼 관리
/// </summary>
public abstract class UI_Button : MonoBehaviour {
    protected Button button;
    private EventTrigger eventTrigger;
    //버튼 활성화 여부
    protected bool seletable = true;

    protected virtual void Awake() {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
    }

    /// <summary>
    /// 이벤트 트리거에 함수 연동
    /// </summary>
    private void Start() {
        Init();

        AddEventTrigger(OnPointerDown, EventTriggerType.PointerDown);
        AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
    }

    /// <summary>
    /// 버튼 선택시 호출
    /// 스케일 조절
    /// </summary>
    /// <param name="eventData"></param>
    private void OnPointerDown(BaseEventData eventData) {
        button.transform.localScale = new Vector3(.9f, .9f, 1f);
    }

    /// <summary>
    /// 버튼 MouseUp시 호출
    /// 스케일 조절 및 Select함수 호출
    /// </summary>
    /// <param name="eventData"></param>
    private void OnPointerUp(BaseEventData eventData) {
        button.transform.localScale = Vector3.one;
        if (!seletable)
            return;

        Select();
    }

    /// <summary>
    /// 이벤트 트리거에 이벤트 추가
    /// </summary>
    /// <param name="action">추가할 이벤트</param>
    /// <param name="triggerType">추가할 이벤트 타입</param>
    private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }

    public abstract void Init();

    public abstract void Select();
}