using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ����Ʈ ����
/// </summary>
public class ExplosionProjectileEffect : ProjectileEffectBase {
    //�߻��� Ÿ��
    protected TowerBase towerBase;
    //��Ÿ������ ���� ��ü�
    private HashSet<EnemyBase> enemyHash = new HashSet<EnemyBase>();
    /// <summary>
    /// ����Ʈ ���� �� �ʱ�ȭ �� ����
    /// </summary>
    /// <param name="towerBase">�߻��� Ÿ��</param>
    /// <param name="attackData">�߻��� Ÿ���� ������</param>
    /// <param name="pos">����Ʈ ���� ��ġ</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        if (towerBase.TowerType == Define.TowerType.Icemage)
            SoundManager.Instance.PlaySfx(Define.SFXType.IceExplosion);
        else
            SoundManager.Instance.PlaySfx(Define.SFXType.FireExplosion);

        this.towerBase = towerBase;
        base.Init(towerBase, attackData, pos);

        var enemyList = Managers.Enemy.EnemyList;

        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            float distance = Vector2.Distance(transform.position, enemyList[i].transform.position);

            //���� ��Ÿ� ���� ���� ����
            if (distance > Managers.Data.DefineData.TOWER_EXPLOSION_RADIUS)
                continue;

            //���� ���� ���ݴ����� �ʾҴٸ�
            if (enemyHash.Contains(enemyList[i]))
                continue;

            //���� �� ����� �Ұ�
            enemyHash.Add(enemyList[i]);
            //����� ����

            ExplosionAbility(enemyList[i]);
            enemyList[i].EnemyStatus.SetHp(attackData.Damage, towerBase);
        }
    }

    /// <summary>
    /// ����Ʈ ����(�ݹ����� ȣ��)
    /// </summary>
    public override void DestroyEvent() {
        enemyHash.Clear();
        base.DestroyEvent();
    }

    /// <summary>
    /// ���� �浹 ��, �������� ����� ����
    /// </summary>
    /// <param name="enemy">������� �����ų ��</param>
    private void ExplosionAbility(EnemyBase enemy) {
        foreach (var item in towerBase.Debuffs) {
            enemy.DebuffManager.AddDebuff(item, enemy);
        }
    }
}