using UnityEngine;

/// <summary>
/// 버프 아이콘 애니메이션 표기 및 관리 클래스
/// </summary>
public class BuffAdjustment : MonoBehaviour {
    //버프 적용 타워
    private TowerBase towerBase;
    //버프 표기 애니메이터
    private SpriteRenderer[] buffs;

    private void Awake() {
        towerBase = GetComponentInParent<TowerBase>();
        buffs = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnEnable() {
        foreach (SpriteRenderer buff in buffs)
            buff.enabled = false;

        towerBase.BuffManager.BuffAction -= SetBuff;
        towerBase.BuffManager.BuffAction += SetBuff;
    }

    /// <summary>
    /// 버프 활성화 및 비활성화
    /// </summary>
    /// <param name="buffType">버프 타입</param>
    /// <param name="trigger">활성화 유무</param>
    public void SetBuff(Define.BuffType buffType, bool trigger) {
        buffs[(int)buffType].enabled = trigger;
    }
}