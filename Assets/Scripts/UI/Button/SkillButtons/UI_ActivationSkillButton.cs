using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ��ų ��� ��ư �θ� Ŭ����
/// </summary>
public abstract class UI_ActivationSkillButton : UI_Button {
    //��ų ������
    private Image iconImage;
    public override void Init() {
        iconImage = GetComponent<Image>();
        seletable = false;
        button.interactable = false;
    }

    /// <summary>
    /// ��ų ��Ÿ�� ǥ��� �ڷ�ƾ
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
    /// ��ų ���
    /// </summary>
    /// <param name="coolTime"></param>
    protected void UseSkill(float coolTime) {
        StartCoroutine(Co_Cooltime(coolTime));
    }

    /// <summary>
    /// ��ų ��Ÿ�� ȸ�� �� �ʱ�ȭ
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