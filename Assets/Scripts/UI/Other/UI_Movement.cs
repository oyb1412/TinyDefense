using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI �̵� Ŭ����
/// </summary>
public class UI_Movement : MonoBehaviour {
    private RectTransform rect;
    //���� ������
    [SerializeField] private Vector3 originalPosition;
    //�̵���ų ������
    [SerializeField] private Vector3 targetPosition;
    //�̵� Ʈ��
    private Tween moveTween;
    private void Awake() {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
        rect.anchoredPosition = targetPosition;
    }

    /// <summary>
    /// Ȱ��ȭ��, ���� ���������� �̵�
    /// </summary>
    public void Activation() {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(originalPosition, 0.5f);
    }

    /// <summary>
    /// ��Ȱ��ȭ��, �̵���ų ���������� �̵� �� �ݹ��Լ� ȣ��
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        Util.ResetTween(moveTween);
        moveTween = rect.DOLocalMove(targetPosition, 0.5f).OnComplete(() => action?.Invoke());
    }
}