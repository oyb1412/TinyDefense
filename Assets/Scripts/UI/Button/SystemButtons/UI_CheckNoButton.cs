/// <summary>
/// '아니오'버튼 클래스
/// </summary>
public class UI_CheckNoButton : UI_Button {
    private UI_CheckPanel checkPanel;

    /// <summary>
    /// 활성화
    /// 선택시 활성화할 판넬 저장
    /// </summary>
    /// <param name="checkPanel"></param>
    public void Activation(UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
    }
    public override void Init() {
        buttonSfxType = Define.SFXType.DeSelectUIButton;
    }

    /// <summary>
    /// '아니오' 선택시, 저장해둔 판넬 활성화
    /// </summary>
    public override void Select() {
        checkPanel.DeActivation();
    }
}