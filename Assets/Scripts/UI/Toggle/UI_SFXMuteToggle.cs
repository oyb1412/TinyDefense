public class UI_SFXMuteToggle : UI_MuteToggle {
    protected override void SetToggle(bool trigger) {
        SoundManager.Instance.SetSFXMute(trigger);
    }
}