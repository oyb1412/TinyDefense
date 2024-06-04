public class Ranger : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Ranger;
        TowerBundle = Define.TowerBundle.Archer;
        base.Init();
    }
}