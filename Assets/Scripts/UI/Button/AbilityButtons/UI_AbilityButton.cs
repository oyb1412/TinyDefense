using UnityEngine;

/// <summary>
/// �� �����Ƽ ���� ��ư Ŭ����
/// </summary>
public class UI_AbilityButton : UI_Button {
    //�� ������Ʈ�� �����Ƽ Ÿ��
    [SerializeField] private Define.AbilityType AbilityType;
    //�����Ƽ ��� �ǳ�
    private UI_SelectAbility selectAbilityUI;

    public override void Init() {
        selectAbilityUI = GetComponentInParent<UI_SelectAbility>();
        seletable = true;
    }

    /// <summary>
    /// �����Ƽ ���� ��
    /// ���� �����Ƽ ��Ͽ� �����Ƽ �߰�
    /// </summary>
    public override void Select() {
        IAbility ability = AbilityFactory.CreateAbility(AbilityType);

        if (ability == null)
            return;

        selectAbilityUI.DeActivation();
        Managers.Ability.AddAbility(ability);
    }
}