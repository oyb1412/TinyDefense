using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 자식 tmp및 버튼 관리 클래스
/// 컴포넌트로 활용
/// </summary>
public class TMPandButton : MonoBehaviour {
    //모든 자식 버튼
    private Button[] buttons;
    //모든 자식 tmp
    private TextMeshProUGUI[] tmps;
    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        tmps = GetComponentsInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// 모든 자식 오브젝트 활성화
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
    /// 모든 자식 오브젝트 비활성화
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