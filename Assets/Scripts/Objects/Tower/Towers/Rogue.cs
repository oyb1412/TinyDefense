public class Rogue : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Rogue;
        TowerBundle = Define.TowerBundle.Soldier;
        base.Init();
    }
}