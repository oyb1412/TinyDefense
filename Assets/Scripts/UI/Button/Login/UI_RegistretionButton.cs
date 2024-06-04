using TMPro;
using UnityEngine;
using DG.Tweening;

public class UI_RegistretionButton : UI_Button {
    [SerializeField] private TMP_InputField emailIF;
    [SerializeField] private TMP_InputField passwordIF;
    [SerializeField] private TextMeshProUGUI logTMP;
    public override void Init() {
        
    }

    public override void Select() {
        if(emailIF.text.Length == 0 || passwordIF.text.Length == 0) {
            Registretion(false);
            return;
        }
        if(emailIF.text.Length > 30 ||  passwordIF.text.Length > 20) {
            Registretion(false);
            return;
        }
        Managers.Auth.Registretion(emailIF.text, passwordIF.text, Registretion);
    }

    private void Registretion(bool success) {
        logTMP.text = success ? Define.MENT_SUCCESS_REGISTRETION : Define.MENT_FAIELD_REGISTRETION;
        if (!success)
            Camera.main.transform.DOShakePosition(2f, .1f).SetEase(Ease.Linear);
    }
}
