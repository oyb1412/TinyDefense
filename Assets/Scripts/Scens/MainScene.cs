using UnityEngine;

public class MainScene : BaseScene {
 
    public override void Init() {
        if(Time.timeScale == 0f)
            Time.timeScale = 1f;

        SoundManager.Instance.SetBgm(true, Define.BGMType.Main);
        base.Init();
        UI_Fade.Instance.DeActivationFade();
    }
}