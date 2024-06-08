using UnityEngine;
public class LoginScene : BaseScene {
    //�α��� �ǳ�
    [SerializeField] private GameObject _loginPanel;
    //���� ���� ���� �ǳ�
    [SerializeField] private GameObject _mainPanel;
    
    /// <summary>
    /// �α��� �� �ʱ�ȭ
    /// </summary>
    public override void Init() {
        base.Init();
        if (!SoundManager.Instance.GetBGMPlaying())
            SoundManager.Instance.SetBgm(true, Define.BGMType.Main);

        if(Time.timeScale == 0) {
            Time.timeScale = 1f;
        }
        //�̹� �α����� ���¸� �ڵ� �α���
        if (Managers.Auth.User != null) {
            AutoLogin(true);
        } 
        else {
            var loginData = Managers.Data.LoadLoginData();
            //�α��� ������ ���������� �ڵ� �α���
            if (loginData != null) {
                Managers.Auth.Login(loginData.Email, loginData.Password, AutoLogin);
            } else {
                UI_Fade.Instance.DeActivationFade();
            }
        }

    }

    /// <summary>
    /// �ڵ� �α���
    /// </summary>
    /// <param name="trigger"></param>
    private void AutoLogin(bool trigger) {
        _loginPanel.SetActive(false);
        _mainPanel.SetActive(true);
        UI_Fade.Instance.DeActivationFade();
    }
}