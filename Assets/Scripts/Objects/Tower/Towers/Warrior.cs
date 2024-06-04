public class Warrior : TowerBase {
    protected override void Init() {
        TowerType = Define.TowerType.Warrior;
        TowerBundle = Define.TowerBundle.Soldier;
        base.Init();
    }
}