using UnityEngine;

/// <summary>
/// �� ����� �ִϸ��̼� ǥ�� Ŭ����
/// </summary>
public class DebuffAdjustment : MonoBehaviour {
    //ǥ���� �� Ŭ����
    private EnemyBase enemyBase;
    //����� ǥ�� �ִϸ�����
    private SpriteRenderer[] debuffs;

    private void Awake() {
        enemyBase = GetComponentInParent<EnemyBase>();
        debuffs = GetComponentsInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// ����� Ȱ��ȭ�� �׼� ����
    /// </summary>
    private void OnEnable() {
        foreach (SpriteRenderer debuff in debuffs)
            debuff.enabled = false;

        enemyBase.DebuffManager.DebuffAction -= SetDebuff;
        enemyBase.DebuffManager.DebuffAction += SetDebuff;
    }

    /// <summary>
    /// ����� Ȱ��ȭ �� ��Ȱ��ȭ
    /// </summary>
    /// <param name="debuffType">����� Ÿ��</param>
    /// <param name="trigger">Ȱ��ȭ ����</param>
    public void SetDebuff(Define.DebuffType debuffType, bool trigger) {
        debuffs[(int)debuffType].enabled = trigger;
    }
}