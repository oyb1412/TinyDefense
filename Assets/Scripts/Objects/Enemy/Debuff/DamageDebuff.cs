/// <summary>
/// �� ��� ���ӵ����� ����� ���� Ŭ����
/// Ÿ���� ������Ʈ�� ����
/// </summary>
public class DamageDebuff : IDebuff {
    //����� Ÿ��
    public Define.DebuffType Type { get; protected set; }
    //����� ����(���ӵ�����)
    public Define.DebuffBundle Bundle => Define.DebuffBundle.Damage;
    //����� ���
    public float DebuffValue { get; protected set; }
    //����� ���� ����
    public bool IsActive { get; private set; }
    //����� ���ӽð�
    public float DebuffTime { get; protected set; }

    /// <summary>
    /// ����� ����
    /// </summary>
    public void ApplyDebuff(EnemyBase enemy) {
        IsActive = true;
    }

    /// <summary>
    /// ����� ����
    /// </summary>
    public void RemoveDebuff(EnemyBase enemy) {
        IsActive = false;
    }

    /// <summary>
    /// ������� ���� �̵��ӵ� ���� 
    /// </summary>
    /// <param name="baseSpeed">���� �ӵ�</param>
    public float ModifyMoveSpeed(float baseSpeed) {
        return baseSpeed;
    }
}