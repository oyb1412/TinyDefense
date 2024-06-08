using UnityEngine;

/// <summary>
/// 타워 능력치
/// </summary>
public class TowerStatus : StatusBase {
    /// <summary>
    /// 타워 생성 및 재생성시 초기화
    /// </summary>
    /// <param name="tower">대상 타워</param>
    public void Init(TowerBase tower) {
        if (Managers.Tower == null)
            return;

        if (Managers.Tower.TowerData == null)
            return;

        TowerData = Managers.Tower.TowerData[(int)tower.TowerType];

        if (TowerData == null)
            return;

        Level = tower.TowerLevel;
        TowerType = tower.TowerType;
        TowerBundle = tower.TowerBundle;
        towerBase = tower;
        TowerKills = 0;

        SellReward = TowerData.SellReward;
        SetStatus();

        if(Projectile == null)
            Projectile = Resources.Load<GameObject>(Managers.Data.DefineData.PROJECTILE_PATH[(int)TowerType]);

        enhance = Managers.Enhance;
    }

    /// <summary>
    /// 보유 버프 및 강화수치를 계산해 공격 데미지 반환
    /// </summary>
    public float AttackDamage {
        get {
            float finalDamage = attackDamage;
            if (enhance != null && enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDamage).Level > 0) {
                finalDamage *= (1 + enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDamage).EnhanceValue);
            }
            if (towerBase != null && towerBase.BuffManager != null) {
                finalDamage = towerBase.BuffManager.CalculateAttackDamage(finalDamage);
            }
            return finalDamage;
        }
        set {
            if (attackDamage == value)
                return;

            attackDamage = value;
        }
    }

    /// <summary>
    /// 보유 버프 및 강화수치를 계산해 공격 딜레이 반환
    /// </summary>
    public float AttackDelay {
        get {
            float finalSpeed = attackDelay;

            if (enhance != null && enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDelay).Level > 0) {
                finalSpeed *= (1 - enhance.GetEnhanceValue(TowerBundle, Define.EnhanceType.AttackDelay).EnhanceValue * 0.5f);
            }
            if (towerBase != null && towerBase.BuffManager != null) {
                finalSpeed = towerBase.BuffManager.CalculateAttackDelay(finalSpeed);
            }

            if(finalSpeed <= 0)
                return 0;

            return finalSpeed;
        }
        set {
            if (attackDelay == value)
                return;

            attackDelay = value;
        }
    }
    //데이터 컨테이너
    public TowerData.TowerLevelData TowerData { get; private set; }

    //강화 클래스
    private EnhanceManager enhance;
    //공격 사거리
    public float AttackRange { get; private set; }
    //판매 비용
    public int SellReward { get; private set; }
    //발사체 경로
    public GameObject Projectile { get; private set; }
    //타워 타입
    public Define.TowerType TowerType { get; private set; }
    //타워 번들
    public Define.TowerBundle TowerBundle { get; private set; }
    //공격 딜레이
    private float attackDelay;
    //공격 데미지
    private float attackDamage;
    //대상 타워
    private TowerBase towerBase;
    //적 처치 수
    public int TowerKills { get; private set; }

    /// <summary>
    /// 판매비용 조정
    /// </summary>
    /// <param name="value"></param>
    public void SetReward(float value = 0) {
        SellReward = TowerData.SellRewardUpValue * (Level + 2);
        SellReward = Mathf.RoundToInt(SellReward * (1 + value));
    }

    /// <summary>
    /// 레벨업 시 능력치 재적용
    /// </summary>
    /// <param name="bundleType">업그레이드 할 타워 장르</param>
    public void SetStatus() {
        SetReward();
        AttackDamage = TowerData.AttackDamage;
        AttackDelay = TowerData.AttackDelay;
        AttackRange = TowerData.AttackRange;
        SellReward = TowerData.SellReward;
    }

    public void LevelUpStatus(int killCount = 0) {
        Level++;
        SetReward();
        TowerKills += killCount;
        AttackDamage = attackDamage * (TowerData.AttackDamageUpValue);
        AttackRange = TowerData.AttackRange + (TowerData.AttackRangeUpValue * (Level));
        AttackDelay = TowerData.AttackDelay * (1 - TowerData.AttackDelayDownValue * (Level));
    }


    /// <summary>
    /// 적 처치 시 킬 증가 및 액션 호출
    /// </summary>
    public void SetKill() {
        TowerKills++;
    }
}