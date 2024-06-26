using UnityEngine;

/// <summary>
/// 게임 종료 버튼
/// </summary>
public class UI_HomeButton : UI_Button {
    //yes,no 버튼 판넬
    [SerializeField] private UI_CheckPanel checkPanel;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    /// <summary>
    /// 버튼 선택시
    /// yes, no 버튼 판넬 활성화
    /// </summary>
    public override void Select() {
        checkPanel.Activation(Managers.Data.DefineData.MENT_BUTTON_HOME, GoHome);
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    private void GoHome() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}