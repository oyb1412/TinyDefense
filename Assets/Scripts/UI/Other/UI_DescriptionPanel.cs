using TMPro;
using UnityEngine;

/// <summary>
/// 어빌리티 정보 표기 판넬 클래스
/// </summary>
public class UI_DescriptionPanel : MonoBehaviour {
    //어빌리티 이름 표기 tmp
    private TextMeshProUGUI nameTMP;
    //어빌리티 설명 표기 tmp
    private TextMeshProUGUI descriptionTMP;

    private void Awake() {
        nameTMP = GetComponentsInChildren<TextMeshProUGUI>()[0];
        descriptionTMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 판넬 활성화
    /// </summary>
    /// <param name="abilityIcon">표기할 어빌리티 정보</param>
    /// <param name="pos">표기할 위치</param>
    public void ActivationDescription(string name, string description, Vector3 pos) 
    {
        gameObject.SetActive(true);
        transform.position = pos;
        nameTMP.text = name;
        descriptionTMP.text = description;
    }

    /// <summary>
    /// 판넬 비활성화
    /// </summary>
    public void DeActivationDescription() {
        gameObject.SetActive(false);
    }
}