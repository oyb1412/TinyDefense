using System;

/// <summary>
/// �����Ƽ �Ӽ�
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AbilityAttribute : Attribute {
    public Define.AbilityType AbilityType { get; }

    public AbilityAttribute(Define.AbilityType abilityType) {
        AbilityType = abilityType;
    }
}