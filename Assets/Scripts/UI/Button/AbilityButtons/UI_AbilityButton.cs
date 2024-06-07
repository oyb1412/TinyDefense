using UnityEngine;

/// <summary>
/// 각 어빌리티 선택 버튼 클래스
/// </summary>
public class UI_AbilityButton : UI_Button {
    //현 오브젝트의 어빌리티 타입
    [SerializeField] private Define.AbilityType AbilityType;
    //어빌리티 목록 판넬
    private UI_SelectAbility selectAbilityUI;

    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        selectAbilityUI = GetComponentInParent<UI_SelectAbility>();
        seletable = true;
    }

    /// <summary>
    /// 어빌리티 선택 및
    /// 보유 어빌리티 목록에 어빌리티 추가
    /// </summary>
    public override void Select() {
        IAbility ability = AbilityFactory.CreateAbility(AbilityType);

        if (ability == null)
            return;

        selectAbilityUI.DeActivation();
        Managers.Ability.AddAbility(ability);
    }
}