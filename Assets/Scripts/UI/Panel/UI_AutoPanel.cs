using UnityEngine;

/// <summary>
/// �ڵ� ��ư�� �����ϴ� �ǳ�
/// </summary>
public class UI_AutoPanel : MonoBehaviour {
    //UI�̵� Ŭ����
    private UI_Movement movement;

    private void Awake() {
        movement = GetComponent<UI_Movement>();
    }

    /// <summary>
    /// Ȱ��ȭ �� UI �̵�
    /// </summary>
    public void Activation() {
        gameObject.SetActive(true);
        movement.Activation();
    }

    /// <summary>
    /// ��Ȱ��ȭ�� �̵� �� �ǳ� ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        movement.DeActivation(() => gameObject.SetActive(false));
    }
}