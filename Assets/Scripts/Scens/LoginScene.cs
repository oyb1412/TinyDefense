using UnityEngine;
public class LoginScene : BaseScene {
    //로그인 판넬
    [SerializeField] private GameObject _loginPanel;
    //게임 시작 메인 판넬
    [SerializeField] private GameObject _mainPanel;
    
    /// <summary>
    /// 로그인 씬 초기화
    /// </summary>
    public override void Init() {
        base.Init();
        if (!SoundManager.Instance.GetBGMPlaying())
            SoundManager.Instance.SetBgm(true, Define.BGMType.Main);

        if(Time.timeScale == 0) {
            Time.timeScale = 1f;
        }
        //이미 로그인한 상태면 자동 로그인
        if (Managers.Auth.User != null) {
            AutoLogin(true);
        } 
        else {
            var loginData = Managers.Data.LoadLoginData();
            //로그인 정보가 남아있을시 자동 로그인
            if (loginData != null) {
                Managers.Auth.Login(loginData.Email, loginData.Password, AutoLogin);
            } else {
                UI_Fade.Instance.DeActivationFade();
            }
        }

    }

    /// <summary>
    /// 자동 로그인
    /// </summary>
    /// <param name="trigger"></param>
    private void AutoLogin(bool trigger) {
        _loginPanel.SetActive(false);
        _mainPanel.SetActive(true);
        UI_Fade.Instance.DeActivationFade();
    }
}