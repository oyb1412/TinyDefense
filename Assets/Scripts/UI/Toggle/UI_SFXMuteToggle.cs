/// <summary>
/// 효과음 뮤트 토글
/// </summary>
public class UI_SFXMuteToggle : UI_MuteToggle {
    /// <summary>
    /// 효과음 뮤트
    /// </summary>
    /// <param name="trigger"></param>
    protected override void SetToggle(bool trigger) {
        base.SetToggle(trigger);
        SoundManager.Instance.SetSFXMute(trigger);
    }
}