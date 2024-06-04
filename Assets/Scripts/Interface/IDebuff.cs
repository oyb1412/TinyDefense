/// <summary>
/// ��� ����� ���� �������̽�
/// </summary>
public interface IDebuff {
    //����� ���
    float DebuffValue { get; }
    //����� ���ӽð�
    float DebuffTime { get; }
    //����� Ÿ��
    Define.DebuffType Type { get; }
    //����� ���� Ÿ��
    Define.DebuffBundle Bundle { get; }
    //����� ����
    void ApplyDebuff(EnemyBase enemy);
    //����� ����
    void RemoveDebuff(EnemyBase enemy);
    //������� ���� �̵��ӵ� ���
    float ModifyMoveSpeed(float baseSpeed); 
    //����� ���� ����
    bool IsActive { get; }
}