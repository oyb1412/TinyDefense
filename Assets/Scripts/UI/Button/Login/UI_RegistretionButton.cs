using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 회원가입 버튼
/// </summary>
public class UI_RegistretionButton : UI_Button {
    //이메일 인풋필드
    [SerializeField] private TMP_InputField emailIF;
    //비밀번호 인풋필드
    [SerializeField] private TMP_InputField passwordIF;
    //회원가입 상태 표기 텍스트
    [SerializeField] private TextMeshProUGUI logTMP;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    //회원가입 시도
    public override void Select() {
        //미입력시 회원가입 실패
        if(emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Registretion(false);
            return;
        }
        //문자열이 너무 길 시 실패
        if(emailIF.text.Length > Define.MAX_EMAIL_LENGTH ||  passwordIF.text.Length > Define.MAX_PASSWORD_LENGTH) {
            Registretion(false);
            return;
        }

        //파이어베이스 auth에 회원가입 전송
        Managers.Auth.Registretion(emailIF.text, passwordIF.text, Registretion);
    }

    private void Registretion(bool success) {
        logTMP.text = success ? Define.MENT_SUCCESS_REGISTRETION : Define.MENT_FAIELD_REGISTRETION;
        if (!success)
            Camera.main.transform.DOShakePosition(2f, .1f).SetEase(Ease.Linear);
    }
}
