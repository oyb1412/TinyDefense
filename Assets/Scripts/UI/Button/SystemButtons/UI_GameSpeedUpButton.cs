using UnityEngine;
using UnityEngine.UI;

public class UI_GameSpeedUpButton : UI_Button {
    private Sprite defaultSpeedSprite;
    private Sprite faseSpeedSprite;
    private Image iconImage;
    public override void Init() {
        iconImage = GetComponent<Image>();
        defaultSpeedSprite = Resources.Load<Sprite>(Define.SPRITE_BUTTON_DEFAULT_SPEED);
        faseSpeedSprite = Resources.Load<Sprite>(Define.SPRITE_BUTTON_FASE_SPEED);
        //button.interactable = false;
        //seletable = false;
    }

    public override void Select() {
        var speed = Managers.Game.GameSpeed;

        if(speed == Define.GameSpeed.Default) {
            iconImage.sprite = faseSpeedSprite;
            Managers.Game.GameSpeed = Define.GameSpeed.Fast;
        }
        else {
            iconImage.sprite = defaultSpeedSprite;
            Managers.Game.GameSpeed = Define.GameSpeed.Default;
        }
    }

    public void SetButton() {
        button.interactable = true;
        seletable = true;
    }
}