using UnityEngine;

/// <summary>
/// 타워 자동 업그레이드 버튼
/// </summary>
public class UI_AutoUpgradeButton : UI_Button {
    [SerializeField]private UI_MergeButton mergeButton;

    public override void Init() {
        buttonSfxType = Define.SFXType.SelectTowerUIButton;
    }

    /// <summary>
    /// 버튼 선택시, 모든 타워 업그레이드 가능 여부 판단
    /// 가능 시 업그레이드
    /// </summary>
    public override void Select() {
        mergeButton.CheckAndMerge();
    }
}
