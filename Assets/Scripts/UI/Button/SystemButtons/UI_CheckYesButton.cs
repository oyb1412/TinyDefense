using UnityEngine.Events;

/// <summary>
/// '예'버튼 클래스
/// </summary>
public class UI_CheckYesButton : UI_Button {
    //버튼 선택시 실행할 델리게이트
    private UnityAction action;
    //선택시 비활성화할 판넬 저장
    private UI_CheckPanel checkPanel;

    /// <summary>
    /// 버튼 활성화
    /// </summary>
    public void Activation(UnityAction action, UI_CheckPanel checkPanel) {
        this.checkPanel = checkPanel;
        this.action = action;
    }
    public override void Init() {
        
    }

    /// <summary>
    /// 버튼 선택
    /// 델리게이트 실행 및 저장한 판넬 비활성화
    /// </summary>
    public override void Select() {
        action?.Invoke();
        checkPanel.DeActivation();
    }
}