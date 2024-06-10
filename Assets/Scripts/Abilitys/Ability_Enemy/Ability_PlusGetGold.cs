
using System.Linq;

/// <summary>
/// ������ ����Ǵ� �����Ƽ
/// �� ��� ��� ���� ����
/// </summary>
[Ability(Define.AbilityType.PlusGetGold)]
public class Ability_PlusGetGold : IEnemyAbility {
    //������ų ��差
    public float PlusGold { get; private set; }
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue { get; private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_PlusGetGold() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.PlusGetGold, Managers.Data.DefineData);
        PlusGold = Managers.Data.DefineData.ABILITY_PLUS_ENEMY_REWARD;
    }

    /// <summary>
    /// �������ִ� ��� �ֳʹ� ������ ����
    /// </summary>
    public void SetAbility() {
        var enemyList = Managers.Enemy.EnemyList;
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].EnemyStatus.SetReward(this);
        }
    }

    /// <summary>
    /// �ֳʹ� ���� ��, ��ų�� ������ ���¸�
    /// ������ �ֳʹ̿��� ��ų ����
    /// </summary>
    /// <param name="enemyBase">��ų�� ������ �ֳʹ�</param>
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetReward(this);
    }
}