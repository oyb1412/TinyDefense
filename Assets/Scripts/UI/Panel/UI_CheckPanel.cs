using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 'yes' 'no' 버튼 관리 판넬
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
    /// 활성화시 각 버튼에 데이터 할당
    /// </summary>
    public void Activation(string ment, UnityAction action) {
        gameObject.SetActive(true);
        questionTMP.text = ment;

        yesButtons.Activation(action, this);
        noButtons.Activation(this);
    }

    /// <summary>
    /// 비활성화
    /// </summary>
    public void DeActivation() {
        gameObject.SetActive(false);
    }
}