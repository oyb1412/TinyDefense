/// <summary>
/// ȿ���� ���� ���� ��ũ�ѹ�
/// </summary>
public class UI_SFXScrollbar : UI_VolumeScrollbar {
    /// <summary>
    /// ȿ���� ���� ����
    /// </summary>
    /// <param name="value"></param>
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetSfxVolume(value);
    }
}