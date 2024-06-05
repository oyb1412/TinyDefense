using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� ���� ���� ��ũ�ѹ� �θ� Ŭ����
/// </summary>
public class UI_VolumeScrollbar : MonoBehaviour {
    //��ũ�� ��
    protected Scrollbar scrollBar;
    //���� ǥ�� �ؽ�Ʈ
    protected TextMeshProUGUI valueTMP;

    /// <summary>
    /// �ʱ�ȭ �� ���� ���� �ݹ� ���
    /// </summary>
    private void Awake() {
        scrollBar = GetComponent<Scrollbar>();
        valueTMP = GetComponentInChildren<TextMeshProUGUI>();

        scrollBar.onValueChanged.AddListener(SetScrollBar);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="value"></param>
    protected virtual void SetScrollBar(float value) {
        valueTMP.text = (Mathf.RoundToInt(value * 100f)).ToString();
    }
}