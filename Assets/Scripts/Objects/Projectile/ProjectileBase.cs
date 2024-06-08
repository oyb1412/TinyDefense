using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

/// <summary>
/// 모든 프로젝타일 관리 클래스
/// </summary>
public abstract class ProjectileBase : MonoBehaviour
{
    //이 발사체를 생성한 타워
    protected TowerBase towerBase;
    //발사체 자동 삭제 타이머
    private float destroyTimer;
    //회전 방향
    private Vector3 saveDir;
    //충돌 이펙트 패스
    protected GameObject explosionEffect;
    //현재 표적
    private EnemyBase targetEnemy;
    //투사체 정보
    protected TowerBase.AttackData attackData;
    //트랜스폼 캐싱
    private Transform myTransform;
    //적 트랜스폼 캐싱
    private Transform targetTransform;
    //이동 코루틴
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
    /// 발사체 생성 및 초기화
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
    /// 적 추적 코루틴
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
    /// 투사체 충돌 시
    /// 일반 투사체는 데미지 + 소멸
    /// 폭발 투사체는 소멸
    /// </summary>
    protected virtual void Collison(EnemyBase enemy) {
        CreateExplosion();
        Managers.Resources.Release(gameObject);

        //적이 사망 상태면
        if (Util.IsEnemyNull(enemy))
            return;

        //스턴 적용
        if(attackData.IsStun) {
            enemy.DebuffManager.AddDebuff(new StunDebuff(Managers.Data.DefineData.ABILITY_STUN_DEFAULT_TIME), enemy);
        }
    }

    /// <summary>
    /// 폭발 이펙트 생성
    /// </summary>
    protected abstract void CreateExplosion();
}
