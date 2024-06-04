using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

/// <summary>
/// ¾îºô¸®Æ¼ ÆÑÅä¸®
/// </summary>

public static class AbilityFactory {
    private static Dictionary<Define.AbilityType, Type> AbilityTypeMap;

    static AbilityFactory() {
        AbilityTypeMap = new Dictionary<Define.AbilityType, Type>();
        var AbilityTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(AbilityAttribute), false).Length > 0);

        foreach (var type in AbilityTypes) {
            var attribute = (AbilityAttribute)type.GetCustomAttributes(typeof(AbilityAttribute), false)[0];
            AbilityTypeMap[attribute.AbilityType] = type;
        }
    }

    public static IAbility CreateAbility(Define.AbilityType AbilityType) {
        if (AbilityTypeMap.TryGetValue(AbilityType, out var type)) {
            return (IAbility)Activator.CreateInstance(type);
        }
        return null;
    }
}
