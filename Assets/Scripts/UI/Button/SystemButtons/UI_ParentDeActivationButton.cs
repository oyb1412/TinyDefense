/// <summary>
/// ��ŷ �ǳ� ��Ȱ��ȭ ��ư
/// </summary>
public class UI_ParentDeActivationButton : UI_Button {
    public override void Init() {
        buttonSfxType = Define.SFXType.DeSelectUIButton;
    }

    /// <summary>
    /// ��ŷ �ǳ� ��Ȱ��ȭ
    /// </summary>
    public override void Select() {
        
        transform.parent.gameObject.SetActive(false);
    }
}