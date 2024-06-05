using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI 이동 클래스
/// </summary>
public class UI_Movement : MonoBehaviour {
    private RectTransform rect;
    //원본 포지션
    [SerializeField] private Vector3 originalPosition;
    //이동시킬 포지션
    [SerializeField] private Vector3 targetPosition;
    //이동 트윈
    private Tween moveTween;
    private void Awake() {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
        rect.anchoredPosition = targetPosition;
    }

    /// <summary>
    /// 활성화시, 원본 포지션으로 이동
    /// </summary>
    public void Activation() {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(originalPosition, 0.5f);
    }

    /// <summary>
    /// 비활성화시, 이동시킬 포지션으로 이동 및 콜백함수 호출
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(targetPosition, 0.5f).OnComplete(() => action?.Invoke());
    }
}