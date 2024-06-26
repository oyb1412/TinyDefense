using UnityEngine;

/// <summary>
/// 각 탭 버튼 관리 클래스
/// </summary>
public class UI_TapButton : UI_Button {
    //각 탭 버튼
    private UI_Button[] tapButtons;
    //탭 버튼에 연동된 컨텐츠 판넬
    [SerializeField] private GameObject contentPanel;

    /// <summary>
    /// 초기화
    /// </summary>
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        tapButtons = transform.parent.GetComponentsInChildren<UI_TapButton>();
        seletable = true;
    }

    /// <summary>
    /// 버튼 선택
    /// 이미 선택되있는 탭을 선택시 return
    /// 모든 탭을 비활성화 후, 선택한 탭을 활성화
    /// </summary>
    public override void Select() {
        if (contentPanel.activeInHierarchy)
            return;

        foreach (UI_TapButton button in tapButtons) {
            button.SetButtonColor(Color.white);
            button.contentPanel.SetActive(false);
        }

        SetButtonColor(Color.gray);
        contentPanel.SetActive(true);
    }

    /// <summary>
    /// 활성화 및 비활성화 버튼 색 변경
    /// </summary>
    /// <param name="color">변경할 색</param>
    public void SetButtonColor(Color color) {
        button.image.color = color;
    }
}