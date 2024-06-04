public class Hunter : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Hunter;
        TowerBundle = Define.TowerBundle.Archer;
        base.Init();
    }
}