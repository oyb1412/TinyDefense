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

    public void Loading(UnityAction callBack) {
        StartCoroutine(Co_GetKeyAndLoad(callBack));
        //StartCoroutine(Co_GetKeyAndSave());
    }

    private IEnumerator Co_GetKeyAndSave() {
        //Managers.FireStore.GetKeyData(Managers.Data.GetKey);
        yield return new WaitUntil(() => Managers.Data.Key != null);
        Managers.Data.SaveData();
    }

    private IEnumerator Co_GetKeyAndLoad(UnityAction callBack) {
        //Managers.FireStore.GetKeyData(Managers.Data.GetKey);
        yield return new WaitUntil(() => Managers.Data.Key != null);
        Managers.Data.LoadData(callBack);
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

    public override void Init() {
        base.Init();
        if (Time.timeScale == 0)
            Time.timeScale = 1f;
        Loading(CompletedLoading);
    }

    private void Update() {
        if(isLoading) {
            Managers.Select.OnUpdate();
            Managers.Game.OnUpdate();
        }
    }
}
