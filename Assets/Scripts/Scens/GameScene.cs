using System.Collections;
using System.IO;
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
            UI_AutoCreateButton = GameObject.Find(Managers.Data.DefineData.TAG_AUTOCREATE_BUTTON).GetComponent<UI_AutoCreateButton>();
        }
        loadingSlider = GameObject.Find(Managers.Data.DefineData.TAG_LOADING_SLIDER).GetComponent<UI_LoadingSlider>();
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
            Debug.Log("리소스 폴더에서 게임 데이터 로드 및 풀링");
            StartCoroutine(LoadGameDataAndStartPoolAsync(callBack));
        } else {
            Debug.Log("이미 게임 데이터가 존재하므로 풀링만 진행");
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
        isLoading = true;
    }

    /// <summary>
    /// 비동기 풀링 오브젝트 생성
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator StartPoolAsync(UnityAction callback) {
        loadingSlider.SetLoading(.7f, callback);
        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();
        Debug.Log("풀링 완료");
        loadingSlider.SetLoading(1f, callback);
        SoundManager.Instance.SetBgm(true, Define.BGMType.Ingame);
    }

    /// <summary>
    /// 게임 데이터 비동기 로드
    /// 비동기 풀링 오브젝트 생성
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator LoadGameDataAndStartPoolAsync(UnityAction callback) {
        // JSON 파일 비동기로 로드
        Task<GameData> loadJsonTask = Managers.Data.DecryptionLoadJsonAsync<GameData>(Managers.Data.DefineData.TAG_GAME_DATA);
        loadingSlider.SetLoading(.6f, callback);

        // 풀링 오브젝트 비동기 생성
        yield return StartPoolManager.Instance.StartPoolAsync();
        Debug.Log("풀링 완료");

        loadingSlider.SetLoading(.7f, callback);

        while (!loadJsonTask.IsCompleted) {
            yield return null;
        }


        if (loadJsonTask.IsFaulted) {
            Debug.Log($"데이터 로드 실패{loadJsonTask.IsFaulted.ToString()}");

            string path = Path.Combine(Application.persistentDataPath, Managers.Data.DefineData.TAG_GAME_DATA_JSON);
            if (File.Exists(path)) {
                Debug.Log($"{path}데이터 삭제 성공");
                File.Delete(path);
            }
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
            yield break;
        }

        Debug.Log($"데이터 로드 완료{loadJsonTask.Result.ToString()}");

        Managers.Data.GameData = loadJsonTask.Result;

        Debug.Log($"게임 데이터에 데이터 저장. 애너미 체력 {Managers.Data.GameData.EnemyDatas.Enemys.MaxHp}");

        loadingSlider.SetLoading(1f, callback);
        SoundManager.Instance.SetBgm(true, Define.BGMType.Ingame);
    }

    private void Update() {
        if (isLoading) {
            Managers.Select.OnUpdate();
            Managers.Game.OnUpdate();
        }
    }
}
