using UnityEngine;

/// <summary>
/// ���� ������ �ִϸ��̼� ǥ�� �� ���� Ŭ����
/// </summary>
public class BuffAdjustment : MonoBehaviour {
    //���� ���� Ÿ��
    private TowerBase towerBase;
    //���� ǥ�� �ִϸ�����
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
    /// ���� Ȱ��ȭ �� ��Ȱ��ȭ
    /// </summary>
    /// <param name="buffType">���� Ÿ��</param>
    /// <param name="trigger">Ȱ��ȭ ����</param>
    public void SetBuff(Define.BuffType buffType, bool trigger) {
        buffs[(int)buffType].enabled = trigger;
    }
}