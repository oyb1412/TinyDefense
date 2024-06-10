/// <summary>
/// ������ ����Ǵ� �����Ƽ
/// �� �̵��ӵ� ������ ����
/// </summary>
[Ability(Define.AbilityType.MinusEnemyMoveSpeed)]
public class Ability_MinusEnemyMoveSpeed : IEnemyAbility {
    //���ҽ�ų �̵��ӵ� %
    public float MoveSpeedValue { get; private set; }
    //�����Ƽ ����
    public Define.AbilityValue AbilityValue{get;private set; }

    /// <summary>
    /// �����Ƽ Ÿ���� �������� �ʱ�ȭ
    /// Ÿ��, �̸�, ����, ������ ��������Ʈ
    /// </summary>
    public Ability_MinusEnemyMoveSpeed() {
        AbilityValue = new Define.AbilityValue(Define.AbilityType.MinusEnemyMoveSpeed, Managers.Data.DefineData);
        MoveSpeedValue = Managers.Data.DefineData.ABILITY_ENEMY_MOVESPEED_MINUS;
    }

    /// <summary>
    /// ��ų ���� ��, ������ ��� ������ ��ų ����
    /// </summary>
    public void SetAbility() {
        var enemyList = Managers.Enemy.EnemyList;
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            enemyList[i].EnemyStatus.SetMoveSpeed(this);
        }
    }

    /// <summary>
    /// �ֳʹ� ���� ��, ��ų�� ������ ���¸�
    /// ������ �ֳʹ̿��� ��ų ����
    /// </summary>
    /// <param name="enemyBase">��ų�� ������ �ֳʹ�</param>
    public void ExecuteEnemyAbility(EnemyBase enemyBase) {
        enemyBase.EnemyStatus.SetMoveSpeed(this);
    }
}