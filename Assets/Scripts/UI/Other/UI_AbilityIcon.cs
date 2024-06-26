using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 어빌리티 아이콘 관리 클래스
/// </summary>
public class UI_AbilityIcon : MonoBehaviour {
    private EventTrigger eventTrigger;
    //오브젝트 어빌리티 정보
    private Define.AbilityValue abilityIcon;
    //표기할 이미지 컴포넌트
    public Image IconImage {  get; private set; }
    //어빌리티 정보 표기 판넬
    [SerializeField] private UI_DescriptionPanel abilityDescription;

    /// <summary>
    /// 초기화 및 이벤트 트리거에 함수 연동
    /// </summary>
    private void Awake() {
        IconImage = GetComponent<Image>();  
        eventTrigger = GetComponent<EventTrigger>();

        AddEventTrigger(OnPointerDown, EventTriggerType.PointerDown);
        AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);

        IconImage.color = Managers.Data.DefineData.COLOR_NOT;
        IconImage.sprite = null;
    }

    /// <summary>
    /// 어빌리티 정보 저장
    /// 어빌리티 습득시 호출
    /// </summary>
    /// <param name="abilityIcon">저장할 어빌리티 정보</param>
    public void SetAbilityIcon(Define.AbilityValue abilityIcon) {
        this.abilityIcon = abilityIcon;
        IconImage.color = Color.white;
        IconImage.sprite = this.abilityIcon.Sprite;
    }

    /// <summary>
    /// 어빌리티 아이콘 선택시 정보 표기 판넬 호출
    /// </summary>
    private void OnPointerDown(BaseEventData eventData) {
        if (abilityIcon == null)
            return;

        abilityDescription.ActivationDescription(abilityIcon.Name, abilityIcon.Description, transform.position);
    }

    /// <summary>
    /// 어빌리티 아이콘 pressUp시 정보 표기 판넬 비활성화
    /// </summary>
    private void OnPointerUp(BaseEventData eventData) {
        if (abilityIcon == null)
            return;

        abilityDescription.DeActivationDescription();
    }

    /// <summary>
    /// 이벤트트리거에 이벤트함수 추가
    /// </summary>
    /// <param name="action">추가할 함수</param>
    /// <param name="triggerType">이벤트 타입</param>
    private void AddEventTrigger(UnityAction<BaseEventData> action, EventTriggerType triggerType) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }
}