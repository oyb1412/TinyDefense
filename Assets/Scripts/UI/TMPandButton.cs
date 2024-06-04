using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� �ڽ� tmp�� ��ư ���� Ŭ����
/// ������Ʈ�� Ȱ��
/// </summary>
public class TMPandButton : MonoBehaviour {
    //��� �ڽ� ��ư
    private Button[] buttons;
    //��� �ڽ� tmp
    private TextMeshProUGUI[] tmps;
    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        tmps = GetComponentsInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// ��� �ڽ� ������Ʈ Ȱ��ȭ
    /// </summary>
    public void Activation() {
        foreach (Button button in buttons) {
            button.interactable = true;
        }

        foreach (TextMeshProUGUI tmp in tmps) {
            Color color = tmp.color;
            color.a = 1f;
            tmp.color = color;
        }
    }

    /// <summary>
    /// ��� �ڽ� ������Ʈ ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        foreach(Button button in buttons) {
            button.interactable = false;
        }

        foreach(TextMeshProUGUI tmp in tmps) {
            Color color = tmp.color;
            color.a = .5f;
            tmp.color = color;
        }
    }
}