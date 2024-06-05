using TMPro;
using UnityEngine;

/// <summary>
/// ���� ���� �ǳ�
/// </summary>
public class UI_GameOver : MonoBehaviour {
    public static UI_GameOver Instance;
    //���� ǥ�� �ؽ�Ʈ
    [SerializeField]private TextMeshProUGUI scoreText;
    //�ǳ�
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
    /// �ǳ� Ȱ��ȭ �� ��Ȱ��ȭ
    /// ���ھ� ǥ��
    /// </summary>
    /// <param name="trigger"></param>
    public void SetGameOverUI(bool trigger) {
        panelObject.SetActive(trigger);
        scoreText.text = Managers.Game.CurrentKillNumber.ToString();
    }
}