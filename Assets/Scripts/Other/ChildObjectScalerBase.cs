using System;
using UnityEngine;

/// <summary>
/// 자식 오브젝트 스케일 보존 클래스
/// </summary>
public class ChildObjectScaler : MonoBehaviour {
    //원본 스케일
    private Vector3 originalScale;
    //스케일 변경이 일어나는 부모 클래스
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
    /// 오브젝트의 스케일을 원본 스케일로 변환
    /// </summary>
    private void HandleScaleChanged(Define.Direction dir) {
        if(dir == Define.Direction.Right)
            transform.localScale = originalScale;
        else if(dir == Define.Direction.Left)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }
}