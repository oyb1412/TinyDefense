using UnityEngine;

/// <summary>
/// Ÿ�� ����, �Ǹ�, ���׷��̵� ����Ʈ
/// </summary>
public class BuildEffect : MonoBehaviour {
    /// <summary>
    /// �̺�Ʈ���� �ݹ����� ȣ��
    /// </summary>
    public void DestroyEvent() {
        Managers.Resources.Release(gameObject);
    }
}