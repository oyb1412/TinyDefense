/// <summary>
/// ȿ���� ��Ʈ ���
/// </summary>
public class UI_SFXMuteToggle : UI_MuteToggle {
    /// <summary>
    /// ȿ���� ��Ʈ
    /// </summary>
    /// <param name="trigger"></param>
    protected override void SetToggle(bool trigger) {
        base.SetToggle(trigger);
        SoundManager.Instance.SetSFXMute(trigger);
    }
}