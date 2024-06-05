using UnityEngine;

/// <summary>
/// 자동 버튼을 관리하는 판넬
/// </summary>
public class UI_AutoPanel : MonoBehaviour {
    //UI이동 클래스
    private UI_Movement movement;

    private void Awake() {
        movement = GetComponent<UI_Movement>();
    }

    /// <summary>
    /// 활성화 시 UI 이동
    /// </summary>
    public void Activation() {
        gameObject.SetActive(true);
        movement.Activation();
    }

    /// <summary>
    /// 비활성화시 이동 및 판넬 비활성화
    /// </summary>
    public void DeActivation() {
        movement.DeActivation(() => gameObject.SetActive(false));
    }
}