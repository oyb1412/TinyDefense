using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

/// <summary>
/// Ÿ�� ����, ����, �Ǹ� ��ư ���� Ŭ����
/// </summary>
public class UI_TowerBuild : MonoBehaviour {
    //Ÿ�� ����Ʈ
    protected GameObject buildEffect;
    //�� Ÿ�� ��ư
    private IUI_TowerButton[] buildButtons;
    //Ÿ�� ���� �� ���� ������ �̵� Ŭ����
    private UI_Movement movement;

    protected void Awake() {
        if(buildEffect == null) 
            buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);

        buildButtons = GetComponentsInChildren<IUI_TowerButton>();
        movement = GetComponent<UI_Movement>();
    }

    /// <summary>
    /// Ÿ�� ����
    /// UI �̵�
    /// </summary>
    /// <param name="cell"></param>
    public void Activation(Cell cell) {
        movement.Activation();
        foreach (var button in buildButtons) {
            button.Activation(cell);
        }
    }

    /// <summary>
    /// Ÿ�� ���� ����
    /// UI �̵�
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        movement.DeActivation(action);
        foreach (var button in buildButtons) {
            button.DeActivation();
        }
    }
}