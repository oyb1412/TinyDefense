public class Sniper : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Sniper;
        TowerBundle = Define.TowerBundle.Archer;
        base.Init();
    }
}