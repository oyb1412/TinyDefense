using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    private UI_LoadingSlider loadingSlider;
    private bool isLoading;
    public UI_AutoCreateButton UI_AutoCreateButton { get; private set; }

    public override void Clear()
    {
    }

    protected override void Awake() {
        base.Awake();
        if (UI_AutoCreateButton == null) {
            UI_AutoCreateButton = GameObject.Find(Define.TAG_AUTOCREATE_BUTTON).GetComponent<UI_AutoCreateButton>();
        }
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<UI_LoadingSlider>();
        Managers.Pool.Init();
        Managers.Game.Clear();
        Managers.Enemy.Clear();
    }

    public override void Init() {
        base.Init();
        if (Time.timeScale == 0)
            Time.timeScale = 1f;

        UI_Fade.Instance.DeActivationFade();
        
        LoadGameData(() => UI_Fade.Instance.ActivationAndDeActivationFade(CompletedLoading));
    }

    public void CompletedLoading() {
        loadingSlider.transform.parent.gameObject.SetActive(false);
        Managers.Enhance.Init();
        Managers.Tower.Init();
        Managers.Enemy.Init();
        Managers.Skill.Init();
        Managers.Ability.Init();
        Managers.Select.Init();
        Managers.Spawn.SpawnStart();
        Managers.Game.Init();
        Managers.ADMob.Init();
        isLoading = true;
    }

    public void LoadGameData(UnityAction callBack) {
        if (Managers.Data.GameData == null) {
            Managers.Data.GameData = new GameData();
            StartCoroutine(LoadGameDataAndStartPoolAsync(callBack));
        } else {
            StartCoroutine(StartPoolAsync(callBack));
        }
    }

    private IEnumerator StartPoolAsync(UnityAction callback) {
        loadingSlider.SetLoading(.9f, callback);
        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();
        loadingSlider.SetLoading(1f, callback);
    }

    private IEnumerator LoadGameDataAndStartPoolAsync(UnityAction callback) {
        // JSON 파일 비동기로 로드
        Task<GameData> loadJsonTask = Managers.Data.LoadJsonAsync<GameData>("GameData");
        loadingSlider.SetLoading(.6f, callback);

        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();

        loadingSlider.SetLoading(.7f, callback);

        while (!loadJsonTask.IsCompleted) {
            yield return null;
        }
        Managers.Data.GameData = loadJsonTask.Result;
        loadingSlider.SetLoading(1f, callback);
    }

    private void Update() {
        if (isLoading) {
            Managers.Select.OnUpdate();
            Managers.Game.OnUpdate();
        }
    }
}
