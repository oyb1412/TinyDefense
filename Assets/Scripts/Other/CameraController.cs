using UnityEngine;

public class CameraController : MonoBehaviour {
    private  Camera mainCamera;

    void Start() {
        mainCamera = GetComponent<Camera>();
        float targetAspect = 9.0f / 16.0f; // �⺻ �ػ� ���� (��: 1920x1080)
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f) {
            mainCamera.orthographicSize = mainCamera.orthographicSize / scaleHeight;
        }
    }
}