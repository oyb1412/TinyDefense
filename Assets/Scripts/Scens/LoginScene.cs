using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class LoginScene : BaseScene {
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private GameObject _mainPanel;
    public override void Clear() {
    }

    public override void Init() {
        base.Init();
        if(Time.timeScale == 0) {
            Time.timeScale = 1f;
        }

        if (Managers.Auth.User != null) {
            AutoLogin(true);
        } 
        else {
            var loginData = Managers.Data.LoadLoginData();
            if (loginData != null) {
                Managers.Auth.Login(loginData.Email, loginData.Password, AutoLogin);
            } else {
                UI_Fade.Instance.DeActivationFade();
            }
        }

    }


    private void AutoLogin(bool trigger) {
        _loginPanel.SetActive(false);
        _mainPanel.SetActive(true);
        UI_Fade.Instance.DeActivationFade();
    }
}