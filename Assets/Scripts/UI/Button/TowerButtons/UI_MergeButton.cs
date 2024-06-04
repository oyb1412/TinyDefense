using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타워 업그레이드 버튼 클래스
/// </summary>
public class UI_MergeButton : UI_Button, IUI_TowerButton {
    //선택 타워
    private TowerBase tower;
    //업그레이드 재료 타워
    public TowerBase TargetTower { get; private set; }
    //모든 자식 TMP및 버튼 오브젝트 관리 컴포넌트
    private TMPandButton tmpAndButton;
    private GameObject buildEffect;

    public override void Init() {
        buildEffect = Resources.Load<GameObject>(Define.EFFECT_TOWER_BUILD);
    }

    protected override void Awake() {
        base.Awake();
        tmpAndButton = GetComponent<TMPandButton>();
    }

    /// <summary>
    /// 머지 가능 여부 계산
    /// 셀 선택시 호출
    /// </summary>
    public void CheckMerge() {
        //머지 가능한 타워 유무 판단 후, 버튼 활성화 및 비활성화
        var towers = Managers.Tower.TowerList;
        foreach (var tower in towers) {
            if (this.tower == tower)
                continue;

            if (this.tower.TowerStatus.Level != tower.TowerStatus.Level)
                continue;

            if (this.tower.TowerStatus.TowerType != tower.TowerStatus.TowerType)
                continue;

            if (tower.StateMachine.GetState() == Define.TowerState.Movement)
                continue;

            TargetTower = tower;
            tmpAndButton.Activation();
            seletable = true;
            return;
        }
        seletable = false;
        TargetTower = null;
        tmpAndButton.DeActivation();
    }

    public void CheckAndMerge() {
        //머지 가능한 타워 유무 판단 후, 버튼 활성화 및 비활성화
        List<TowerBase> towers = new List<TowerBase>(Managers.Tower.TowerList);

        for (int i = 0; i < towers.Count; i++) {
            for (int j = i + 1; j < towers.Count; j++) {
                if (towers[i].TowerStatus.Level != towers[j].TowerStatus.Level)
                    continue;

                if (towers[i].TowerStatus.TowerType != towers[j].TowerStatus.TowerType)
                    continue;

                if (towers[i].StateMachine.GetState() == Define.TowerState.Movement)
                    continue;

                Select(towers[i], towers[j]);
            }
        }

        seletable = false;
        tmpAndButton.DeActivation();
    }

    public void Select(TowerBase baseTower, TowerBase materialTower) {
        GameObject go = Managers.Resources.Instantiate(buildEffect);
        go.transform.position = baseTower.TowerCell.transform.position;

        GameObject go2 = Managers.Resources.Instantiate(buildEffect);
        go2.transform.position = materialTower.TowerCell.transform.position;

        baseTower.DeSelect();

        //현재 타워 레벨업
        baseTower.TowerLevelup();

        //타겟 타워 파괴 
        materialTower.DestroyTower();

        UI_TowerDescription.Instance.DeActivation();
    }


    /// <summary>
    /// 버튼 선택시 타워 머지
    /// </summary>
    public override void Select() {
        GameObject go = Managers.Resources.Instantiate(buildEffect);
        go.transform.position = tower.TowerCell.transform.position;

        GameObject go2 = Managers.Resources.Instantiate(buildEffect);
        go2.transform.position = TargetTower.TowerCell.transform.position;

        tower.DeSelect();

        //현재 타워 레벨업
        tower.TowerLevelup();

        //타겟 타워 파괴 
        TargetTower.DestroyTower();

        UI_TowerDescription.Instance.DeActivation();
    }

    /// <summary>
    /// 버튼 비활성화
    /// </summary>
    public void DeActivation() {
        tower = null;
        seletable = false;
    }

    /// <summary>
    /// 버튼 활성화
    /// </summary>
    /// <param name="cell">선택한 셀</param>
    public void Activation(Cell cell, GameObject buildEffect) {
        if(!cell.IsUse()) {
            tmpAndButton.DeActivation();
            return;
        }
        tower = cell.Tower;
        CheckMerge();

        if (this.buildEffect == null)
            this.buildEffect = buildEffect;
    }
}