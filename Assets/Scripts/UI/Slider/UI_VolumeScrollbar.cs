using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_VolumeScrollbar : MonoBehaviour {
    protected Scrollbar scrollBar;
    protected TextMeshProUGUI valueTMP;
    private void Awake() {
        scrollBar = GetComponent<Scrollbar>();
        valueTMP = GetComponentInChildren<TextMeshProUGUI>();

        scrollBar.onValueChanged.AddListener(SetScrollBar);
    }

    protected virtual void SetScrollBar(float value) {
        valueTMP.text = (Mathf.RoundToInt(value * 100f)).ToString();
    }
}