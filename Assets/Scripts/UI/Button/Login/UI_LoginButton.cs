using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �α��� ��ư Ŭ����
/// </summary>
public class UI_LoginButton : UI_Button {
    //�̸��� ��ǲ�ʵ�
    [SerializeField] private TMP_InputField emailIF;
    //��й�ȣ ��ǲ�ʵ�
    [SerializeField] private TMP_InputField passwordIF;
    //�α��� / ���� ���¸� ǥ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI logTMP;
    [SerializeField] private GameObject mainPanel;

    public override void Init() {
        
    }

    /// <summary>
    /// �α��� ��ư ���ý�
    /// </summary>
    public override void Select() {
        //���Է½� �α��� ����
        if (emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Login(false);
            return;
        }
        //���ڿ��� �ʹ� �� �� �α��� ����
        if (emailIF.text.Length > Define.MAX_EMAIL_LENGTH || passwordIF.text.Length > Define.MAX_PASSWORD_LENGTH) {
            Login(false);
            return;
        }
        Managers.Auth.Login(emailIF.text, passwordIF.text, Login);
    }

    /// <summary>
    /// �α��� ���� & ����
    /// </summary>
    /// <param name="success">����?</param>
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
    /// �α��� ���� �� UI ũ�� ������ 
    /// ���� �ǳ� Ȱ��ȭ
    /// </summary>
    private void LoginComplete() {
        transform.parent.gameObject.SetActive(false);
        mainPanel.SetActive(true);
        mainPanel.transform.localScale = Vector3.zero;
        mainPanel.transform.DOScale(1f, 0.5f);
    }
}