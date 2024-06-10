/// <summary> 
/// ��� ���� ���� �������̽�
/// </summary>
public interface IBuff {
    //���� ���
    float BuffValue { get; }
    //���� ���ӽð�
    float BuffTime { get; }
    //���� Ÿ��
    Define.BuffType Type { get; }
    //���� ����
    void ApplyBuff(TowerBase tower);
    //���� ����
    void RemoveBuff(TowerBase tower);
    //���� ��� ���
    float ModifyValue(float baseValue); 
    //���� ���� ����
    bool IsActive { get; } 
}