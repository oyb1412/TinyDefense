using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    //�ΰ��� �ε� ��
    private UI_LoadingSlider loadingSlider;
    //�ε� ���ΰ�?
    private bool isLoading;
    //�ڵ� ���� ��ư 
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
    /// �ʱ�ȭ
    /// ������ �ε� �� ���̵�
    /// </summary>
    public override void Init() {
        base.Init();
        if (Time.timeScale == 0)
            Time.timeScale = 1f;

        UI_Fade.Instance.DeActivationFade();

        LoadGameData(() => UI_Fade.Instance.ActivationAndDeActivationFade(CompletedLoading));
    }

    /// <summary>
    /// �����Ͱ� ������ ������ �ε�
    /// �����Ͱ� ������ �� Ǯ���� ����
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
    /// ������ �ε� �� Ǯ���� ������ 
    /// �����ͷ� ��ü�� �ʱ�ȭ �� ���� ����
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
    /// �񵿱� Ǯ�� ������Ʈ ����
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator StartPoolAsync(UnityAction callback) {
        loadingSlider.SetLoading(.9f, callback);
        // Ǯ�� ������Ʈ �񵿱� ����
        yield return StartPoolManager.Instance.StartPoolAsync();
        var qw = Managers.Data.GameData;
        loadingSlider.SetLoading(1f, callback);
    }

    /// <summary>
    /// ���� ������ �񵿱� �ε�
    /// �񵿱� Ǯ�� ������Ʈ ����
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator LoadGameDataAndStartPoolAsync(UnityAction callback) {
        // JSON ���� �񵿱�� �ε�
        Task<GameData> loadJsonTask = Managers.Data.LoadJsonAsync<GameData>("GameData");
        loadingSlider.SetLoading(.6f, callback);

        // Ǯ�� ������Ʈ �񵿱� ����
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
