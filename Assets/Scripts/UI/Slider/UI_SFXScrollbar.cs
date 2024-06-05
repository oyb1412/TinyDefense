/// <summary>
/// 효과음 볼륨 조절 스크롤바
/// </summary>
public class UI_SFXScrollbar : UI_VolumeScrollbar {
    /// <summary>
    /// 효과음 볼륨 조절
    /// </summary>
    /// <param name="value"></param>
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetSfxVolume(value);
    }
}