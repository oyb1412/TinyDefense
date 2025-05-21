using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance {
        get {
            return _instance;
        }
    }

    private FireStoreManager _fireStore = new FireStoreManager();
    private DataManager _data = new DataManager();
    private AdmobManager _admob = new AdmobManager();
    private FirebaseAuthManager _auth = new FirebaseAuthManager();
    private AbilityManager _ability = new AbilityManager();
    private SkillManager _skill = new SkillManager();
    private EnemyManager _enemy = new EnemyManager();
    private TowerManager _tower = new TowerManager();
    private EnhanceManager _enhance = new EnhanceManager();
    private GameManager _game = new GameManager();
    private EnemySpawnManager _spawn = new EnemySpawnManager();
    private SelectManager _select = new SelectManager();
    private PoolManager _pool = new PoolManager();
    private ResourcesManager _resources = new ResourcesManager();
    private SceneManagerEX _scene = new SceneManagerEX();
    private ProjectileManager _projectile = new ProjectileManager();

    public static FireStoreManager FireStore => _instance._fireStore; 
    public static DataManager Data => _instance._data; 
    public static AdmobManager ADMob => _instance._admob;
    public static FirebaseAuthManager Auth => _instance._auth;
    public static SkillManager Skill => _instance._skill;
    public static AbilityManager Ability => _instance._ability;
    public static EnemyManager Enemy => _instance._enemy;
    public static TowerManager Tower => _instance._tower;
    public static ProjectileManager Projectile => _instance._projectile;
    public static EnhanceManager Enhance => _instance._enhance;
    public static GameManager Game => _instance._game;
    public static EnemySpawnManager Spawn => _instance._spawn;
    public static SelectManager Select => _instance._select;
    public static PoolManager Pool => _instance._pool;
    public static SceneManagerEX Scene => _instance._scene;
    public static ResourcesManager Resources => Instance._resources;
    
    private void Awake()
    {
        Init();

#if UNITY_EDITOR
        Application.targetFrameRate = -1; // 에디터에서는 프레임 레이트 제한을 해제
#elif UNITY_ANDROID
        Application.targetFrameRate = 60; // 안드로이드 기기에서 프레임 레이트를 60으로 설정
#else
        Application.targetFrameRate = -1; // 다른 플랫폼에서는 기본 설정
#endif
    }
    private void Init()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
