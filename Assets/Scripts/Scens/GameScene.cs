using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    //인게임 로딩 바
    private UI_LoadingSlider loadingSlider;
    //로딩 중인가?
    private bool isLoading;
    //자동 생성 버튼 
    public UI_AutoCreateButton UI_AutoCreateButton { get; private set; }

    protected override void Awake() {
        base.Awake();
        if (UI_AutoCreateButton == null) {
            UI_AutoCreateButton = GameObject.Find(Define.TAG_AUTOCREATE_BUTTON).GetComponent<UI_AutoCreateButton>();
        }
        loadingSlider = GameObject.Find(Define.TAG_LOADING_SLIDER).GetComponent<UI_LoadingSlider>();
        Managers.Pool.Init();
        Managers.Game.Clear();
        Managers.Enemy.Clear();
        Managers.Tower.Clear();
    }

    /// <summary>
    /// 초기화
    /// 데이터 로드 및 페이드
    /// </summary>
    public override void Init() {
        base.Init();
        if (Time.timeScale == 0)
            Time.timeScale = 1f;

        UI_Fade.Instance.DeActivationFade();

        LoadGameData(() => UI_Fade.Instance.ActivationAndDeActivationFade(CompletedLoading));
    }

    /// <summary>
    /// 데이터가 없을시 데이터 로드
    /// 데이터가 존재할 시 풀링만 진행
    /// </summary>
    /// <param name="callBack"></param>
    public void LoadGameData(UnityAction callBack) {
        if (Managers.Data.GameData == null) {
            Managers.Data.GameData = new GameData();
            StartCoroutine(LoadGameDataAndStartPoolAsync(callBack));
        } else {
            StartCoroutine(StartPoolAsync(callBack));
        }
    }

    /// <summary>
    /// 데이터 로드 및 풀링이 끝나면 
    /// 데이터로 객체들 초기화 후 게임 시작
    /// </summary>
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

    /// <summary>
    /// 비동기 풀링 오브젝트 생성
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator StartPoolAsync(UnityAction callback) {
        loadingSlider.SetLoading(.9f, callback);
        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();
        var qw = Managers.Data.GameData;
        loadingSlider.SetLoading(1f, callback);
    }

    /// <summary>
    /// 게임 데이터 비동기 로드
    /// 비동기 풀링 오브젝트 생성
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
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
