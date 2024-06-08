using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// ��� ���� ���� �Ŵ���
/// �� Ÿ���� ������Ʈ�� ����
/// </summary>
public class BuffManager {
    //���� ����Ʈ
    public HashSet<IBuff> Buffs { get; private set; }

    //���� ���� Ÿ��, ���� ���� �׼�
    public Action<Define.BuffType, bool> BuffAction;

    public BuffManager() {
        if(Buffs == null) 
            Buffs = new HashSet<IBuff>((int)Define.BuffType.Count);

        BuffAction = null;
    }

    /// <summary>
    /// ���� ���� ��
    /// ���� ���� ����
    /// </summary>
    /// <param name="buff">������ ����</param>
    /// <param name="tower">�����ų Ÿ��</param>
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
    /// ���� ����
    /// </summary>
    /// <param name="buff">������ ����</param>
    /// <param name="tower">������ Ÿ��</param>
    public void RemoveBuff(IBuff buff, TowerBase tower) {
        buff.RemoveBuff(tower);
        Buffs.Remove(buff);
        BuffAction?.Invoke(buff.Type, false);
    }

    /// <summary>
    /// ���� ���� ���� �ڷ�ƾ
    /// </summary>
    /// <param name="time">���� ���������� ������ �ð�</param>
    /// <param name="buff">������ ����</param>
    /// <param name="tower">������ Ÿ��</param>
    private IEnumerator Co_RemoveBuff(float time, IBuff buff, TowerBase tower) {
        float endTime = Time.time + time;
        while (Time.time < endTime) {
            yield return null;
        }
        RemoveBuff(buff, tower);
    }

    /// <summary>
    /// ���� ������ ����� ���� ������ ���
    /// </summary>
    /// <param name="baseDamage">���� ���� ������</param>
    /// <returns>��� �Ϸ�� ������</returns>
    public float CalculateAttackDamage(float baseDamage) {
        float finalDamage = baseDamage;
        foreach (var b in Buffs) {
            if(b.Type == Define.BuffType.AttackDamageUp && b.IsActive)
                finalDamage = b.ModifyValue(finalDamage);
        }
        return finalDamage;
    }

    /// <summary>
    /// ���� ������ ����� ���� ������ ���
    /// </summary>
    /// <param name="baseDelay">���� ���� ������</param>
    /// <returns>��� �Ϸ�� ������</returns>
    public float CalculateAttackDelay(float baseDelay) {
        float finalDelay = baseDelay;
        foreach (var buff in Buffs) {
            if (buff.Type == Define.BuffType.AttackDelayDown && buff.IsActive)
                finalDelay = buff.ModifyValue(finalDelay);
        }
        return finalDelay;
    }
}