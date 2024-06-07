/// <summary>
/// ����� ��Ʈ ���
/// </summary>
public class UI_BGMMuteToggle : UI_MuteToggle {
    /// <summary>
    /// ����� ��Ʈ
    /// </summary>
    /// <param name="trigger"></param>
    protected override void SetToggle(bool trigger) {
        base.SetToggle(trigger);
        SoundManager.Instance.SetBGMMute(trigger);
    }
}