/// <summary>
/// �����Ƽ �ʱ�ȭ ��ư Ŭ����
/// </summary>
public class UI_AbilityResetButton : UI_Button {
    private UI_SelectAbility selectAbility;
    public override void Init() {
        selectAbility = GetComponentInParent<UI_SelectAbility>();
    }

    /// <summary>
    /// �����Ƽ �ʱ�ȭ
    /// ���� �ε�
    /// </summary>
    public override void Select() {
        Managers.ADMob.ShowRewardedAd(selectAbility.ReActivation);
    }
}