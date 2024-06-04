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
    public HashSet<IBuff> Buffs { get; private set; } = new HashSet<IBuff>();

    //���� ���� Ÿ��, ���� ���� �׼�
    public Action<Define.BuffType, bool> BuffAction;

    /// <summary>
    /// ���� ���� ��
    /// ���� ���� ����
    /// </summary>
    /// <param name="buff">������ ����</param>
    /// <param name="tower">�����ų Ÿ��</param>
    public void AddBuff(IBuff buff, TowerBase tower) {
        if (Buffs.FirstOrDefault(x => x.Type == buff.Type) != null) {
            RemoveBuff(buff, tower);
        }

        Buffs.Add(buff);
        buff.ApplyBuff(tower);
        Managers.Instance.StartCoroutine(Co_RemoveBuff(buff.BuffTime, buff, tower));
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
        yield return new WaitForSeconds(time);
        RemoveBuff(buff, tower);
    }

    /// <summary>
    /// ���� ������ ����� ���� ������ ���
    /// </summary>
    /// <param name="baseDamage">���� ���� ������</param>
    /// <returns>��� �Ϸ�� ������</returns>
    public float CalculateAttackDamage(float baseDamage) {
        float finalDamage = baseDamage;
        foreach (var buff in Buffs.Where(d => d.Type == Define.BuffType.AttackDamageUp)) {
            if(buff.IsActive)
                finalDamage = buff.ModifyValue(finalDamage);
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
        foreach (var buff in Buffs.Where(d => d.Type == Define.BuffType.AttackDelayDown)) {
            if (buff.IsActive)
                finalDelay = buff.ModifyValue(finalDelay);
        }
        return finalDelay;
    }
}