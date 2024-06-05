using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 'yes' 'no' ��ư ���� �ǳ�
/// </summary>
public class UI_CheckPanel : MonoBehaviour {
    private TextMeshProUGUI questionTMP;
    private UI_CheckYesButton yesButtons;
    private UI_CheckNoButton noButtons;

    private void Awake() {
        questionTMP = GetComponentInChildren<TextMeshProUGUI>();
        yesButtons = GetComponentInChildren<UI_CheckYesButton>();
        noButtons = GetComponentInChildren<UI_CheckNoButton>();
    }

    /// <summary>
    /// Ȱ��ȭ�� �� ��ư�� ������ �Ҵ�
    /// </summary>
    public void Activation(string ment, UnityAction action) {
        gameObject.SetActive(true);
        questionTMP.text = ment;

        yesButtons.Activation(action, this);
        noButtons.Activation(this);
    }

    /// <summary>
    /// ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        gameObject.SetActive(false);
    }
}