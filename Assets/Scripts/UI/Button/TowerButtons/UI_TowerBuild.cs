using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

/// <summary>
/// Ÿ�� ����, ����, �Ǹ� ��ư ���� Ŭ����
/// </summary>
public class UI_TowerBuild : MonoBehaviour {
    protected GameObject buildEffect;
    private IUI_TowerButton[] buildButtons;
    private UI_Movement movement;

    protected void Awake() {
        buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);
        buildButtons = GetComponentsInChildren<IUI_TowerButton>();
        movement = GetComponent<UI_Movement>();
    }

    public void Activation(Cell cell) {
        movement.Activation();
        foreach (var button in buildButtons) {
            button.Activation(cell, buildEffect);
        }
    }

    public void DeActivation(UnityAction action) {
        movement.DeActivation(action);
        foreach (var button in buildButtons) {
            button.DeActivation();
        }
    }
}