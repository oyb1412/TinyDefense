using System.Collections;
using UnityEngine;
public class MainScene : BaseScene {
    public override void Clear() {
    }

    public override void Init() {
        base.Init();
        UI_Fade.Instance.DeActivationFade();
    }
}