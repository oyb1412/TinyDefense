using UnityEngine;
using System;

/// <summary>
/// ������ ������ �Ͼ�� �θ� Ŭ����
/// </summary>
public class ParentScaleEventHandler : MonoBehaviour {
    public Action<Define.Direction> OnScaleChanged;

    /// <summary>
    /// ������ ���� �� �̺�Ʈ ȣ��
    /// </summary>
    /// <param name="right">���� ��ġ</param>
    /// <param name="trans">������ Ʈ������</param>
    public void ChangeScale(Define.Direction dir, Transform trans) {
        Util.ChangeFlip(trans, dir);
        OnScaleChanged?.Invoke(dir);
    }
}