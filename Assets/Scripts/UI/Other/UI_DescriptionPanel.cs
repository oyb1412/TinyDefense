using TMPro;
using UnityEngine;

/// <summary>
/// �����Ƽ ���� ǥ�� �ǳ� Ŭ����
/// </summary>
public class UI_DescriptionPanel : MonoBehaviour {
    //�����Ƽ �̸� ǥ�� tmp
    private TextMeshProUGUI nameTMP;
    //�����Ƽ ���� ǥ�� tmp
    private TextMeshProUGUI descriptionTMP;

    private void Awake() {
        nameTMP = GetComponentsInChildren<TextMeshProUGUI>()[0];
        descriptionTMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �ǳ� Ȱ��ȭ
    /// </summary>
    /// <param name="abilityIcon">ǥ���� �����Ƽ ����</param>
    /// <param name="pos">ǥ���� ��ġ</param>
    public void ActivationDescription(string name, string description, Vector3 pos) 
    {
        gameObject.SetActive(true);
        transform.position = pos;
        nameTMP.text = name;
        descriptionTMP.text = description;
    }

    /// <summary>
    /// �ǳ� ��Ȱ��ȭ
    /// </summary>
    public void DeActivationDescription() {
        gameObject.SetActive(false);
    }
}