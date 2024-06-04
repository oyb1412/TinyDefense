public class Knight : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Knight;
        TowerBundle = Define.TowerBundle.Soldier;
        base.Init();
    }
}