public class MainScene : BaseScene {
 
    public override void Init() {
        var qw = Managers.Data.DefineData;
        SoundManager.Instance.SetBgm(true, Define.BGMType.Main);
        base.Init();
        UI_Fade.Instance.DeActivationFade();
    }
}