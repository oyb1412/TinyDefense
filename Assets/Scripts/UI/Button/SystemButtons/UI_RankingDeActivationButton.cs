/// <summary>
/// 랭킹 판넬 비활성화 버튼
/// </summary>
public class UI_RankingDeActivationButton : UI_Button {
    public override void Init() {
    }

    /// <summary>
    /// 랭킹 판넬 비활성화
    /// </summary>
    public override void Select() {
        transform.parent.gameObject.SetActive(false);
    }
}