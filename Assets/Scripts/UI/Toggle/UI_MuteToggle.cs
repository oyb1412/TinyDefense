using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� ��Ʈ ��� ���� �θ� Ŭ����
/// </summary>
public class UI_MuteToggle : MonoBehaviour {
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
    protected virtual void SetToggle(bool trigger) {
        SoundManager.Instance.PlaySfx(Define.SFXType.SelectUIButton);
    }
}