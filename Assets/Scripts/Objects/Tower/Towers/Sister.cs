using System.Linq;
using UnityEngine;

/// <summary>
/// �ý��� Ÿ�� Ŭ����
/// </summary>
public class Sister : TowerBase {
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    protected override void Init() {
        TowerType = Define.TowerType.Sister;
        TowerBundle = Define.TowerBundle.Mage;
        base.Init();
    }

    /// <summary>
    /// ����ü �߻� �� ���� ����
    /// </summary>
    public override void FireProjectile() {
        TowerBuff();
        if (Util.IsEnemyNull(TargetEnemy)) {
            StateMachine.ChangeState(Define.TowerState.Idle);
            return;
        }
        base.FireProjectile();
    }

    /// <summary>
    /// ������ �ֺ� Ÿ������ ������ ���� ����
    /// </summary>
    private void TowerBuff() {
        foreach (var item in Managers.Tower.TowerList) {
            if (!item.gameObject.activeInHierarchy)
                continue;

            if (item.TowerCell == null)
                continue;

            if(Vector2.Distance(TowerCell.transform.position, item.TowerCell.transform.position) <= TowerStatus.AttackRange / 3
                && item.gameObject.activeInHierarchy) {
                int ran = Random.Range(0, 2);
                
                if(ran == 0) {
                    float buffValue = Managers.Tower.TowerData[(int)TowerType].BuffValue + Managers.Tower.TowerData[(int)TowerType].BuffValueUpValue * TowerLevel;
                    float bufftime = Managers.Tower.TowerData[(int)TowerType].BuffTime + Managers.Tower.TowerData[(int)TowerType].BuffTimeUpValue * TowerLevel;
                    item.BuffManager.AddBuff(new AttackDamageBuff(buffValue, bufftime), item);
                }
                else {
                    float buffValue = Managers.Tower.TowerData[(int)TowerType].BuffValue + Managers.Tower.TowerData[(int)TowerType].BuffValueUpValue * TowerLevel;
                    float bufftime = Managers.Tower.TowerData[(int)TowerType].BuffTime + Managers.Tower.TowerData[(int)TowerType].BuffTimeUpValue * TowerLevel;
                    item.BuffManager.AddBuff(new AttackDelayBuff(buffValue, bufftime), item);
                }

                return;
            }
        }
    }
}