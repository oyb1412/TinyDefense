using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UI �̵� Ŭ����
/// </summary>
public class UI_Movement : MonoBehaviour {
    private CanvasGroup canvasGroup;
    //�̵� Ʈ��
    private Tween alphaTween;
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Ȱ��ȭ��, ���� ���������� �̵�
    /// </summary>
    public void Activation() {
        Util.ResetTween(alphaTween);
        alphaTween = canvasGroup.DOFade(1f, 0.5f);
    }

    /// <summary>
    /// ��Ȱ��ȭ��, �̵���ų ���������� �̵� �� �ݹ��Լ� ȣ��
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        Util.ResetTween(alphaTween);
        alphaTween = canvasGroup.DOFade(0f, 0.5f).OnComplete(() => action?.Invoke());
    }
}