/// <summary>
/// 얼음 마법사 클래스
/// </summary>
public class Icemage : TowerBase {
    /// <summary>
    /// 초기화 및 슬로우 디버프 추가
    /// </summary>
    protected override void Init() {
        TowerType = Define.TowerType.Icemage;
        TowerBundle = Define.TowerBundle.Mage;

        AddDebuff();

        base.Init();
    }

    public override void TowerLevelup() {
        base.TowerLevelup();

        var debuff = Debuffs.Find(x => x.Type == Define.DebuffType.Slow);

        if (debuff == null)
            return;

        Debuffs.Remove(debuff);
        AddDebuff();
    }

    private void AddDebuff() {
        if (Managers.Tower.TowerData == null)
            return;

        float debuffValue = Managers.Tower.TowerData[(int)TowerType].SlowValue + Managers.Tower.TowerData[(int)TowerType].SlowValueUpValue * TowerLevel;
        float debufftime = Managers.Tower.TowerData[(int)TowerType].SlowTime + Managers.Tower.TowerData[(int)TowerType].SlowTimeUpValue * TowerLevel;
        SetDebuff(new SlowDebuff(debuffValue, debufftime));
    }
}