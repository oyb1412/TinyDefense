using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

/// <summary>
/// 타워 생성, 머지, 판매 버튼 관리 클래스
/// </summary>
public class UI_TowerBuild : MonoBehaviour {
    //타워 이펙트
    protected GameObject buildEffect;
    //각 타워 버튼
    private IUI_TowerButton[] buildButtons;
    //타워 선택 미 선택 해제시 이동 클래스
    private UI_Movement movement;

    protected void Awake() {
        if(buildEffect == null) 
            buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);

        buildButtons = GetComponentsInChildren<IUI_TowerButton>();
        movement = GetComponent<UI_Movement>();
    }

    /// <summary>
    /// 타워 선택
    /// UI 이동
    /// </summary>
    /// <param name="cell"></param>
    public void Activation(Cell cell) {
        movement.Activation();
        foreach (var button in buildButtons) {
            button.Activation(cell);
        }
    }

    /// <summary>
    /// 타워 선택 해제
    /// UI 이동
    /// </summary>
    /// <param name="action"></param>
    public void DeActivation(UnityAction action) {
        movement.DeActivation(action);
        foreach (var button in buildButtons) {
            button.DeActivation();
        }
    }
}