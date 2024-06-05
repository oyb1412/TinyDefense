using System;
using UnityEngine;

/// <summary>
/// 애너미 능력치
/// </summary>
public class EnemyStatus : StatusBase {
    //적용 애너미
    private EnemyBase enemyBase;
    //생존 여부
    private bool isLive;

    /// <summary>
    /// 애너미 이동속도를 디버프에 맞게 적용
    /// </summary>
    public float MoveSpeed {
        get {
            float finalSpeed = moveSpeed;

            if (enemyBase != null && enemyBase.DebuffManager != null)
                finalSpeed = enemyBase.DebuffManager.CalculateMoveSpeed(finalSpeed);
            
            return finalSpeed;
        }
        set {
            moveSpeed = value;
        }
    }
    //데이터 컨테이너
    public EnemyData EnemyData { get; private set; }
    //이동 속도
    private float moveSpeed;
    //현재 체력
    public float CurrentHp { get; private set; }
    //최대 체력
    public float MaxHp { get; private set; }
    //보상 골드
    public int Reward { get; private set; }
    //체력 변화가 생길시 호출
    public Action<float> SetHpAction;

    private GameObject goldObject;

    public void Clear() {
        SetHpAction = null;
    }

    /// <summary>
    /// 애너미 초기화 및 재생성시 수치 초기화
    /// </summary>
    public void Init(EnemyBase enemyBase, int level) {
        if (Managers.Data.GameData == null)
            return;

        if (Managers.Enemy.EnemyData == null)
            return;

        EnemyData = Managers.Data.GameData.EnemysLevelDatas;

        this.enemyBase = enemyBase;
        Level = level;

        MoveSpeed = Managers.Enemy.EnemyData.MoveSpeed;
        if(level == 0) {
            MaxHp = Managers.Enemy.EnemyData.MaxHp;
        }
        else {
            MaxHp = Managers.Enemy.EnemyData.MaxHp * Mathf.Pow(1 + Managers.Enemy.EnemyData.MaxHpUpVolume, level);
            Debug.Log($"적 체력 {MaxHp}");
        }
        CurrentHp = MaxHp;
        SetHpAction?.Invoke(CurrentHp);
        Reward = Managers.Enemy.EnemyData.Reward + level / 10;

        if (goldObject == null)
            goldObject = Resources.Load<GameObject>(Define.OBJECT_REWARD_PATH);

        //애너미에게 적용되는 스킬의 유무를 판단해,
        //존재한다면 스킬 적용
        foreach(var item in Managers.Ability.GetAbilitysOfType<IEnemyAbility>()) {
            item.ExecuteEnemyAbility(enemyBase);
        }
    }

    /// <summary>
    /// 애너미 리워드 변경
    /// </summary>
    public void SetReward(Ability_PlusGetGold ability) {
        Reward += Mathf.RoundToInt(Reward * ability.PlusGold);
    }

    /// <summary>
    /// 애너미 이속 조절
    /// </summary>
    /// <param name="ability"></param>
    public void SetMoveSpeed(Ability_MinusEnemyMoveSpeed ability) {
        MoveSpeed *= ability.MoveSpeedValue;
    }

    /// <summary>
    /// 애너미 생존 여부 
    /// islive = true시, 재사용을 위한 체력 초기화
    /// islive = false시, 애너미 리워드 획득 및 킬 수 추가, 애너미 사망 애니메이션
    /// 애너미 사망 애니메이션에서 콜백으로 Destroy할 예정
    /// </summary>
    public bool IsLive {
        get { return isLive; }
        set {
            if(value) {
                CurrentHp = MaxHp;
                if(enemyBase)
                    enemyBase.StateMachine.ChangeState(Define.EnemyState.Run);
            } else {
                Managers.Game.CurrentKillNumber++;
                Managers.Game.CurrentGold += Reward;
                UI_GoldObject go = Managers.Resources.Instantiate(goldObject).GetComponent<UI_GoldObject>();
                go.Init(enemyBase.transform.position, Reward, true);
                enemyBase.StateMachine.ChangeState(Define.EnemyState.Dead);
                Managers.Enemy.RemoveEnemy(enemyBase);
            }
            isLive = value;
        }
    }


    /// <summary>
    /// 체력 조절 및 체력이 0 이하가 될시 사망
    /// </summary>
    /// <param name="value">체력 변경량</param>
    public void SetHp(float value, TowerBase tower = null) {
        CurrentHp -= value;
        SetHpAction?.Invoke(CurrentHp);
        if (CurrentHp <= 0 && IsLive) {
            if(tower)
                tower.TowerStatus.SetKill();

            IsLive = false;
        }
    }   
}