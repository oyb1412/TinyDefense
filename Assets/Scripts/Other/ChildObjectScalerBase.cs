using System;
using UnityEngine;

/// <summary>
/// �ڽ� ������Ʈ ������ ���� Ŭ����
/// </summary>
public class ChildObjectScaler : MonoBehaviour {
    //���� ������
    private Vector3 originalScale;
    //������ ������ �Ͼ�� �θ� Ŭ����
    private ParentScaleEventHandler eventHandler;

    private void Awake() {
        originalScale = transform.localScale;
    }
    void Start() {
        eventHandler = GetComponentInParent<ParentScaleEventHandler>();
        eventHandler.OnScaleChanged += HandleScaleChanged;
    }

    private void OnEnable() {
        if (eventHandler != null)
            eventHandler.OnScaleChanged += HandleScaleChanged;
    }

    private void OnDisable() {
        if (eventHandler != null)
            eventHandler.OnScaleChanged -= HandleScaleChanged;

        transform.localScale = originalScale;
    }

    /// <summary>
    /// ������Ʈ�� �������� ���� �����Ϸ� ��ȯ
    /// </summary>
    private void HandleScaleChanged(Define.Direction dir) {
        if(dir == Define.Direction.Right)
            transform.localScale = originalScale;
        else if(dir == Define.Direction.Left)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }
}