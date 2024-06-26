using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 모든 어빌리티 관리 매니저
/// </summary>
public class AbilityManager : Attribute {
    //보유중인 어빌리티 리스트
    public Dictionary<Define.AbilityType, IAbility> AbilityList { get; private set; }
    //보유중인 공격 어빌리티 리스트
    public HashSet<IAttackAbility> AttackAbilityList { get; private set; }
    //보유중인 적 적용 어빌리티 리스트
    public HashSet<IEnemyAbility> EnemyAbilityList { get; private set; }
    //보유중인 타워 적용 어빌리티 리스트
    public HashSet<ITowerAbility> TowerAbilityList { get; private set; }


    public void Init() {

        AbilityList = new Dictionary<Define.AbilityType, IAbility>((int)Define.AbilityType.Count);
        AttackAbilityList = new HashSet<IAttackAbility>(Managers.Data.DefineData.ABILITY_ATTACK_COUNT);
        EnemyAbilityList = new HashSet<IEnemyAbility>(Managers.Data.DefineData.ABILITY_ENEMY_COUNT);
        TowerAbilityList = new HashSet<ITowerAbility>(Managers.Data.DefineData.ABILITY_TOWER_COUNT);
    }

    /// <summary>
    /// 어빌리티 추가(버튼으로 실행)
    /// </summary>
    public void AddAbility(IAbility ability) {
        Managers.Game.IsPlaying = true;

        if (AbilityList.ContainsKey(ability.AbilityValue.Type)) {
            ability.SetAbility();
            return;
        }

        AbilityList.Add(ability.AbilityValue.Type, ability);
        ApplyAbility(ability);

        UI_AbilitysPanel.Instance.AddAbilityIcon(ability);
    }

    /// <summary>
    /// 어빌리티 적용
    /// </summary>
    /// <param name="ability">적용할 스킬</param>
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
}