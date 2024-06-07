using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 스피드 증가 버튼
/// </summary>
public class UI_GameSpeedUpButton : UI_Button {
    //기본 스프라이트
    private Sprite defaultSpeedSprite;
    //속도 2배시 스프라이트
    private Sprite fastSpeedSprite;
    //스프라이트 저장용 이미지
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
    /// 버튼 선택시
    /// 게임 속도 조절
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
    /// 광고 시청으로 버튼 언락
    /// </summary>
    public void SetButton() {
        button.interactable = true;
        seletable = true;
    }
}