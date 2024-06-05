using System;
using UnityEngine;

/// <summary>
/// �ڽ� ������Ʈ ������ ���� Ŭ����
/// </summary>
public class ChildObjectScaler : MonoBehaviour {
    //���� ������
    private Vector3 originalScale;
    //���� ������(x flip)
    private Vector3 originalFlipScale;
    //������ ������ �Ͼ�� �θ� Ŭ����
    private ParentScaleEventHandler eventHandler;

    private void Awake() {
        originalScale = transform.localScale;
        originalFlipScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Start() {
        eventHandler = GetComponentInParent<ParentScaleEventHandler>();
        eventHandler.OnScaleChanged += HandleScaleChanged;
    }

    /// <summary>
    /// Ȱ��ȭ�� �θ� ��ü �̺�Ʈ�� ����
    /// </summary>
    private void OnEnable() {
        if (eventHandler != null)
            eventHandler.OnScaleChanged += HandleScaleChanged;
    }

    /// <summary>
    /// ��Ȱ��ȭ�� �̺�Ʈ ����
    /// </summary>
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
            transform.localScale = originalFlipScale;
    }
}