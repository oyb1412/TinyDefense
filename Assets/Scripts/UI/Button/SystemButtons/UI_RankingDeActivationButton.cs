public class UI_RankingDeActivationButton : UI_Button {
    public override void Init() {
    }

    public override void Select() {
        transform.parent.gameObject.SetActive(false);
    }
}