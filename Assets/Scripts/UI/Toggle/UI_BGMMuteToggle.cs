/// <summary>
/// ����� ��Ʈ ���
/// </summary>
public class UI_BGMMuteToggle : UI_MuteToggle {
    /// <summary>
    /// ����� ��Ʈ
    /// </summary>
    /// <param name="trigger"></param>
    protected override void SetToggle(bool trigger) {
        SoundManager.Instance.SetBGMMute(trigger);
    }
}