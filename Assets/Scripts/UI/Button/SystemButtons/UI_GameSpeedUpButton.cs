using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���ǵ� ���� ��ư
/// </summary>
public class UI_GameSpeedUpButton : UI_Button {
    //�⺻ ��������Ʈ
    private Sprite defaultSpeedSprite;
    //�ӵ� 2��� ��������Ʈ
    private Sprite fastSpeedSprite;
    //��������Ʈ ����� �̹���
    private Image iconImage;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
        iconImage = GetComponent<Image>();

        if(defaultSpeedSprite == null)
            defaultSpeedSprite = Resources.Load<Sprite>(Define.SPRITE_BUTTON_DEFAULT_SPEED);

        if(fastSpeedSprite == null)
            fastSpeedSprite = Resources.Load<Sprite>(Define.SPRITE_BUTTON_FASE_SPEED);

        button.interactable = false;
        seletable = false;
    }

    /// <summary>
    /// ��ư ���ý�
    /// ���� �ӵ� ����
    /// </summary>
    public override void Select() {
        var speed = Managers.Game.GameSpeed;

        if(speed == Define.GameSpeed.Default) {
            iconImage.sprite = fastSpeedSprite;
            Managers.Game.GameSpeed = Define.GameSpeed.Fast;
        }
        else {
            iconImage.sprite = defaultSpeedSprite;
            Managers.Game.GameSpeed = Define.GameSpeed.Default;
        }
    }

    /// <summary>
    /// ���� ��û���� ��ư ���
    /// </summary>
    public void SetButton() {
        button.interactable = true;
        seletable = true;
    }
}