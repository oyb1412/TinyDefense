using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UI_Movement : MonoBehaviour {
    private RectTransform rect;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 targetPosition;

    private Tween moveTween;
    private void Awake() {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
        rect.anchoredPosition = targetPosition;
    }

    public void Activation() {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(originalPosition, 0.5f);
    }

    public void DeActivation(UnityAction action) {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(targetPosition, 0.5f).OnComplete(() => action?.Invoke());
    }
}