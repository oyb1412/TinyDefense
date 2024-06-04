using UnityEngine;
/// <summary>
/// �� ��� �̵��ӵ� ���� ����� ����
/// Ÿ���� ������Ʈ�� ����
/// </summary>
public abstract class MovementDebuff : IDebuff {
    //����� Ÿ��
    public Define.DebuffType Type { get; protected set; }
    //����� ����(�̵�)
    public Define.DebuffBundle Bundle => Define.DebuffBundle.Movement;
    //����� ���
    public float DebuffValue { get; protected set; }
    //����� ���� ����
    public bool IsActive { get; private set; }
    //����� ���� �ð�
    public float DebuffTime { get; protected set; }

    /// <summary>
    /// ����� ����
    /// </summary>
    public virtual void ApplyDebuff(EnemyBase enemy) {
        IsActive = true;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public virtual void RemoveDebuff(EnemyBase enemy) {
        IsActive = false;
    }

    /// <summary>
    /// ������� ���� �̵��ӵ� ��� �� ��ȯ
    /// </summary>
    /// <param name="baseSpeed">���� �̵��ӵ�</param>
    /// <returns>���� �̵��ӵ�</returns>
    public abstract float ModifyMoveSpeed(float baseSpeed);
}