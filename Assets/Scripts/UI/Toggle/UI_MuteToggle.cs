using UnityEngine;
using UnityEngine.UI;

public abstract class UI_MuteToggle : MonoBehaviour {
    private Toggle muteToggle;

    private void Awake() {
        muteToggle = GetComponent<Toggle>();

        muteToggle.onValueChanged.AddListener(SetToggle);
    }

    protected abstract void SetToggle(bool trigger);
}