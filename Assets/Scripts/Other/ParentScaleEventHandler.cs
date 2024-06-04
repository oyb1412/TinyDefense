using UnityEngine;
using System;

/// <summary>
/// 스케일 변경이 일어나는 부모 클래스
/// </summary>
public class ParentScaleEventHandler : MonoBehaviour {
    public Action<Define.Direction> OnScaleChanged;

    /// <summary>
    /// 스케일 변경 및 이벤트 호출
    /// </summary>
    /// <param name="right">변경 위치</param>
    /// <param name="trans">변경할 트랜스폼</param>
    public void ChangeScale(Define.Direction dir, Transform trans) {
        Util.ChangeFlip(trans, dir);
        OnScaleChanged?.Invoke(dir);
    }
}