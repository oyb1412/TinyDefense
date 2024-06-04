public class UI_BGMScrollbar : UI_VolumeScrollbar {
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetBgmVolume(value);
    }
}