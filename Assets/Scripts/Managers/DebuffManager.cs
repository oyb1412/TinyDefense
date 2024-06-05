using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// ��� ����� ���� �Ŵ���
/// ��� ���� ������Ʈ�� ����
/// </summary>
public class DebuffManager {
    //����� ���
    public HashSet<IDebuff> Debuffs { get; private set; }
    //����� ���� Ÿ��, ���� ���� �׼�
    public Action<Define.DebuffType, bool> DebuffAction;

    public DebuffManager() {
        DebuffAction = null;
        Debuffs = new HashSet<IDebuff>();
    }

    /// <summary>
    /// ����� �߰� ��
    /// ���� ����
    /// </summary>
    /// <param name="debuff">�߰��� �����</param>
    /// <param name="enemy">�߰��� ��</param>
    public void AddDebuff(IDebuff debuff, EnemyBase enemy) {

        if (debuff.Bundle == Define.DebuffBundle.Movement)
        {
            foreach(var item in Debuffs) {
                if(item.Type == debuff.Type) {
                    RemoveDebuff(debuff, enemy);
                    break;
                }
            }
        }

        Debuffs.Add(debuff);
        debuff.ApplyDebuff(enemy);
        Managers.Instance.StartCoroutine(Co_RemoveDebuff(debuff.DebuffTime, debuff, enemy));
        DebuffAction?.Invoke(debuff.Type, true);
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="debuff">������ �����</param>
    /// <param name="enemy">������ ��</param>
    public void RemoveDebuff(IDebuff debuff, EnemyBase enemy) {
        debuff.RemoveDebuff(enemy);
        Debuffs.Remove(debuff);
        DebuffAction?.Invoke(debuff.Type, false);
    }

    /// <summary>
    /// ����� ���� ���� �ڷ�ƾ
    /// </summary>
    /// <param name="time">���������� ������</param>
    /// <param name="debuff">������ �����</param>
    /// <param name="enemy">������ ��</param>
    private IEnumerator Co_RemoveDebuff(float time, IDebuff debuff, EnemyBase enemy) {
        float endTime = Time.time + time;
        while (Time.time < endTime) {
            yield return null;
        }
        RemoveDebuff(debuff, enemy);
    }

    /// <summary>
    /// ������� ���� �̵��ӵ� ���
    /// </summary>
    /// <param name="baseSpeed">���� �̵��ӵ�</param>
    /// <returns>��� �� �̵��ӵ�</returns>
    public float CalculateMoveSpeed(float baseSpeed) {
        float finalSpeed = baseSpeed;
        foreach (var debuff in Debuffs) {
            if (debuff.Bundle == Define.DebuffBundle.Movement && debuff.IsActive)
                finalSpeed = debuff.ModifyMoveSpeed(finalSpeed);
        }
        return finalSpeed;
    }
}