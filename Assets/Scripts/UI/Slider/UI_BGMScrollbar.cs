/// <summary>
/// BGM���� ���� ��ũ�� ��
/// </summary>
public class UI_BGMScrollbar : UI_VolumeScrollbar {
    /// <summary>
    /// ���� �Ŵ��� ���� ����
    /// </summary>
    /// <param name="value"></param>
    protected override void SetScrollBar(float value) {
        base.SetScrollBar(value);
        SoundManager.Instance.SetBgmVolume(value);
    }
}