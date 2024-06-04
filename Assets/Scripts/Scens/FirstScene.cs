using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class FirstScene : BaseScene {
    public override void Clear() {
    }

    public override void Init() {
        base.Init();
        StartCoroutine(Co_Init());
    }

    private IEnumerator Co_Init() {
        yield return StartCoroutine(Co_FirebaseInit());
        //pre패스에 게임 데이터가 있나 체크.
        if (Managers.Data.CheckPathFile("GameData.json")) {
            //pre패스에 게임 데이터가 있으면, 씬 이동
            UI_Fade.Instance.ActivationFade(Define.SceneType.Main);
        }
        //pre패스에 게임 데이터가 없으면, 로딩창을 돌리고 파이어베이스에서 데이터 불러오기.
        else {
            Managers.Data.GameData = new GameData();
            yield return StartCoroutine(GetEnemyData(Managers.Data.GameData.EnemysLevelDatas));
            yield return StartCoroutine(GetEnhanceData(Managers.Data.GameData.EnhancesLevelDatas));
            for(int i = 0; i< (int)Define.SkillType.Count; i++) {
                yield return StartCoroutine(GetSkillData(Managers.Data.GameData.SkillsLevelDatas, (Define.SkillType)i));
            }
            for(int i = 0; i< (int)Define.TowerType.Count; i++) {
                yield return StartCoroutine(GetTowerData(Managers.Data.GameData.TowersLevelDatas, (Define.TowerType)i));
            }
            //데이터 불러오기 완료 후, 저장
            Managers.Data.SaveData();
            UI_Fade.Instance.ActivationFade(Define.SceneType.Main);
        }
    }



    private IEnumerator Co_FirebaseInit() {
        Managers.FireStore.Init();
        yield return new WaitUntil(() => Managers.Auth.Auth != null);
        GetDataKey();
        yield return new WaitUntil(() => Managers.Data.Key != null);
    }

    private async void GetDataKey() {
        Managers.Data.Key = (string)await Managers.FireStore.LoadDataToFireStore(Define.TAG_KEY_DATA, Define.TAG_KEY_DOCUMENT, Define.TAG_DATA_KEY);
    }

    private IEnumerator GetEnemyData(EnemyData data) {
        var levelTask = Managers.FireStore.LoadDataToFireStore("EnemyData", "EnemyData", "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Enemys.Level = Convert.ToInt32(levelTask.Result);
        }

        var maxHpTask = Managers.FireStore.LoadDataToFireStore("EnemyData", "EnemyData", "MaxHp");
        yield return new WaitUntil(() => maxHpTask.IsCompleted);

        if (maxHpTask.Result != null) {
            data.Enemys.MaxHp = Convert.ToSingle(maxHpTask.Result);
        }

        var maxHpUpVolumeTask = Managers.FireStore.LoadDataToFireStore("EnemyData", "EnemyData", "MaxHpUpVolume");
        yield return new WaitUntil(() => maxHpUpVolumeTask.IsCompleted);

        if (maxHpUpVolumeTask.Result != null) {
            data.Enemys.MaxHpUpVolume = Convert.ToSingle(maxHpUpVolumeTask.Result);
        }

        var moveSpeedTask = Managers.FireStore.LoadDataToFireStore("EnemyData", "EnemyData", "MoveSpeed");
        yield return new WaitUntil(() => moveSpeedTask.IsCompleted);

        if (moveSpeedTask.Result != null) {
            data.Enemys.MoveSpeed = Convert.ToSingle(moveSpeedTask.Result);
        }

        var rewardTask = Managers.FireStore.LoadDataToFireStore("EnemyData", "EnemyData", "Reward");
        yield return new WaitUntil(() => rewardTask.IsCompleted);

        if (rewardTask.Result != null) {
            data.Enemys.Reward = Convert.ToInt32(rewardTask.Result);
        }
    }

    private IEnumerator GetEnhanceData(EnhanceData data) {
        var levelTask = Managers.FireStore.LoadDataToFireStore("EnhanceData", "EnhanceData", "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Enhances.Level = Convert.ToInt32(levelTask.Result);
        }

        var costTask = Managers.FireStore.LoadDataToFireStore("EnhanceData", "EnhanceData", "EnhanceCost");
        yield return new WaitUntil(() => costTask.IsCompleted);

        if (costTask.Result != null) {
            data.Enhances.EnhanceCost = Convert.ToInt32(costTask.Result);
        }

        var costUpTask = Managers.FireStore.LoadDataToFireStore("EnhanceData", "EnhanceData", "EnhanceCostUpValue");
        yield return new WaitUntil(() => costUpTask.IsCompleted);

        if (costUpTask.Result != null) {
            data.Enhances.EnhanceCostUpValue = Convert.ToInt32(costUpTask.Result);
        }

        var valueTask = Managers.FireStore.LoadDataToFireStore("EnhanceData", "EnhanceData", "EnhanceValue");
        yield return new WaitUntil(() => valueTask.IsCompleted);

        if (valueTask.Result != null) {
            data.Enhances.EnhanceValue = Convert.ToInt32(valueTask.Result);
        }

        var valueUpTask = Managers.FireStore.LoadDataToFireStore("EnhanceData", "EnhanceData", "EnhanceUpValue");
        yield return new WaitUntil(() => valueUpTask.IsCompleted);

        if (valueUpTask.Result != null) {
            data.Enhances.EnhanceUpValue = Convert.ToInt32(valueUpTask.Result);
        }

    }

    private IEnumerator GetSkillData(SkillData data, Define.SkillType type) {
        var levelTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "Level");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Skills[(int)type].Level = Convert.ToInt32(levelTask.Result);
        }

        var cooltimeTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillCoolTime");
        yield return new WaitUntil(() => cooltimeTask.IsCompleted);

        if (cooltimeTask.Result != null) {
            data.Skills[(int)type].SkillCoolTime = Convert.ToSingle(cooltimeTask.Result);
        }

        var cooltimeDonwTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillCoolTimeDownValue");
        yield return new WaitUntil(() => cooltimeDonwTask.IsCompleted);

        if (cooltimeDonwTask.Result != null) {
            data.Skills[(int)type].SkillCoolTimeDownValue = Convert.ToSingle(cooltimeDonwTask.Result);
        }

        var costTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillCost");
        yield return new WaitUntil(() => costTask.IsCompleted);

        if (costTask.Result != null) {
            data.Skills[(int)type].SkillCost = Convert.ToInt32(costTask.Result);
        }

        var costUpTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillCostUpValue");
        yield return new WaitUntil(() => costUpTask.IsCompleted);

        if (costUpTask.Result != null) {
            data.Skills[(int)type].SkillCostUpValue = Convert.ToInt32(costUpTask.Result);
        }

        var damageTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillDamage");
        yield return new WaitUntil(() => damageTask.IsCompleted);

        if (damageTask.Result != null) {
            data.Skills[(int)type].SkillDamage = Convert.ToSingle(damageTask.Result);
        }

        var TimeTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillTime");
        yield return new WaitUntil(() => TimeTask.IsCompleted);

        if (TimeTask.Result != null) {
            data.Skills[(int)type].SkillTime = Convert.ToSingle(TimeTask.Result);
        }

        var timeUpTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillTimeUpValue");
        yield return new WaitUntil(() => timeUpTask.IsCompleted);

        if (timeUpTask.Result != null) {
            data.Skills[(int)type].SkillTimeUpValue = Convert.ToSingle(timeUpTask.Result);
        }

        var valueTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillValue");
        yield return new WaitUntil(() => valueTask.IsCompleted);

        if (valueTask.Result != null) {
            data.Skills[(int)type].SkillValue = Convert.ToSingle(valueTask.Result);
        }

        var valueUpTask = Managers.FireStore.LoadDataToFireStore("SkillData", $"{type.ToString()}_SkillData", "SkillValueUpValue");
        yield return new WaitUntil(() => valueUpTask.IsCompleted);

        if (valueUpTask.Result != null) {
            data.Skills[(int)type].SkillValueUpValue = Convert.ToSingle(valueUpTask.Result);
        }
    }

    private IEnumerator GetTowerData(TowerData data, Define.TowerType type) {
        var levelTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "TowerLevel");
        yield return new WaitUntil(() => levelTask.IsCompleted);

        if (levelTask.Result != null) {
            data.Towers[(int)type].Level = Convert.ToInt32(levelTask.Result);
        }

        var rewardTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "SellReward");
        yield return new WaitUntil(() => rewardTask.IsCompleted);

        if (rewardTask.Result != null) {
            data.Towers[(int)type].SellReward = Convert.ToInt32(rewardTask.Result);
        }

        var rewardUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "SellRewardUpValue");
        yield return new WaitUntil(() => rewardUpTask.IsCompleted);

        if (rewardUpTask.Result != null) {
            data.Towers[(int)type].SellRewardUpValue = Convert.ToInt32(rewardUpTask.Result);
        }

        var slowTimeTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "SlowTime");
        yield return new WaitUntil(() => slowTimeTask.IsCompleted);

        if (slowTimeTask.Result != null) {
            data.Towers[(int)type].SlowTime = Convert.ToSingle(slowTimeTask.Result);
        }

        var slowTimeUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "SlowTimeUpValue");
        yield return new WaitUntil(() => slowTimeUpTask.IsCompleted);

        if (slowTimeUpTask.Result != null) {
            data.Towers[(int)type].SlowTimeUpValue = Convert.ToSingle(slowTimeUpTask.Result);
        }

        var damageTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackDamage");
        yield return new WaitUntil(() => damageTask.IsCompleted);

        if (damageTask.Result != null) {
            data.Towers[(int)type].AttackDamage = Convert.ToSingle(damageTask.Result);
        }

        var damageUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackDamageUpValue");
        yield return new WaitUntil(() => damageUpTask.IsCompleted);

        if (damageUpTask.Result != null) {
            data.Towers[(int)type].AttackDamageUpValue = Convert.ToSingle(damageUpTask.Result);
        }

        var delyaTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackDelay");
        yield return new WaitUntil(() => delyaTask.IsCompleted);

        if (delyaTask.Result != null) {
            data.Towers[(int)type].AttackDelay = Convert.ToSingle(delyaTask.Result);
        }

        var delayUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackDelayDownValue");
        yield return new WaitUntil(() => delayUpTask.IsCompleted);

        if (delayUpTask.Result != null) {
            data.Towers[(int)type].AttackDelayDownValue = Convert.ToSingle(delayUpTask.Result);
        }

        var rangeTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackRange");
        yield return new WaitUntil(() => rangeTask.IsCompleted);

        if (rangeTask.Result != null) {
            data.Towers[(int)type].AttackRange = Convert.ToSingle(rangeTask.Result);
        }

        var rangeUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "AttackRangeUpValue");
        yield return new WaitUntil(() => rangeUpTask.IsCompleted);

        if (rangeUpTask.Result != null) {
            data.Towers[(int)type].AttackRangeUpValue = Convert.ToSingle(rangeUpTask.Result);
        }

        var buffTimeTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "BuffTime");
        yield return new WaitUntil(() => buffTimeTask.IsCompleted);

        if (buffTimeTask.Result != null) {
            data.Towers[(int)type].BuffTime = Convert.ToSingle(buffTimeTask.Result);
        }

        var buffTimeUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "BuffTimeUpValue");
        yield return new WaitUntil(() => buffTimeUpTask.IsCompleted);

        if (buffTimeUpTask.Result != null) {
            data.Towers[(int)type].BuffTimeUpValue = Convert.ToSingle(buffTimeUpTask.Result);
        }

        var fireTimeTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "FireTime");
        yield return new WaitUntil(() => fireTimeTask.IsCompleted);

        if (fireTimeTask.Result != null) {
            data.Towers[(int)type].FireTime = Convert.ToSingle(fireTimeTask.Result);
        }

        var fireTimeUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "FireTimeUpValue");
        yield return new WaitUntil(() => fireTimeUpTask.IsCompleted);

        if (fireTimeUpTask.Result != null) {
            data.Towers[(int)type].FireTimeUpValue = Convert.ToSingle(fireTimeUpTask.Result);
        }

        var fireValueTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "FireValue");
        yield return new WaitUntil(() => fireValueTask.IsCompleted);

        if (fireValueTask.Result != null) {
            data.Towers[(int)type].FireValue = Convert.ToSingle(fireValueTask.Result);
        }

        var fireValueUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "FireValueUpValue");
        yield return new WaitUntil(() => fireValueUpTask.IsCompleted);

        if (fireValueUpTask.Result != null) {
            data.Towers[(int)type].FireValueUpValue = Convert.ToSingle(fireValueUpTask.Result);
        }

        var buffValueTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "BuffValue");
        yield return new WaitUntil(() => buffValueTask.IsCompleted);

        if (buffValueTask.Result != null) {
            data.Towers[(int)type].BuffValue = Convert.ToSingle(buffValueTask.Result);
        }

        var buffValueUpTask = Managers.FireStore.LoadDataToFireStore("TowerData", $"TowerData{(int)type}", "BuffValueUpValue");
        yield return new WaitUntil(() => buffValueUpTask.IsCompleted);

        if (buffValueUpTask.Result != null) {
            data.Towers[(int)type].BuffValueUpValue = Convert.ToSingle(buffValueUpTask.Result);
        }
    }
}