using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 볼륨 조절 스크롤바 부모 클래스
/// </summary>
public class UI_VolumeScrollbar : MonoBehaviour {
    //스크롤 바
    protected Scrollbar scrollBar;
    //볼륨 표시 텍스트
    [SerializeField]protected TextMeshProUGUI valueTMP;

    /// <summary>
    /// 초기화 및 볼륨 조절 콜백 등록
    /// </summary>
    protected virtual void Awake() {
        scrollBar = GetComponent<Scrollbar>();
        scrollBar.onValueChanged.AddListener(SetScrollBar);
    }

    /// <summary>
    /// 볼륨 조절
    /// </summary>
    /// <param name="value"></param>
    protected virtual void SetScrollBar(float value) {
        valueTMP.text = (Mathf.RoundToInt(value * 100f)).ToString();
    }
}