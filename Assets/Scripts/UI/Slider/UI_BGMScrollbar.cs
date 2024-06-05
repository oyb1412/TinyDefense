/// <summary>
/// BGM볼륨 변경 스크롤 바
/// </summary>
public class UI_BGMScrollbar : UI_VolumeScrollbar {
    /// <summary>
    /// 사운드 매니저 볼륨 변경
    /// </summary>
    /// <param name="value"></param>
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetBgmVolume(value);
    }
}