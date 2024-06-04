using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// 모든 디버프 관리 매니저
/// 모든 적이 컴포넌트로 보유
/// </summary>
public class DebuffManager {
    //디버프 목록
    public HashSet<IDebuff> Debuffs { get; private set; } = new HashSet<IDebuff>();
    //디버프 적용 타입, 적용 유무 액션
    public Action<Define.DebuffType, bool> DebuffAction;

    /// <summary>
    /// 디버프 추가 및
    /// 해제 예약
    /// </summary>
    /// <param name="debuff">추가할 디버프</param>
    /// <param name="enemy">추가할 적</param>
    public void AddDebuff(IDebuff debuff, EnemyBase enemy) {
        if (debuff.Bundle == Define.DebuffBundle.Movement)
        {
            if (Debuffs.FirstOrDefault(x => x.Type == debuff.Type) != null) {
                RemoveDebuff(debuff, enemy);
            }
        }

        Debuffs.Add(debuff);
        debuff.ApplyDebuff(enemy);
        Managers.Instance.StartCoroutine(Co_RemoveDebuff(debuff.DebuffTime, debuff, enemy));
        DebuffAction?.Invoke(debuff.Type, true);
    }

    /// <summary>
    /// 디버프 해제
    /// </summary>
    /// <param name="debuff">해제할 디버프</param>
    /// <param name="enemy">해제할 적</param>
    public void RemoveDebuff(IDebuff debuff, EnemyBase enemy) {
        
        debuff.RemoveDebuff(enemy);
        Debuffs.Remove(debuff);
        DebuffAction?.Invoke(debuff.Type, false);
    }

    /// <summary>
    /// 디버프 해제 예약 코루틴
    /// </summary>
    /// <param name="time">해제까지의 딜레이</param>
    /// <param name="debuff">해제할 디버프</param>
    /// <param name="enemy">해제할 적</param>
    private IEnumerator Co_RemoveDebuff(float time, IDebuff debuff, EnemyBase enemy) {
        yield return new WaitForSeconds(time);
        RemoveDebuff(debuff, enemy);
    }

    /// <summary>
    /// 디버프로 인한 이동속도 계산
    /// </summary>
    /// <param name="baseSpeed">원본 이동속도</param>
    /// <returns>계산 후 이동속도</returns>
    public float CalculateMoveSpeed(float baseSpeed) {
        float finalSpeed = baseSpeed;
        foreach (var debuff in Debuffs.Where(d => d.Bundle == Define.DebuffBundle.Movement)) {
            if(debuff.IsActive)
                finalSpeed = debuff.ModifyMoveSpeed(finalSpeed);
        }
        return finalSpeed;
    }
}