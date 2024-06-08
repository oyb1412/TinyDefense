using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// 모든 버프 관리 매니저
/// 각 타워가 컴포넌트로 보유
/// </summary>
public class BuffManager {
    //버프 리스트
    public HashSet<IBuff> Buffs { get; private set; }

    //버프 적용 타입, 적용 유무 액션
    public Action<Define.BuffType, bool> BuffAction;

    public BuffManager() {
        if(Buffs == null) 
            Buffs = new HashSet<IBuff>((int)Define.BuffType.Count);

        BuffAction = null;
    }

    /// <summary>
    /// 버프 적용 및
    /// 버프 해제 예약
    /// </summary>
    /// <param name="buff">적용할 버프</param>
    /// <param name="tower">적용시킬 타워</param>
    public void AddBuff(IBuff buff, TowerBase tower) {
        foreach (var b in Buffs) {
            if(b.Type == buff.Type) {
                RemoveBuff(b, tower);
                break;
            }
        }

        Buffs.Add(buff);
        buff.ApplyBuff(tower);
        tower.StartCoroutine(Co_RemoveBuff(buff.BuffTime, buff, tower));
        BuffAction?.Invoke(buff.Type, true);
    }

    /// <summary>
    /// 버프 해제
    /// </summary>
    /// <param name="buff">해제할 버프</param>
    /// <param name="tower">해제할 타워</param>
    public void RemoveBuff(IBuff buff, TowerBase tower) {
        buff.RemoveBuff(tower);
        Buffs.Remove(buff);
        BuffAction?.Invoke(buff.Type, false);
    }

    /// <summary>
    /// 버프 해제 예약 코루틴
    /// </summary>
    /// <param name="time">버프 해제까지의 딜레이 시간</param>
    /// <param name="buff">해제할 버프</param>
    /// <param name="tower">해제할 타워</param>
    private IEnumerator Co_RemoveBuff(float time, IBuff buff, TowerBase tower) {
        float endTime = Time.time + time;
        while (Time.time < endTime) {
            yield return null;
        }
        RemoveBuff(buff, tower);
    }

    /// <summary>
    /// 보유 버프를 계산해 공격 데미지 계산
    /// </summary>
    /// <param name="baseDamage">원본 공격 데미지</param>
    /// <returns>계산 완료된 데미지</returns>
    public float CalculateAttackDamage(float baseDamage) {
        float finalDamage = baseDamage;
        foreach (var b in Buffs) {
            if(b.Type == Define.BuffType.AttackDamageUp && b.IsActive)
                finalDamage = b.ModifyValue(finalDamage);
        }
        return finalDamage;
    }

    /// <summary>
    /// 보유 버프를 계산해 공격 딜레이 계산
    /// </summary>
    /// <param name="baseDelay">원본 공격 딜레이</param>
    /// <returns>계산 완료된 딜레이</returns>
    public float CalculateAttackDelay(float baseDelay) {
        float finalDelay = baseDelay;
        foreach (var buff in Buffs) {
            if (buff.Type == Define.BuffType.AttackDelayDown && buff.IsActive)
                finalDelay = buff.ModifyValue(finalDelay);
        }
        return finalDelay;
    }
}