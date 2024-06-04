using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    private bool isLoading;
    public override void Clear()
    {
    }

    protected override void Awake() {
        base.Awake();
        Managers.Pool.Init();
    }

    public override void Init() {
        base.Init();
        if (Time.timeScale == 0)
            Time.timeScale = 1f;

        Managers.Data.LoadGameData(CompletedLoading);
    }

    public void CompletedLoading() {
        Managers.Enhance.Init();
        Managers.Tower.Init();
        Managers.Enemy.Init();
        Managers.Skill.Init();
        Managers.Ability.Init();
        Managers.Select.Init();
        Managers.Spawn.SpawnStart();
        Managers.Game.Init();
        Managers.ADMob.Init();
        UI_Fade.Instance.DeActivationFade();
        isLoading = true;
    }

    private void Update() {
        if(isLoading) {
            Managers.Select.OnUpdate();
            Managers.Game.OnUpdate();
        }
    }
}
