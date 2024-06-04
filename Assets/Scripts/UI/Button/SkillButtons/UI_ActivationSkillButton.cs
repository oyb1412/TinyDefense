using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UI_ActivationSkillButton : UI_Button {
    private Image iconImage;
    public override void Init() {
        iconImage = GetComponent<Image>();
        seletable = false;
        button.interactable = false;
    }

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

    protected void UseSkill(float coolTime) {
        StartCoroutine(Co_Cooltime(coolTime));
    }

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
        //어쨌든 버튼 한번 누르면 비활성화
    }
}