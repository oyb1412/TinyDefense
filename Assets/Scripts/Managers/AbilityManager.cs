using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ��� �����Ƽ ���� �Ŵ���
/// </summary>
public class AbilityManager : Attribute {
    //�������� �����Ƽ ����Ʈ
    public Dictionary<Define.AbilityType, IAbility> AbilityList { get; private set; }
    //�������� ���� �����Ƽ ����Ʈ
    public HashSet<IAttackAbility> AttackAbilityList { get; private set; }
    //�������� �� ���� �����Ƽ ����Ʈ
    public HashSet<IEnemyAbility> EnemyAbilityList { get; private set; }
    //�������� Ÿ�� ���� �����Ƽ ����Ʈ
    public HashSet<ITowerAbility> TowerAbilityList { get; private set; }

    public void Init() {
        AbilityList = new Dictionary<Define.AbilityType, IAbility>((int)Define.AbilityType.Count);
        AttackAbilityList = new HashSet<IAttackAbility>();
        EnemyAbilityList = new HashSet<IEnemyAbility>();
        TowerAbilityList = new HashSet<ITowerAbility>();
    }

    /// <summary>
    /// �����Ƽ �߰�(��ư���� ����)
    /// </summary>
    public void AddAbility(IAbility ability) {
        Managers.Game.IsPlaying = true;

        if (AbilityList.ContainsKey(ability.AbilityValue.Type))
            return;

        AbilityList.Add(ability.AbilityValue.Type, ability);
        ApplyAbility(ability);

        UI_AbilitysPanel.Instance.AddAbilityIcon(ability);
    }

    /// <summary>
    /// �����Ƽ ����
    /// </summary>
    /// <param name="ability">������ ��ų</param>
    private void ApplyAbility(IAbility ability) {
        ability.SetAbility();
        switch (ability) {
            case IAttackAbility attackAbility:
                if (!AttackAbilityList.Contains(attackAbility))
                    AttackAbilityList.Add(attackAbility);
                break;
            case IEnemyAbility enemyAbility:
                if (!EnemyAbilityList.Contains(enemyAbility))
                    EnemyAbilityList.Add(enemyAbility);
                break;
            case ITowerAbility systemAbility:
                if (!TowerAbilityList.Contains(systemAbility))
                    TowerAbilityList.Add(systemAbility);
                break;
        }
    }

    /// <summary>
    /// �����Ƽ ����Ʈ ��ȯ
    /// </summary>
    public IEnumerable<T> GetAbilitysOfType<T>() where T : IAbility {
        var abilitysSnapshot = AbilityList.Values.ToList(); 

        foreach (var ability in abilitysSnapshot) {
            if (ability is T tAbility) {
                yield return tAbility;
            }
        }
    }
}