using System;

/// <summary>
/// 어빌리티 속성
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AbilityAttribute : Attribute {
    public Define.AbilityType AbilityType { get; }

    public AbilityAttribute(Define.AbilityType abilityType) {
        AbilityType = abilityType;
    }
}