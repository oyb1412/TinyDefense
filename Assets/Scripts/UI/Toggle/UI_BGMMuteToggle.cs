/// <summary>
/// 배경음 뮤트 토글
/// </summary>
public class UI_BGMMuteToggle : UI_MuteToggle {
    /// <summary>
    /// 배경음 뮤트
    /// </summary>
    /// <param name="trigger"></param>
    protected override void SetToggle(bool trigger) {
        base.SetToggle(trigger);
        SoundManager.Instance.SetBGMMute(trigger);
    }
}