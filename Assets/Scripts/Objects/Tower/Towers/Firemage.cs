using Unity.VisualScripting;

/// <summary>
/// ȭ�������� Ŭ����
/// </summary>
public class Firemage : TowerBase {
    /// <summary>
    /// �ʱ�ȭ �� ȭ�� ����� �߰�
    /// </summary>
    protected override void Init() {
        TowerType = Define.TowerType.Firemage;
        TowerBundle = Define.TowerBundle.Mage;
        base.Init();
        AddDebuff();
    }

    public override void TowerLevelup(int killCount) {
        base.TowerLevelup();

        foreach(var item in Debuffs) {
            if(item.Type == Define.DebuffType.Fire) {
                Debuffs.Remove(item);
                break;
            }
        }

        AddDebuff();
    }

    private void AddDebuff() {
        if (Managers.Tower.TowerData == null)
            return;

        float debuffValue = Managers.Tower.TowerData[(int)TowerType].FireValue + Managers.Tower.TowerData[(int)TowerType].FireValueUpValue * TowerLevel;
        float debufftime = Managers.Tower.TowerData[(int)TowerType].FireTime + Managers.Tower.TowerData[(int)TowerType].FireTimeUpValue * TowerLevel;
        SetDebuff(new FireDebuff(TowerStatus.AttackDamage * debuffValue, debufftime));
    }
}