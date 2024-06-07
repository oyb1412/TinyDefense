using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI 이동 클래스
/// </summary>
public class UI_Movement : MonoBehaviour {
    private CanvasGroup canvasGroup;
    //이동 트윈
    private Tween alphaTween;
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 활성화시, 원본 포지션으로 이동
    /// </summary>
    public void Activation() {
        Util.ResetTween(alphaTween);
        alphaTween = canvasGroup.DOFade(1f, 0.5f);
    }

    /// <summary>
    /// 비활성화시, 이동시킬 포지션으로 이동 및 콜백함수 호출
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        Util.ResetTween(alphaTween);
        alphaTween = canvasGroup.DOFade(0f, 0.5f).OnComplete(() => action?.Invoke());
    }
}