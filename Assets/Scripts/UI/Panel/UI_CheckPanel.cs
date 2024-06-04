using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_CheckPanel : MonoBehaviour {
    private TextMeshProUGUI questionTMP;
    private UI_CheckYesButton yesButtons;
    private UI_CheckNoButton noButtons;

    private void Awake() {
        questionTMP = GetComponentInChildren<TextMeshProUGUI>();
        yesButtons = GetComponentInChildren<UI_CheckYesButton>();
        noButtons = GetComponentInChildren<UI_CheckNoButton>();
    }

    public void Activation(string ment, UnityAction action) {
        gameObject.SetActive(true);
        questionTMP.text = ment;

        yesButtons.Activation(action, this);
        noButtons.Activation(this);
    }

    public void DeActivation() {
        gameObject.SetActive(false);
    }
}