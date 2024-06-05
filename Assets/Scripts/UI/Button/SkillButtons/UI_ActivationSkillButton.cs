using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 스킬 사용 버튼 부모 클래스
/// </summary>
public abstract class UI_ActivationSkillButton : UI_Button {
    //스킬 아이콘
    private Image iconImage;
    public override void Init() {
        iconImage = GetComponent<Image>();
        seletable = false;
        button.interactable = false;
    }

    /// <summary>
    /// 스킬 쿨타임 표기용 코루틴
    /// </summary>
    /// <param name="coolTime"></param>
    /// <returns></returns>
    protected IEnumerator Co_Cooltime(float coolTime) {
        iconImage.fillAmount = 0f;

        float elapsedTime = 0f; 

        while (elapsedTime < coolTime) {
            yield return null;
            elapsedTime += Time.deltaTime; 
            iconImage.fillAmount = Mathf.Clamp01(elapsedTime / coolTime); 
        }

        iconImage.fillAmount = 1f;
        IconActivation();
    }

    /// <summary>
    /// 스킬 사용
    /// </summary>
    /// <param name="coolTime"></param>
    protected void UseSkill(float coolTime) {
        StartCoroutine(Co_Cooltime(coolTime));
    }

    /// <summary>
    /// 스킬 쿨타임 회복 시 초기화
    /// </summary>
    protected void IconActivation() {
        iconImage.fillAmount = 1f;
        seletable = true;
        button.interactable = true;
    }

    public override void Select() {
        OnSelect();
    }

    protected virtual void OnSelect() {
        button.interactable = false;
        seletable = false;
    }
}