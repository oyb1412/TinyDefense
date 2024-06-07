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
    //피벗 조정을 위한 렉트
    private RectTransform rect;
    //카메라 캐싱
    private Camera mainCamera;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        nameTMP = GetComponentsInChildren<TextMeshProUGUI>()[0];
        descriptionTMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        gameObject.SetActive(false);
        mainCamera = Camera.main;
    }

    /// <summary>
    /// 판넬 활성화
    /// </summary>
    /// <param name="abilityIcon">표기할 어빌리티 정보</param>
    /// <param name="pos">표기할 위치</param>
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
        // 캔버스 좌표를 화면 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, pos, mainCamera, out screenPos);

        if (screenPos.x < Screen.width / 2) {
            rect.pivot = new Vector2(0f, 0.5f);
        } else {
            rect.pivot = new Vector2(1f, 0.5f);
        }
    }

    /// <summary>
    /// 판넬 비활성화
    /// </summary>
    public void DeActivationDescription() {
        gameObject.SetActive(false);
    }
}