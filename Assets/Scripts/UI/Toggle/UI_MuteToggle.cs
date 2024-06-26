using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 뮤트 토글 관리 부모 클래스
/// </summary>
public class UI_MuteToggle : MonoBehaviour {
    //토글
    private Toggle muteToggle;

    /// <summary>
    /// 초기화 및 콜백함수 연동
    /// </summary>
    private void Awake() {
        muteToggle = GetComponent<Toggle>();
        muteToggle.onValueChanged.AddListener(SetToggle);
    }

    /// <summary>
    /// 볼륨 뮤트
    /// </summary>
    /// <param name="trigger"></param>
    protected virtual void SetToggle(bool trigger) {
        SoundManager.Instance.PlaySfx(Define.SFXType.SelectUIButton);
    }
}