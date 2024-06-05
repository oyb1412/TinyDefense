using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// 로딩 상태 표기 텍스트 클래스
/// </summary>
public class UI_LoadingText : MonoBehaviour {
    //로딩 중 표기 텍스트
    [SerializeField] private string mainText;
    //로딩 완료시 표기 텍스트
    [SerializeField] private string clearText;
    //텍스트 컴포넌트
    private Text text;
    //로딩 % 표기 텍스트
    private TextMeshProUGUI loadingText;

    /// <summary>
    /// 초기화 및 로딩 중... 텍스트 트위닝 실행
    /// </summary>
    private void Awake() {
        text = GetComponent<Text>();
        loadingText = GetComponentInChildren<TextMeshProUGUI>();
        text.DOText(mainText, 1.5f).SetLoops(-1, LoopType.Restart);
    }

    /// <summary>
    /// 로딩 % 텍스트 표기
    /// </summary>
    /// <param name="value"></param>
    public void SetLoadingText(float value) {
        int integerValue = Mathf.RoundToInt(value * 100);
        loadingText.text = $"{integerValue.ToString()}%";
    }

    /// <summary>
    /// 로딩 완료
    /// 모든 트위닝 종료
    /// 로딩 완료 텍스트 표기
    /// </summary>
    public void CompleteLoading() {
        DOTween.KillAll();
        text.text = clearText;
    }
}