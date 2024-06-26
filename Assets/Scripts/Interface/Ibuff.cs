
public interface IBuff {
    
    float BuffValue { get; }
    
    float BuffTime { get; }
    
    Define.BuffType Type { get; }
    
    void ApplyBuff(TowerBase tower);
    
    void RemoveBuff(TowerBase tower);
    
    float ModifyValue(float baseValue); 
    
    bool IsActive { get; } 
}