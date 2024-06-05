using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// �ε� ���� ǥ�� �ؽ�Ʈ Ŭ����
/// </summary>
public class UI_LoadingText : MonoBehaviour {
    //�ε� �� ǥ�� �ؽ�Ʈ
    [SerializeField] private string mainText;
    //�ε� �Ϸ�� ǥ�� �ؽ�Ʈ
    [SerializeField] private string clearText;
    //�ؽ�Ʈ ������Ʈ
    private Text text;
    //�ε� % ǥ�� �ؽ�Ʈ
    private TextMeshProUGUI loadingText;

    /// <summary>
    /// �ʱ�ȭ �� �ε� ��... �ؽ�Ʈ Ʈ���� ����
    /// </summary>
    private void Awake() {
        text = GetComponent<Text>();
        loadingText = GetComponentInChildren<TextMeshProUGUI>();
        text.DOText(mainText, 1.5f).SetLoops(-1, LoopType.Restart);
    }

    /// <summary>
    /// �ε� % �ؽ�Ʈ ǥ��
    /// </summary>
    /// <param name="value"></param>
    public void SetLoadingText(float value) {
        int integerValue = Mathf.RoundToInt(value * 100);
        loadingText.text = $"{integerValue.ToString()}%";
    }

    /// <summary>
    /// �ε� �Ϸ�
    /// ��� Ʈ���� ����
    /// �ε� �Ϸ� �ؽ�Ʈ ǥ��
    /// </summary>
    public void CompleteLoading() {
        DOTween.KillAll();
        text.text = clearText;
    }
}