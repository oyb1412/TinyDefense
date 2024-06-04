using TMPro;
using UnityEngine;
using DG.Tweening;
public class UI_LoginButton : UI_Button {
    [SerializeField] private TMP_InputField emailIF;
    [SerializeField] private TMP_InputField passwordIF;
    [SerializeField] private TextMeshProUGUI logTMP;
    [SerializeField] private GameObject mainPanel;

    public override void Init() {
        
    }

    public override void Select() {
        if (emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Login(false);
            return;
        }
        if (emailIF.text.Length > 30 || passwordIF.text.Length > 20) {
            Login(false);
            return;
        }
        Managers.Auth.Login(emailIF.text, passwordIF.text, Login);
    }

    private void Login(bool success) {
        logTMP.text = success ? Define.MENT_SUCCESS_LOGIN : Define.MENT_FAIELD_LOGIN;
        if (success) {
            Managers.Data.SaveLoginData(emailIF.text, passwordIF.text);
            transform.parent.DOScale(0f, 0.5f).OnComplete(() => LoginComplete());
        }
        else
            Camera.main.transform.DOShakePosition(2f, .1f).SetEase(Ease.Linear);
    }

    private void LoginComplete() {
        transform.parent.gameObject.SetActive(false);
        mainPanel.SetActive(true);
        mainPanel.transform.localScale = Vector3.zero;
        mainPanel.transform.DOScale(1f, 0.5f);
    }
}