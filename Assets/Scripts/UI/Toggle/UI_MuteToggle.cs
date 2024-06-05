using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� ��Ʈ ��� ���� �θ� Ŭ����
/// </summary>
public abstract class UI_MuteToggle : MonoBehaviour {
    //���
    private Toggle muteToggle;

    /// <summary>
    /// �ʱ�ȭ �� �ݹ��Լ� ����
    /// </summary>
    private void Awake() {
        muteToggle = GetComponent<Toggle>();
        muteToggle.onValueChanged.AddListener(SetToggle);
    }

    /// <summary>
    /// ���� ��Ʈ
    /// </summary>
    /// <param name="trigger"></param>
    protected abstract void SetToggle(bool trigger);
}