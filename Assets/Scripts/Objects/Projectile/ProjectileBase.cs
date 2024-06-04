using UnityEngine;

/// <summary>
/// ��� ������Ÿ�� ���� Ŭ����
/// </summary>
public abstract class ProjectileBase : MonoBehaviour
{
    //�� �߻�ü�� ������ Ÿ��
    protected TowerBase towerBase;
    //�߻�ü �ڵ� ���� Ÿ�̸�
    private float destroyTimer;
    //ȸ�� ����
    private Vector3 saveDir;
    //�浹 ����Ʈ �н�
    protected GameObject explosionEffect;
    //���� ǥ��
    private EnemyBase targetEnemy;
    //����ü ����
    protected TowerBase.AttackData attackData;

    /// <summary>
    /// �߻�ü ���� �� �ʱ�ȭ
    /// </summary>
    public void Init(TowerBase towerBase, TowerBase.AttackData attackData) {
        this.attackData = attackData;
        this.towerBase = towerBase;
        targetEnemy = towerBase.TargetEnemy;
        transform.position = this.towerBase.transform.position;
        explosionEffect = Resources.Load<GameObject>(Define.PROJECTILE_EXPLOSION_PATH[(int)towerBase.TowerType]);
    }

    /// <summary>
    /// �߻�ü �ڵ� ���� 
    /// </summary>
    private void Update() {
        destroyTimer += Time.deltaTime;

        if(destroyTimer > Define.PROJECTILE_DESTROY_TIME) {
            destroyTimer = 0f;
            Managers.Resources.Destroy(gameObject);
        }

        if (!Util.IsEnemyNull(targetEnemy)) {
            Vector3 direction = targetEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            saveDir = direction.normalized;

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) < Define.PROJECTILE_PERMISSION_RANGE) {
                Collison(targetEnemy);
                return;
            }
        }

        transform.position += saveDir * Define.PROJECTILE_VELOCITY * Time.deltaTime;
    }

    /// <summary>
    /// ����ü �浹 ��
    /// �Ϲ� ����ü�� ������ + �Ҹ�
    /// ���� ����ü�� �Ҹ�
    /// </summary>
    protected virtual void Collison(EnemyBase enemy) {
        CreateExplosion();
        Managers.Resources.Destroy(gameObject);

        //���� ����
        if(attackData.IsStun) {
            enemy.DebuffManager.AddDebuff(new StunDebuff(Define.ABILITY_STUN_DEFAULT_TIME), enemy);
        }
    }

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    protected abstract void CreateExplosion();
}
