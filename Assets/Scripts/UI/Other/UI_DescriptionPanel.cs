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
    //�ǹ� ������ ���� ��Ʈ
    private RectTransform rect;
    //ī�޶� ĳ��
    private Camera mainCamera;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        nameTMP = GetComponentsInChildren<TextMeshProUGUI>()[0];
        descriptionTMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        gameObject.SetActive(false);
        mainCamera = Camera.main;
    }

    /// <summary>
    /// �ǳ� Ȱ��ȭ
    /// </summary>
    /// <param name="abilityIcon">ǥ���� �����Ƽ ����</param>
    /// <param name="pos">ǥ���� ��ġ</param>
    public void ActivationDescription(string name, string description, Vector3 pos) 
    {
        if(!gameObject.activeInHierarchy) {
            gameObject.SetActive(true);

            UpdatePivotBasedOnPosition(pos);

            transform.position = pos;
            nameTMP.text = name;
            descriptionTMP.text = description;
        }
    }

    private void UpdatePivotBasedOnPosition(Vector3 pos) {
        Vector2 screenPos;
        // ĵ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pos, mainCamera, out screenPos);

        if (screenPos.x < Screen.width / 2) {
            rect.pivot = new Vector2(0f, 0.5f);
        } else {
            rect.pivot = new Vector2(1f, 0.5f);
        }
    }

    /// <summary>
    /// �ǳ� ��Ȱ��ȭ
    /// </summary>
    public void DeActivationDescription() {
        gameObject.SetActive(false);
    }
}