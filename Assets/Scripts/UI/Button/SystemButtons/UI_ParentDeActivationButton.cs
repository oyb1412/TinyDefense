/// <summary>
/// 랭킹 판넬 비활성화 버튼
/// </summary>
public class UI_ParentDeActivationButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.DeSelectUIButton;
    }

    /// <summary>
    /// 랭킹 판넬 비활성화
    /// </summary>
    public override void Select() {
        
        transform.parent.gameObject.SetActive(false);
    }
}