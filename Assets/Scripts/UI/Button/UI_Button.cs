using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �Ǽ�, �Ǹ�, �ռ�, ��ȭ �� ��� ��ư ����
/// </summary>
public abstract class UI_Button : MonoBehaviour {
    protected Button button;
    private EventTrigger eventTrigger;
    //��ư Ȱ��ȭ ����
    protected bool seletable = true;

    protected virtual void Awake() {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();
    }

    /// <summary>
    /// �̺�Ʈ Ʈ���ſ� �Լ� ����
    /// </summary>
    private void Start() {
        Init();

        AddEventTrigger(OnPointerDown, EventTriggerType.PointerDown);
        AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
    }

    /// <summary>
    /// ��ư ���ý� ȣ��
    /// ������ ����
    /// </summary>
    /// <param name="eventData"></param>
    private void OnPointerDown(BaseEventData eventData) {
        button.transform.localScale = new Vector3(.9f, .9f, 1f);
    }

    /// <summary>
    /// ��ư MouseUp�� ȣ��
    /// ������ ���� �� Select�Լ� ȣ��
    /// </summary>
    /// <param name="eventData"></param>
    private void OnPointerUp(BaseEventData eventData) {
        button.transform.localScale = Vector3.one;
        if (!seletable)
            return;

        Select();
    }

    /// <summary>
    /// �̺�Ʈ Ʈ���ſ� �̺�Ʈ �߰�
    /// </summary>
    /// <param name="action">�߰��� �̺�Ʈ</param>
    /// <param name="triggerType">�߰��� �̺�Ʈ Ÿ��</param>
    private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }

    public abstract void Init();

    public abstract void Select();
}