using UnityEngine;
using static EnemyData;

public class MainScene : BaseScene {
 
    public override void Init() {
        if(Time.timeScale == 0f)
            Time.timeScale = 1f;

        var data = new EnemyStatusData() {
           Level = 1,
           MaxHp = 100,
           MaxHpUpVolume = 150,
           MoveSpeed = 5,
           Reward = 10,
        };
        Managers.FireStore.SaveDataToFirestoreDocument("test", "test", data);
        SoundManager.Instance.SetBgm(true, Define.BGMType.Main);
        base.Init();
        UI_Fade.Instance.DeActivationFade();
    }
}