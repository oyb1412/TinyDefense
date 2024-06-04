public class UI_BGMMuteToggle : UI_MuteToggle {
    protected override void SetToggle(bool trigger) {
        SoundManager.Instance.SetBGMMute(trigger);
    }
}