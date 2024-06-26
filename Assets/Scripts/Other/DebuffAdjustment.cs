using UnityEngine;

/// <summary>
/// 적 디버프 애니메이션 표기 클래스
/// </summary>
public class DebuffAdjustment : MonoBehaviour {
    //표기할 적 클래스
    private EnemyBase enemyBase;
    //디버프 표기 애니메이터
    private SpriteRenderer[] debuffs;

    private void Awake() {
        enemyBase = GetComponentInParent<EnemyBase>();
        debuffs = GetComponentsInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// 디버프 활성화시 액션 연동
    /// </summary>
    private void OnEnable() {
        foreach (SpriteRenderer debuff in debuffs)
            debuff.enabled = false;

        enemyBase.DebuffManager.DebuffAction -= SetDebuff;
        enemyBase.DebuffManager.DebuffAction += SetDebuff;
    }

    /// <summary>
    /// 디버프 활성화 및 비활성화
    /// </summary>
    /// <param name="debuffType">디버프 타입</param>
    /// <param name="trigger">활성화 유무</param>
    public void SetDebuff(Define.DebuffType debuffType, bool trigger) {
        debuffs[(int)debuffType].enabled = trigger;
    }
}