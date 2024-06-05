using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �����Ƽ ������ ���� Ŭ����
/// </summary>
public class UI_AbilityIcon : MonoBehaviour {
    private EventTrigger eventTrigger;
    //������Ʈ �����Ƽ ����
    private Define.AbilityValue abilityIcon;
    //ǥ���� �̹��� ������Ʈ
    public Image IconImage {  get; private set; }
    //�����Ƽ ���� ǥ�� �ǳ�
    [SerializeField] private UI_DescriptionPanel abilityDescription;

    /// <summary>
    /// �ʱ�ȭ �� �̺�Ʈ Ʈ���ſ� �Լ� ����
    /// </summary>
    private void Awake() {
        IconImage = GetComponent<Image>();  
        eventTrigger = GetComponent<EventTrigger>();

        AddEventTrigger(OnPointerDown, EventTriggerType.PointerDown);
        AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);

        IconImage.color = Define.COLOR_NOT;
        IconImage.sprite = null;
    }

    /// <summary>
    /// �����Ƽ ���� ����
    /// �����Ƽ ����� ȣ��
    /// </summary>
    /// <param name="abilityIcon">������ �����Ƽ ����</param>
    public void SetAbilityIcon(Define.AbilityValue abilityIcon) {
        this.abilityIcon = abilityIcon;
        IconImage.color = Color.white;
        IconImage.sprite = this.abilityIcon.Sprite;
    }

    /// <summary>
    /// �����Ƽ ������ ���ý� ���� ǥ�� �ǳ� ȣ��
    /// </summary>
    private void OnPointerDown(BaseEventData eventData) {
        if (abilityIcon == null)
            return;

        abilityDescription.ActivationDescription(abilityIcon.Name, abilityIcon.Description, transform.position);
    }

    /// <summary>
    /// �����Ƽ ������ pressUp�� ���� ǥ�� �ǳ� ��Ȱ��ȭ
    /// </summary>
    private void OnPointerUp(BaseEventData eventData) {
        if (abilityIcon == null)
            return;

        abilityDescription.DeActivationDescription();
    }

    /// <summary>
    /// �̺�ƮƮ���ſ� �̺�Ʈ�Լ� �߰�
    /// </summary>
    /// <param name="action">�߰��� �Լ�</param>
    /// <param name="triggerType">�̺�Ʈ Ÿ��</param>
    private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }
}