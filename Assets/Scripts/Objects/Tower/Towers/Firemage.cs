using Unity.VisualScripting;

/// <summary>
/// 화염마법사 클래스
/// </summary>
public class Firemage : TowerBase {
    /// <summary>
    /// 초기화 및 화염 디버프 추가
    /// </summary>
    protected override void Init() {
        TowerType = Define.TowerType.Firemage;
        TowerBundle = Define.TowerBundle.Mage;
        AddDebuff();

        base.Init();
    }

    public override void TowerLevelup() {
        base.TowerLevelup();

        var debuff = Debuffs.Find(x => x.Type == Define.DebuffType.Fire);

        if (debuff == null)
            return;

        Debuffs.Remove(debuff);
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