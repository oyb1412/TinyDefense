using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

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
    //Ʈ������ ĳ��
    private Transform myTransform;
    //�� Ʈ������ ĳ��
    private Transform targetTransform;
    //�̵� �ڷ�ƾ
    private Coroutine velocityCoroutine;

    private void Awake() {
        if (myTransform == null)
            myTransform = transform;
    }

    private void Start() {
        if (explosionEffect == null)
            explosionEffect = Resources.Load<GameObject>(Managers.Data.DefineData.PROJECTILE_EXPLOSION_PATH[(int)towerBase.TowerType]);
    }

    /// <summary>
    /// �߻�ü ���� �� �ʱ�ȭ
    /// </summary>
    public void Init(TowerBase towerBase, TowerBase.AttackData attackData) {
        SoundManager.Instance.PlaySfx(Define.SFXType.FireProjectile);
        this.attackData = attackData;
        this.towerBase = towerBase;
        targetEnemy = towerBase.TargetEnemy;

        if(Util.IsEnemyNull(targetEnemy)) {
            Managers.Resources.Release(gameObject);
            return;
        }

        targetTransform = targetEnemy.transform;
        myTransform.position = this.towerBase.transform.position;
        destroyTimer = 0f;

        Vector3 targetPosition = targetTransform.position;
        Vector3 direction = targetPosition - myTransform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (velocityCoroutine != null)
            StopCoroutine(velocityCoroutine);

        velocityCoroutine = StartCoroutine(Co_Velocity());
    }

    /// <summary>
    /// �� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_Velocity() {

        while (true) {
            destroyTimer += Time.deltaTime;

            if (destroyTimer > Managers.Data.DefineData.PROJECTILE_DESTROY_TIME) {
                destroyTimer = 0f;
                Managers.Resources.Release(gameObject);
                yield break;
            }

            if (!Util.IsEnemyNull(targetEnemy)) {
                Vector3 targetPosition = targetTransform.position;
                Vector3 direction = targetPosition - myTransform.position;

                saveDir = direction.normalized;

                if (Vector2.Distance(myTransform.position, targetPosition) < Managers.Data.DefineData.PROJECTILE_PERMISSION_RANGE) {
                    Collison(targetEnemy);
                    yield break;
                }
            }

            myTransform.position += saveDir * Managers.Data.DefineData.PROJECTILE_VELOCITY * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// ����ü �浹 ��
    /// �Ϲ� ����ü�� ������ + �Ҹ�
    /// ���� ����ü�� �Ҹ�
    /// </summary>
    protected virtual void Collison(EnemyBase enemy) {
        CreateExplosion();
        Managers.Resources.Release(gameObject);

        //���� ��� ���¸�
        if (Util.IsEnemyNull(enemy))
            return;

        //���� ����
        if(attackData.IsStun) {
            enemy.DebuffManager.AddDebuff(new StunDebuff(Managers.Data.DefineData.ABILITY_STUN_DEFAULT_TIME), enemy);
        }
    }

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    protected abstract void CreateExplosion();
}
