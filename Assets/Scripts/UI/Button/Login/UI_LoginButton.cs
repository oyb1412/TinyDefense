using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 로그인 버튼 클래스
/// </summary>
public class UI_LoginButton : UI_Button {
    //이메일 인풋필드
    [SerializeField] private TMP_InputField emailIF;
    //비밀번호 인풋필드
    [SerializeField] private TMP_InputField passwordIF;
    //로그인 / 가입 상태를 표기할 텍스트
    [SerializeField] private TextMeshProUGUI logTMP;
    [SerializeField] private GameObject mainPanel;

    public override void Init() {
        
    }

    /// <summary>
    /// 로그인 버튼 선택시
    /// </summary>
    public override void Select() {
        //미입력시 로그인 실패
        if (emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Login(false);
            return;
        }
        //문자열이 너무 길 시 로그인 실패
        if (emailIF.text.Length > Define.MAX_EMAIL_LENGTH || passwordIF.text.Length > Define.MAX_PASSWORD_LENGTH) {
            Login(false);
            return;
        }
        Managers.Auth.Login(emailIF.text, passwordIF.text, Login);
    }

    /// <summary>
    /// 로그인 성공 & 실패
    /// </summary>
    /// <param name="success">성공?</param>
    private void Login(bool success) {
        logTMP.text = success ? Define.MENT_SUCCESS_LOGIN : Define.MENT_FAIELD_LOGIN;
        if (success) {
            Managers.Data.SaveLoginData(emailIF.text, passwordIF.text);
            transform.parent.DOScale(0f, 0.5f).OnComplete(() => LoginComplete());
        }
        else
            Camera.main.transform.DOShakePosition(2f, .1f).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 로그인 성공 시 UI 크기 조절로 
    /// 메인 판넬 활성화
    /// </summary>
    private void LoginComplete() {
        transform.parent.gameObject.SetActive(false);
        mainPanel.SetActive(true);
        mainPanel.transform.localScale = Vector3.zero;
        mainPanel.transform.DOScale(1f, 0.5f);
    }
}