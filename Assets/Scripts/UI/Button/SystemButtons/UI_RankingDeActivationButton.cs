/// <summary>
/// ��ŷ �ǳ� ��Ȱ��ȭ ��ư
/// </summary>
public class UI_RankingDeActivationButton : UI_Button {
    public override void Init() {
    }

    /// <summary>
    /// ��ŷ �ǳ� ��Ȱ��ȭ
    /// </summary>
    public override void Select() {
        transform.parent.gameObject.SetActive(false);
    }
}