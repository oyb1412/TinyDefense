using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ȸ������ ��ư
/// </summary>
public class UI_RegistretionButton : UI_Button {
    //�̸��� ��ǲ�ʵ�
    [SerializeField] private TMP_InputField emailIF;
    //��й�ȣ ��ǲ�ʵ�
    [SerializeField] private TMP_InputField passwordIF;
    //ȸ������ ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI logTMP;
    public override void Init() {
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    //ȸ������ �õ�
    public override void Select() {
        //���Է½� ȸ������ ����
        if(emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Registretion(false);
            return;
        }
        //���ڿ��� �ʹ� �� �� ����
        if(emailIF.text.Length > Define.MAX_EMAIL_LENGTH ||  passwordIF.text.Length > Define.MAX_PASSWORD_LENGTH) {
            Registretion(false);
            return;
        }

        //���̾�̽� auth�� ȸ������ ����
        Managers.Auth.Registretion(emailIF.text, passwordIF.text, Registretion);
    }

    private void Registretion(bool success) {
        logTMP.text = success ? Define.MENT_SUCCESS_REGISTRETION : Define.MENT_FAIELD_REGISTRETION;
        if (!success)
            Camera.main.transform.DOShakePosition(2f, .1f).SetEase(Ease.Linear);
    }
}
