# [C# & UNITY] Tiny Defense

## **핵심 기술**

### ・Firebase를 이용한 데이터 관리

**🤔WHY?**

게임에 필요한 모든 데이터를 로컬에 저장하면 보안 및 유지보수성에서 뒤떨어진다 판단했고, 모든 데이터를 클라우드 서버에 저장 후 게임 시작 시 
리소스를 로컬 폴더에 저장하도록 했습니다.
게임 시작시 모든 데이터를 로드하며, 데이터 로드는 모두 비동기로 진행되고 대략적인 로딩 상태를 UI로 표시합니다.

**🤔HOW?**

 관련 코드

- FirstScene
    
    ```csharp
    
    using System;
    using System.Collections;
    using UnityEngine;
    public class FirstScene : BaseScene {
    //로딩 표기 슬라이더
    private UI_LoadingSlider loadingSlider;

    public override void Init() {
        base.Init();
        StartCoroutine(Co_Init());
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<UI_LoadingSlider>();
    }

    /// <summary>
    /// 시작 데이터 로드
    /// 각 데이터 완료 시 로딩바 증가
    /// 데이터 로드 완료시 로컬에 저장
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_Init() {
        //파이어베이스 초기화
        yield return StartCoroutine(Co_FirebaseInit());

        //애드몹 ID 로드
        yield return StartCoroutine(Co_GetAdmobID());

        //디파인 데이터 있나 체크
        //존재 시 바로 할당
        Managers.Data.DefineData = new Define();

        if (Managers.Data.CheckPathFile("Define.json")) {
            Managers.Data.DefineData = Managers.Data.DecryptionLoadJson<Define>("Define");
        }
        //디파인 데이터가 없을 시 파이어베이스에서 로드
        else {
            var task = Managers.FireStore.LoadDataToFireStore("DefineData", "DefineData", "DefineData");
            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Result != null) {
                Managers.Data.DecryptionSaveJson("Define", task.Result.ToString());
            }
            //상수 데이터 로드
            Managers.Data.DefineData = Managers.Data.DecryptionLoadJson<Define>("Define");

            yield return new WaitUntil(() => Managers.Data.DefineData.COLOR_TOWERLEVEL != null);
        }

        //pre패스에 게임 데이터가 있나 체크.
        if (Managers.Data.CheckPathFile(Managers.Data.DefineData.TAG_GAME_DATA_JSON)) {
            DebugWrapper.Log("리소스가 존재하므로 씬 이동");
            //pre패스에 게임 데이터가 있으면, 씬 이동
            loadingSlider.SetLoading(1f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));
            StopAllCoroutines();
            yield break;
        }
        //pre패스에 게임 데이터가 없으면, 로딩창을 돌리고 파이어베이스에서 데이터 불러오기.
        else {
            DebugWrapper.Log("리소스가 존재하지 않으므로 리소스 다운로드");
            Managers.Data.GameData = new GameData();
            //애너미 데이터 로드
            yield return StartCoroutine(GetEnemyData(Managers.Data.GameData.EnemyDatas));
            loadingSlider.SetLoading(.1f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));
            //인핸스 데이터 로드
            yield return StartCoroutine(GetEnhanceData(Managers.Data.GameData.EnhanceDatas));
            loadingSlider.SetLoading(.2f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));

            //모든 스킬 데이터 로드
            for (int i = 0; i< (int)Define.SkillType.Count; i++) {
                yield return StartCoroutine(GetSkillData(Managers.Data.GameData.SkillDatas, (Define.SkillType)i));
            }
            loadingSlider.SetLoading(.6f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));

            //모든 타워 데이터 로드
            for (int i = 0; i< (int)Define.TowerType.Count; i++) {
                yield return StartCoroutine(GetTowerData(Managers.Data.GameData.TowerDatas, (Define.TowerType)i));
            }
            loadingSlider.SetLoading(1f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));

            //데이터 불러오기 완료 후, 저장
            Managers.Data.SaveData(Managers.Data.GameData);
            StopAllCoroutines();
            yield break;
        }
    }

    /// <summary>
    /// 구글 애드몹 ID 로드
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_GetAdmobID() {
        var idTask = Managers.FireStore.LoadDataToFireStore("AdmobIDData", "AdmobIDData","ID");
        yield return new WaitUntil(() => idTask.IsCompleted);
        if(idTask.IsFaulted) {
            DebugWrapper.Log("애드몹 초기화 실패");
            yield break;
        }

        if (idTask.Result != null) {
            Managers.ADMob.Init(idTask.Result.ToString());
        }

        yield return null;
    }

    /// <summary>
    /// 파이어베이스 초기화
    /// 파이어베이스에서 암호 키 로드
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_FirebaseInit() {
        Managers.FireStore.Init();
        yield return new WaitUntil(() => Managers.Auth.Auth != null);
        GetDataKey();
        yield return new WaitUntil(() => Managers.Data.Key != null);
    }

    /// <summary>
    /// 파이어베이스에서 암호 키 로드
    /// </summary>
    private async void GetDataKey() {
        Managers.Data.Key = (string)await Managers.FireStore.LoadDataToFireStore("KeyData", "TqFvLDUVjjYWMfHDdMrX", "DataKey");
    }

    private IEnumerator GetEnemyData(EnemyData data) {
        var levelTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENEMY_DATA, Managers.Data.DefineData.TAG_ENEMY_DATA, "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Enemys.Level = Convert.ToInt32(levelTask.Result);
        }

        var maxHpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENEMY_DATA, Managers.Data.DefineData.TAG_ENEMY_DATA, "MaxHp");
        yield return new WaitUntil(() => maxHpTask.IsCompleted);

        if (maxHpTask.Result != null) {
            data.Enemys.MaxHp = Convert.ToSingle(maxHpTask.Result);
        }

        var maxHpUpVolumeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENEMY_DATA, Managers.Data.DefineData.TAG_ENEMY_DATA, "MaxHpUpVolume");
        yield return new WaitUntil(() => maxHpUpVolumeTask.IsCompleted);

        if (maxHpUpVolumeTask.Result != null) {
            data.Enemys.MaxHpUpVolume = Convert.ToSingle(maxHpUpVolumeTask.Result);
        }

        var moveSpeedTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENEMY_DATA, Managers.Data.DefineData.TAG_ENEMY_DATA, "MoveSpeed");
        yield return new WaitUntil(() => moveSpeedTask.IsCompleted);

        if (moveSpeedTask.Result != null) {
            data.Enemys.MoveSpeed = Convert.ToSingle(moveSpeedTask.Result);
        }

        var rewardTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENEMY_DATA, Managers.Data.DefineData.TAG_ENEMY_DATA, "Reward");
        yield return new WaitUntil(() => rewardTask.IsCompleted);

        if (rewardTask.Result != null) {
            data.Enemys.Reward = Convert.ToInt32(rewardTask.Result);
        }
    }

    private IEnumerator GetEnhanceData(EnhanceData data) {
        var levelTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENHANCE_DATA, Managers.Data.DefineData.TAG_ENHANCE_DATA, "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Enhances.Level = Convert.ToInt32(levelTask.Result);
        }

        var costTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENHANCE_DATA, Managers.Data.DefineData.TAG_ENHANCE_DATA, "EnhanceCost");
        yield return new WaitUntil(() => costTask.IsCompleted);

        if (costTask.Result != null) {
            data.Enhances.EnhanceCost = Convert.ToInt32(costTask.Result);
        }

        var costUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENHANCE_DATA, Managers.Data.DefineData.TAG_ENHANCE_DATA, "EnhanceCostUpValue");
        yield return new WaitUntil(() => costUpTask.IsCompleted);

        if (costUpTask.Result != null) {
            data.Enhances.EnhanceCostUpValue = Convert.ToInt32(costUpTask.Result);
        }

        var valueTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENHANCE_DATA, Managers.Data.DefineData.TAG_ENHANCE_DATA, "EnhanceValue");
        yield return new WaitUntil(() => valueTask.IsCompleted);

        if (valueTask.Result != null) {
            data.Enhances.EnhanceValue = Convert.ToSingle(valueTask.Result);
        }

        var valueUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_ENHANCE_DATA, Managers.Data.DefineData.TAG_ENHANCE_DATA, "EnhanceUpValue");
        yield return new WaitUntil(() => valueUpTask.IsCompleted);

        if (valueUpTask.Result != null) {
            data.Enhances.EnhanceUpValue = Convert.ToSingle(valueUpTask.Result);
        }

    }



    private IEnumerator GetSkillData(SkillData data, Define.SkillType type)     {
        var levelTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Skills[(int)type].Level = Convert.ToInt32(levelTask.Result);
        }

        var cooltimeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillCoolTime");
        yield return new WaitUntil(() => cooltimeTask.IsCompleted);

        if (cooltimeTask.Result != null) {
            data.Skills[(int)type].SkillCoolTime = Convert.ToSingle(cooltimeTask.Result);
        }

        var cooltimeDonwTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillCoolTimeDownValue");
        yield return new WaitUntil(() => cooltimeDonwTask.IsCompleted);

        if (cooltimeDonwTask.Result != null) {
            data.Skills[(int)type].SkillCoolTimeDownValue = Convert.ToSingle(cooltimeDonwTask.Result);
        }

        var costTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillCost");
        yield return new WaitUntil(() => costTask.IsCompleted);

        if (costTask.Result != null) {
            data.Skills[(int)type].SkillCost = Convert.ToInt32(costTask.Result);
        }

        var costUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillCostUpValue");
        yield return new WaitUntil(() => costUpTask.IsCompleted);

        if (costUpTask.Result != null) {
            data.Skills[(int)type].SkillCostUpValue = Convert.ToInt32(costUpTask.Result);
        }

        var damageTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillDamage");
        yield return new WaitUntil(() => damageTask.IsCompleted);

        if (damageTask.Result != null) {
            data.Skills[(int)type].SkillDamage = Convert.ToSingle(damageTask.Result);
        }

        var TimeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillTime");
        yield return new WaitUntil(() => TimeTask.IsCompleted);

        if (TimeTask.Result != null) {
            data.Skills[(int)type].SkillTime = Convert.ToSingle(TimeTask.Result);
        }

        var timeUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillTimeUpValue");
        yield return new WaitUntil(() => timeUpTask.IsCompleted);

        if (timeUpTask.Result != null) {
            data.Skills[(int)type].SkillTimeUpValue = Convert.ToSingle(timeUpTask.Result);
        }

        var valueTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillValue");
        yield return new WaitUntil(() => valueTask.IsCompleted);

        if (valueTask.Result != null) {
            data.Skills[(int)type].SkillValue = Convert.ToSingle(valueTask.Result);
        }

        var valueUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SKILL_DATA, $"{type.ToString()}_SkillData", "SkillValueUpValue");
        yield return new WaitUntil(() => valueUpTask.IsCompleted);

        if (valueUpTask.Result != null) {
            data.Skills[(int)type].SkillValueUpValue = Convert.ToSingle(valueUpTask.Result);
        }
    }

    private IEnumerator GetTowerData(TowerData data, Define.TowerType type) {
        var levelTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "TowerLevel");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Towers[(int)type].Level = Convert.ToInt32(levelTask.Result);
        }

        var rewardTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "SellReward");
        yield return new WaitUntil(() => rewardTask.IsCompleted);

        if (rewardTask.Result != null) {
            data.Towers[(int)type].SellReward = Convert.ToInt32(rewardTask.Result);
        }

        var rewardUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "SellRewardUpValue");
        yield return new WaitUntil(() => rewardUpTask.IsCompleted);

        if (rewardUpTask.Result != null) {
            data.Towers[(int)type].SellRewardUpValue = Convert.ToInt32(rewardUpTask.Result);
        }

        var slowTimeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "SlowTime");
        yield return new WaitUntil(() => slowTimeTask.IsCompleted);

        if (slowTimeTask.Result != null) {
            data.Towers[(int)type].SlowTime = Convert.ToSingle(slowTimeTask.Result);
        }

        var slowTimeUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "SlowTimeUpValue");
        yield return new WaitUntil(() => slowTimeUpTask.IsCompleted);

        if (slowTimeUpTask.Result != null) {
            data.Towers[(int)type].SlowTimeUpValue = Convert.ToSingle(slowTimeUpTask.Result);
        }

        var damageTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackDamage");
        yield return new WaitUntil(() => damageTask.IsCompleted);

        if (damageTask.Result != null) {
            data.Towers[(int)type].AttackDamage = Convert.ToSingle(damageTask.Result);
        }

        var damageUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackDamageUpValue");
        yield return new WaitUntil(() => damageUpTask.IsCompleted);

        if (damageUpTask.Result != null) {
            data.Towers[(int)type].AttackDamageUpValue = Convert.ToSingle(damageUpTask.Result);
        }

        var delyaTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackDelay");
        yield return new WaitUntil(() => delyaTask.IsCompleted);

        if (delyaTask.Result != null) {
            data.Towers[(int)type].AttackDelay = Convert.ToSingle(delyaTask.Result);
        }

        var delayUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackDelayDownValue");
        yield return new WaitUntil(() => delayUpTask.IsCompleted);

        if (delayUpTask.Result != null) {
            data.Towers[(int)type].AttackDelayDownValue = Convert.ToSingle(delayUpTask.Result);
        }

        var rangeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackRange");
        yield return new WaitUntil(() => rangeTask.IsCompleted);

        if (rangeTask.Result != null) {
            data.Towers[(int)type].AttackRange = Convert.ToSingle(rangeTask.Result);
        }

        var rangeUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "AttackRangeUpValue");
        yield return new WaitUntil(() => rangeUpTask.IsCompleted);

        if (rangeUpTask.Result != null) {
            data.Towers[(int)type].AttackRangeUpValue = Convert.ToSingle(rangeUpTask.Result);
        }

        var buffTimeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "BuffTime");
        yield return new WaitUntil(() => buffTimeTask.IsCompleted);

        if (buffTimeTask.Result != null) {
            data.Towers[(int)type].BuffTime = Convert.ToSingle(buffTimeTask.Result);
        }

        var buffTimeUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "BuffTimeUpValue");
        yield return new WaitUntil(() => buffTimeUpTask.IsCompleted);

        if (buffTimeUpTask.Result != null) {
            data.Towers[(int)type].BuffTimeUpValue = Convert.ToSingle(buffTimeUpTask.Result);
        }

        var fireTimeTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "FireTime");
        yield return new WaitUntil(() => fireTimeTask.IsCompleted);

        if (fireTimeTask.Result != null) {
            data.Towers[(int)type].FireTime = Convert.ToSingle(fireTimeTask.Result);
        }

        var fireTimeUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "FireTimeUpValue");
        yield return new WaitUntil(() => fireTimeUpTask.IsCompleted);

        if (fireTimeUpTask.Result != null) {
            data.Towers[(int)type].FireTimeUpValue = Convert.ToSingle(fireTimeUpTask.Result);
        }

        var fireValueTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "FireValue");
        yield return new WaitUntil(() => fireValueTask.IsCompleted);

        if (fireValueTask.Result != null) {
            data.Towers[(int)type].FireValue = Convert.ToSingle(fireValueTask.Result);
        }

        var fireValueUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "FireValueUpValue");
        yield return new WaitUntil(() => fireValueUpTask.IsCompleted);

        if (fireValueUpTask.Result != null) {
            data.Towers[(int)type].FireValueUpValue = Convert.ToSingle(fireValueUpTask.Result);
        }

        var buffValueTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "BuffValue");
        yield return new WaitUntil(() => buffValueTask.IsCompleted);

        if (buffValueTask.Result != null) {
            data.Towers[(int)type].BuffValue = Convert.ToSingle(buffValueTask.Result);
        }

        var buffValueUpTask = Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_TOWER_DATA, $"TowerData{(int)type}", "BuffValueUpValue");
        yield return new WaitUntil(() => buffValueUpTask.IsCompleted);

        if (buffValueUpTask.Result != null) {
            data.Towers[(int)type].BuffValueUpValue = Convert.ToSingle(buffValueUpTask.Result);
        }
    }
}
    ```
- FirebaseStoreManager
    ```csharp
    using Firebase;
    using Firebase.Analytics;
    using Firebase.Auth;
    using Firebase.Extensions;
    using Firebase.Firestore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// 파이어스토어 데이터 관리 클래스
    /// </summary>
    public class FireStoreManager {
    private FirebaseFirestore db;

    /// <summary>
    /// 파이어스토어, 파이어베이스 어스, 파이어베이스 아날리틱스 초기화
    /// </summary>
    public void Init() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted) {
                Debug.LogError("Failed to check and fix dependencies: " + task.Exception);
                return;
            }

            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                DebugWrapper.Log("Firebase Initialized");
                db = FirebaseFirestore.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(false);
                Managers.Auth.Auth = FirebaseAuth.DefaultInstance;
            } else {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                return;
            }
        });
    }

    /// <summary>
    /// 파이어베이스에 정보 저장
    /// </summary>
    /// <param name="collection">저장할 콜렉션 명</param>
    /// <param name="document">저장할 도큐멘트 명</param>
    /// <param name="field">저장할 필드 명</param>
    /// <param name="data">저장할 데이터</param>
    public void SaveDataToFirestore(string collection, string document, string field, object data) {
        if (string.IsNullOrEmpty(collection) || string.IsNullOrEmpty(document) || string.IsNullOrEmpty(field)) {
            Debug.LogError("콜렉션, 도큐멘트 또는 필드 이름이 유효하지 않습니다.");
            return;
        }

        if (data == null) {
            Debug.LogError("저장할 데이터가 null입니다.");
            return;
        }

        try {
            Debug.LogError("데이터 저장 시도.");
            DocumentReference docRef = db.Collection(collection).Document(document);
            Dictionary<string, object> userData = new Dictionary<string, object>
            {
            { field, data }
        };

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted && !task.IsFaulted) {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists) {
                        docRef.UpdateAsync(userData).ContinueWithOnMainThread(updateTask => {
                            if (updateTask.IsCompleted && !updateTask.IsFaulted) {
                                DebugWrapper.Log($"{field}에 {data.ToString()} 업데이트 완료");
                            } else {
                                Debug.LogError($"{field}에 {data.ToString()} 업데이트 실패: " + updateTask.Exception?.Message);
                            }
                        });
                    } else {
                        docRef.SetAsync(userData).ContinueWithOnMainThread(setTask => {
                            if (setTask.IsCompleted && !setTask.IsFaulted) {
                                DebugWrapper.Log($"{field}에 {data.ToString()} 저장 완료");
                            } else {
                                Debug.LogError($"{field}에 {data.ToString()} 저장 실패: " + setTask.Exception?.Message);
                            }
                        });
                    }
                } else {
                    Debug.LogError($"문서 확인 실패: " + task.Exception?.Message);
                }
            });
        } catch (Exception e) {
            Debug.LogError($"Firestore 처리 중 예외 발생: {e.Message}");
        }
    }


    /// <summary>
    /// 파이어베이스에서 정보 받아오기
    /// </summary>
    /// <param name="collection">정보를 받아올 콜렉션 명</param>
    /// <param name="document">정보를 받아올 도큐멘트 명</param>
    /// <param name="field">정보를 받아올 필드 명</param>
    /// <param name="callBack">정보를 저장할 콜백 함수</param>
    public async Task<object> LoadDataToFireStore(string collection, string document, string field) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            if (snapshot.TryGetValue(field, out object value)) {
                return value;
            } else {
                DebugWrapper.Log($"데이터 키{field}가 없습니다");
                return null;
            }
        } else {
            DebugWrapper.Log($"도큐멘트 {document}가 없습니다");
            return null;
        }
    }

    /// <summary>
    /// 특정 도큐멘트의 필드 모두 받아오기
    /// </summary>
    /// <param name="collection">콜렉션 명</param>
    /// <param name="document">도큐멘트 명</param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> LoadAllDataFromDocument(string collection, string document) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            Dictionary<string, object> allFields = snapshot.ToDictionary();
            return allFields;
        } else {
            DebugWrapper.Log($"도큐멘트 {document}가 없습니다");
            return null;
        }
    }
}
    ```

**🤓Result!**

 게임 내 모든 데이터를 클라우드 서버에서 관리해, 유지보수성 및 보안성 상승


### ・AES복호화를 이용한 데이터 복호화

**🤔WHY?**

  클라우드 서버에서 내려받은 데이터를 그대로 로컬에 저장하면 보안 상 위험성이 존재하기 때문에, 모든 데이터를 복호화해 저장/로드

**🤔HOW?**

 관련 코드

- AesEncryption
    
    ```csharp
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 문자열 AES 암호화 클래스
    /// </summary>
    public static class AesEncryption {

    /// <summary>
    /// 키를 바탕으로 암호화
    /// </summary>
    /// <param name="plainText">암호화 대상 문자열</param>
    /// <param name="key">암호화 키</param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string key) {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create()) {
            aes.Key = GetAesKey(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream()) {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream)) {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    /// <summary>
    /// 암호 해독
    /// </summary>
    /// <param name="cipherText">해독할 문자열</param>
    /// <param name="key">암호화 키</param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, string key) {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create()) {
            aes.Key = GetAesKey(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer)) {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 키를 바탕으로 암호 생성
    /// </summary>
    /// <param name="key">키</param>
    /// <returns></returns>
    private static byte[] GetAesKey(string key) {
        using (SHA256 sha256 = SHA256.Create()) {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}

    ```
    

**🤓Result!**

  데이터를 복호화해 안전하게 보호하고, 복호화 키 또한 클라우드 서버에서 보관해 더욱 더 강력한 보안성 확립
  

### ・옵저버 패턴을 이용한 이벤트 위주 로직

**🤔WHY?**

 데이터의 변경이 없음에도 주기적으로 데이터를 동기화해, 필요 없는 작업이 지속적으로 반복되어 결과적으로 퍼포먼스가 하락되었기 때문에, 최적화를 위해 사용했습니다.

**🤔HOW?**

 관련 코드

- GameManager
    
    ```csharp
    public class GameManager  
    {
         //게임레벨 변경 액션
         public Action<int> CurrentGameLevelAction;
     }
    ```
    
- EnemySpawnManager
    
    ```csharp
    public class EnemySpawnManager  {
        /// <summary>
        /// 생성 딜레이 초기화 및
        /// 생성 코루틴 시작
        /// </summary>
        public void SpawnStart() {
          //게임 레벨이 변경 될 때마다 자동으로 적 스폰 코루틴을 실행
          Managers.Game.CurrentGameLevelAction += ((level) => Managers.Instance.StartCoroutine(Co_Spawn()));
       }
    }
    ```
    

**🤓Result!**

Update함수의 사용을 최소화하고, 특정 이벤트의 발동을 감지하여 액션을 실행하는 이벤트 위주의 로직을 구현


### ・Google API를 이용한 광고 시스템

**🤔WHY?**

과금요소가 없는 게임이기 때문에, 수익성을 위해 추가했습니다.

**🤔HOW?**

 관련 코드

- AdmobManager
    
    ```csharp
    using UnityEngine;
    using GoogleMobileAds.Api;
    using UnityEngine.Events;

    public class AdmobManager {
    private string _adUnitId;
    private float _currentTimeScale;
    private RewardedAd _rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    private void LoadRewardedAd() {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null) {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
        DebugWrapper.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                DebugWrapper.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd(UnityAction callBack) {
        _currentTimeScale = Time.timeScale;

        if (_rewardedAd != null && _rewardedAd.CanShowAd()) {
            _rewardedAd.Show((Reward reward) => {
                callBack.Invoke();
                Time.timeScale = _currentTimeScale;
                LoadRewardedAd();
            });
        } else {
            LoadRewardedAd();
        }
    }

    public void Init(string id) {
        _adUnitId = id;
        MobileAds.Initialize((InitializationStatus initStatus) => {
            LoadRewardedAd();
        });
    }
}
    ```
    


**🤓Result!**

게임의 각종 기능 추가에 광고 시청을 강제해, 수익성을 확보했습니다.

### ・애니메이터와 파라미터를 이용한 유닛 애니메이션 시스템

**🤔WHY?**

Play() 등 단순한 애니메이션 호출 메서드로 원할 때 애니메이션을 호출할 수는 있었지만, 애니메이션이 자연스럽게 이어지는 것이 아닌 뚝뚝 끊기는 연출이 반복되는 문제를 해결하기 위해 사용했습니다.

**🤔HOW?**

 관련 코드

- TowerBase
    
    ```csharp
    /// <summary>
    /// 애니메이션 변경(트리거)
    /// </summary>
    public void SetAnimation(string paremeter) {
        animator.SetTrigger(paremeter);
    }

    /// <summary>
    /// 애니메이션 변경(불)
    /// </summary>
    public void SetAnimation(string paremeter, bool trigger) {
        animator.SetBool(paremeter, trigger);
    }
    ```
    

**🤓Result!**

애니메이션을 단발성으로 실행하는 것이 아닌, 파라미터의 상태에 맞게 자연스럽게 애니메이션이 전환되도록 변경, 뚝뚝 끊기는 애니메이션이 아닌 자연스러운 전환을 연출할 수 있었습니다.


### ・씬 전환 페이드 시스템

**🤔WHY?**

씬 전환시 아무 연출없이 즉각적으로 화면이 전환되어 화면이 갈아끼워지는듯한 느낌을 받는다는 피드백을 받아, 보다 극적인 연출을 위해 사용하였습니다.
또한 트위닝을 적극적으로 사용해, 페이드 전환 후 자연스러운 콜백함수 실행이 가능했습니다.

**🤔HOW?**

 관련 코드

- UI_Fade
    
    ```csharp
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;
    using UnityEngine.SceneManagement;
    using UnityEngine.Events;

/// <summary>
/// 모든 페이드 관리 클래스
/// </summary>
public class UI_Fade : MonoBehaviour {
    public static UI_Fade Instance;
    //페이드 이미지
    private Image fadeImage;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        fadeImage = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// 페이드 인
    /// 페이드 종료 시 씬 이동
    /// </summary>
    /// <param name="type">이동할 씬</param>
    public void ActivationFade(Define.SceneType type) {
        fadeImage.DOFade(1f, Managers.Data.DefineData.FADE_TIME).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(type.ToString()));
    }

    /// <summary>
    /// 페이드 아웃
    /// </summary>
    public void DeActivationFade() {
        fadeImage.DOFade(0f, Managers.Data.DefineData.FADE_TIME).SetUpdate(true);
    }

    /// <summary>
    /// 페이드 인
    /// 페이드 종료 시 콜백함수 호출
    /// </summary>
    /// <param name="fadeInCallBack"></param>
    public void ActivationAndDeActivationFade(UnityAction fadeInCallBack) {
        fadeImage.DOFade(1f, Managers.Data.DefineData.FADE_TIME).SetUpdate(true).OnComplete(() =>
        {
            fadeInCallBack?.Invoke();
            fadeImage.DOFade(0f, Managers.Data.DefineData.FADE_TIME);
        });
    }
}
    ```


**🤓Result!**

  씬 전환 시 즉각적인 전환이 아닌, 화면이 가려지고 씬이 전환되고 화면이 밝아지고 게임이 시작되는 등 페이드 연출을 추가해, 사용자가 어색한 느낌을 받지 않도록 하였습니다.
  

### ・물리엔진을 사용하지 않고 모든 로직 구현

**🤔WHY?**

모바일 기기에서의 성능 최적화를 위해, 콜라이더, 리지드바디 등 유니티의 물리엔진을 사용하지 않고 모든 기능을 구현했습니다.
또한 유니티 프로젝트 세팅의 물리 설정을 비활성화해 상당한 성능 최적화에 성공했습니다.

**🤔HOW?**

 관련 코드

- EnemySearchSystem
    
    ```csharp
    /// <summary>
    /// 공격 사거리 내의 적 서치
    /// </summary>
    /// <returns></returns>
    public EnemyBase SearchEnemy() {
    if (towerBase == null)
        return null;

    var enemyList = Managers.Enemy.GetEnemyArray();
    for (int i = enemyList.Length - 1; i >= 0; i--) {
        if (Util.IsEnemyNull(enemyList[i]))
            continue;

        if (Vector2.Distance(transform.position, enemyList[i].transform.position) >= towerBase.TowerStatus.AttackRange * Managers.Data.DefineData.TOWER_RANGE)
            continue;

        return enemyList[i];
    }
    return null;
}
    ```
    
- Skill_Tornado
    
    ```csharp
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 토네이도 스킬 객체 클래스
    /// </summary>
    public class Skill_Tornado : MonoBehaviour {
    /// <summary>
    /// 주변 적을 공격
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_Attack() {
        while (true) {
            if (Managers.Enemy.EnemyList.Count > 0) {
                var enemyList = Managers.Enemy.GetEnemyArray();
                DebugWrapper.Log(enemyList.Length);
                for (int i = enemyList.Length - 1; i >= 0; i--) {
                    if (Util.IsEnemyNull(enemyList[i]))
                        continue;

                    if (Vector2.Distance(transform.position, enemyList[i].transform.position) > Managers.Data.DefineData.SKILL_TORNADO_RANGE) {
                        if (containsEnemy.Contains(enemyList[i]))
                            containsEnemy.Remove(enemyList[i]);

                        continue;
                    }

                    if (containsEnemy.Contains(enemyList[i]))
                        continue;

                    containsEnemy.Add(enemyList[i]);
                    enemyList[i].DebuffManager.AddDebuff(new SlowDebuff(skillData.SkillValue, skillData.SkillTime), enemyList[i]);
                    enemyList[i].EnemyStatus.SetHp(skillData.SkillDamage);
                }
            }

            yield return attackDelay;
        }
    }

}
    ```
    

### ・풀링 오브젝트 시스템

**🤔WHY?**

각종 오브젝트를 필요할 때 마다 생성, 필요가 없어지면 제거해 짧은 시간 내에 다량의 객체를 생성하고 제거하는 상황이 반복되 퍼포먼스가 크게 하락하였기에 사용했습니다.
게임 시작 시 런타임시 동적으로 생성되는 객체들을 수십 ~ 수백체씩 생성 해 두고 풀링으로 사용해, 런타임 시 객체를 생성하는 일을 방지했습니다.

**🤔HOW?**

 관련 코드

- PoolManager
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class PoolManager
    {
    //풀링 객체 종류만큼 생성되는 클래스
    class Pool
    {
        //풀링 객체
        public GameObject Original { get; private set; }
        //풀링 객체들을 모아둘 부모 객체
        public Transform Root { get; private set; }
        //풀링 객체들을 보관해둘 스택
        //스택이던 큐던 크게 상관없다.
        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        //어떤 객체가 처음으로 생성되면, 그 객체의 풀 클래스를 생성
        public void Init(GameObject original, int count = 5)
        {
            //객체를 저장
            Original = original;
            //객체들을 모아둘 부모 객체를 생성 & 이름 지정
            Root = new GameObject().transform;
            Root.name = original.name + "_root";

            //자신이 지정한 카운트 수 만큼 객체를 생성
            //객체 생성 후, 객체들을 스택에 저장
            for (int i = 0; i < count; i++)
            {
                Release(Create());
            }
        }

        //객체 생성
        private Poolable Create()
        {
            GameObject go = Object.Instantiate(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        //객체 부모생성 및 스택에 저장
        public void Release(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            _poolStack.Push(poolable);
        }

        //풀에 담겨있는 객체를 재사용하기위해 스택에서 추출
        public Poolable Activation()
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();
            
            poolable.gameObject.SetActive(true);

            poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = Root;

            return poolable;
        }
    }
    
    private Transform _root;
    private Dictionary<string, Pool> _pools;

    public void Init()
    {
        _root = new GameObject("@Pool_root").transform;
        _pools = new Dictionary<string, Pool>();
    }

    /// <summary>
    /// 사용이 끝난 풀링 객체를 비활성화 및 다시 스택에 저장 
    /// </summary>
    /// <param name="poolable"></param>
    public void Release(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_pools.ContainsKey(name) == false)
        {
            Object.Destroy(poolable.gameObject);
            return;
        }
        
        _pools[name].Release(poolable);
    }

    /// <summary>
    /// 풀링 객체를 사용하기 위해 스택에서 추출
    /// </summary>
    /// <param name="original"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public Poolable Activation(GameObject original, int count = 5)
    {
        if(_pools.ContainsKey(original.name) == false)
            CreatePool(original, count);

        return _pools[original.name].Activation();
    }

    /// <summary>
    /// 풀링 객체가 처음으로 생성되었을때, 그 객체의 풀 클래스를 생성
    /// </summary>
    /// <param name="original"></param>
    /// <param name="count"></param>
    private void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original,count);
        pool.Root.parent = _root;
        
        _pools.Add(original.name, pool);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pools.ContainsKey(name) == false)
            return null;

        return _pools[name].Original;
    }
}

    }
    ```
    

**🤓Result!**

  시스템에 큰 부하를 주는 객체의 직접적인 생성 및 파괴를 최대한 피하고 풀링 시스템을 이용, 이미 생성된 객체를 재사용하는 과정을 통해 게임 도중 객체를 생성하지 않아 퍼포먼스가 크게 상승했습니다.
  

## 📈보완점

**-문제점**

성능상 pc에 비해 부족한 안드로이드 기기에서 객체가 과도하게 많은 경우 프레임 드랍이 발생했습니다.

**-문제의 원인**

간단한 구현을 위한 과한 물리엔진 사용
텍스쳐 및 사운드의 압축이 제대로 되어있지 않았음
안드로이드를 위한 프로젝트 세팅 미흡

**-해결방안**

물리엔진을 전혀 사용하지 않고 모든 기능을 구현함으로써 가장 큰 성능 향상
텍스쳐 및 사운드의 압축, 프로젝트 세팅 완료 (https://blog.naver.com/oyb1234136)에 모든 최적화 방식을 기록했습니다.

