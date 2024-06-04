public class UI_SFXScrollbar : UI_VolumeScrollbar {
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetSfxVolume(value);
    }
}