using TMPro;
using UnityEngine;

/// <summary>
/// 각 강화 선택 버튼 클래스
/// </summary>
public class UI_EnhanceButton : UI_Button
{
    //오브젝트의 타워 번들 타입
    [SerializeField] private Define.TowerBundle bundleType;
    //오브젝트의 강화 타입
    [SerializeField] private Define.EnhanceType enhanceType;
    //강화 레벨 표기 텍스트
    [SerializeField] private TextMeshProUGUI levelText;
    //강화에 필요한 코스트 표기 텍스트
    [SerializeField] private TextMeshProUGUI costText;

    /// <summary>
    /// 초기화
    /// 버튼 활성화 여부 판단
    /// 각 텍스트에 기본값 대입
    /// </summary>
    public override void Init() {
        Managers.Game.CurrentGoldAction += ButtonActive;
        ButtonActive(Managers.Game.CurrentGold);

        levelText.text = GetLevelText(0);
    }

    /// <summary>
    /// 인핸스 골드 부족시 버튼 비활성화
    /// </summary>
    /// <param name="gold">현재 골드</param>
    private void ButtonActive(int gold) {
        if (Managers.Enhance == null)
            return;

        if (Managers.Enhance.EnhanceData == null)
            return;

        var enhance = Managers.Enhance.GetEnhanceValue(bundleType, enhanceType);

        if (enhance == null)
            return;

        if (gold < enhance.EnhanceCost) {
            button.interactable = false;
            seletable = false;
        } else {
            button.interactable = true;
            seletable = true;
        }
    }

    /// <summary>
    /// 인핸스 설명 + 레벨 텍스트 추출
    /// </summary>
    /// <param name="level">레벨</param>
    private string GetLevelText(int level) {
        return string.Format(Define.MENT_TOWER_ENHANCE_LEVEL[(int)enhanceType], level);
    }

    /// <summary>
    /// 버튼 선택(인핸스)
    /// 인핸스 레벨 증가 및 텍스트 변경
    /// 비용 지불
    /// </summary>
    public override void Select() {
        var enhance = Managers.Enhance.GetEnhanceValue(bundleType, enhanceType);
        Managers.Game.CurrentGold -= enhance.EnhanceCost;
        Managers.Enhance.SetEnhanceValue(bundleType, enhanceType);

        levelText.text = GetLevelText(enhance.Level);
        costText.text = string.Format(Define.MENT_GOLD, enhance.EnhanceCost);

        ButtonActive(Managers.Game.CurrentGold);
    }
}
