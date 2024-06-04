using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ÿ�� ���׷��̵� ��ư Ŭ����
/// </summary>
public class UI_MergeButton : UI_Button, IUI_TowerButton {
    //���� Ÿ��
    private TowerBase tower;
    //���׷��̵� ��� Ÿ��
    public TowerBase TargetTower { get; private set; }
    //��� �ڽ� TMP�� ��ư ������Ʈ ���� ������Ʈ
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
    /// ���� ���� ���� ���
    /// �� ���ý� ȣ��
    /// </summary>
    public void CheckMerge() {
        //���� ������ Ÿ�� ���� �Ǵ� ��, ��ư Ȱ��ȭ �� ��Ȱ��ȭ
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
        //���� ������ Ÿ�� ���� �Ǵ� ��, ��ư Ȱ��ȭ �� ��Ȱ��ȭ
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

        //���� Ÿ�� ������
        baseTower.TowerLevelup();

        //Ÿ�� Ÿ�� �ı� 
        materialTower.DestroyTower();

        UI_TowerDescription.Instance.DeActivation();
    }


    /// <summary>
    /// ��ư ���ý� Ÿ�� ����
    /// </summary>
    public override void Select() {
        GameObject go = Managers.Resources.Instantiate(buildEffect);
        go.transform.position = tower.TowerCell.transform.position;

        GameObject go2 = Managers.Resources.Instantiate(buildEffect);
        go2.transform.position = TargetTower.TowerCell.transform.position;

        tower.DeSelect();

        //���� Ÿ�� ������
        tower.TowerLevelup();

        //Ÿ�� Ÿ�� �ı� 
        TargetTower.DestroyTower();

        UI_TowerDescription.Instance.DeActivation();
    }

    /// <summary>
    /// ��ư ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        tower = null;
        seletable = false;
    }

    /// <summary>
    /// ��ư Ȱ��ȭ
    /// </summary>
    /// <param name="cell">������ ��</param>
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