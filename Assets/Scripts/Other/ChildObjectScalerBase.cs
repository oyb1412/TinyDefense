using System;
using UnityEngine;

/// <summary>
/// 자식 오브젝트 스케일 보존 클래스
/// </summary>
public class ChildObjectScaler : MonoBehaviour {
    //원본 스케일
    private Vector3 originalScale;
    //원본 스케일(x flip)
    private Vector3 originalFlipScale;
    //스케일 변경이 일어나는 부모 클래스
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
    /// 활성화시 부모 객체 이벤트에 연결
    /// </summary>
    private void OnEnable() {
        if (eventHandler != null)
            eventHandler.OnScaleChanged += HandleScaleChanged;
    }

    /// <summary>
    /// 비활성화시 이벤트 해제
    /// </summary>
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
            transform.localScale = originalFlipScale;
    }
}