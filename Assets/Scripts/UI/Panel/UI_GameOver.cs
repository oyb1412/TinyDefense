using TMPro;
using UnityEngine;

public class UI_GameOver : MonoBehaviour {
    public static UI_GameOver Instance;
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private GameObject panelObject;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    public void SetGameOverUI(bool trigger) {
        panelObject.SetActive(trigger);
        scoreText.text = Managers.Game.CurrentKillNumber.ToString();
    }
}