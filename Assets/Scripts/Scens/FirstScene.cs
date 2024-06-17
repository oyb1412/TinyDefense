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
            loadingSlider.SetLoading(.5f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));

            //모든 스킬 데이터 로드
            for (int i = 0; i< (int)Define.SkillType.Count; i++) {
                yield return StartCoroutine(GetSkillData(Managers.Data.GameData.SkillDatas, (Define.SkillType)i));
            }
            loadingSlider.SetLoading(.8f, () => UI_Fade.Instance.ActivationFade(Define.SceneType.Main));

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