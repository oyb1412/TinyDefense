using UnityEngine;
/// <summary>
/// 셋팅판넬 비활성화 버튼
/// </summary>
public class UI_ReturnButton : UI_Button {
    [SerializeField] private GameObject settingPanel;
    public override void Init() {
    }

    /// <summary>
    /// 버튼 선택 시
    /// 셋팅 판넬 비활성화
    /// 게임 재시작
    /// </summary>
    public override void Select() {
        settingPanel.SetActive(false);
        if(Managers.Instance != null) 
            Managers.Game.IsPlaying = true;
    }
}