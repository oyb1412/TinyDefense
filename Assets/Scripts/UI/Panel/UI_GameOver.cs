using TMPro;
using UnityEngine;

/// <summary>
/// 게임 오버 판넬
/// </summary>
public class UI_GameOver : MonoBehaviour {
    public static UI_GameOver Instance;
    //점수 표기 텍스트
    [SerializeField]private TextMeshProUGUI scoreText;
    //판넬
    [SerializeField]private GameObject panelObject;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 판넬 활성화 및 비활성화
    /// 스코어 표기
    /// </summary>
    /// <param name="trigger"></param>
    public void SetGameOverUI(bool trigger) {
        panelObject.SetActive(trigger);
        scoreText.text = Managers.Game.CurrentKillNumber.ToString();
    }
}