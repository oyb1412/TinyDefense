using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using TMPro;

/// <summary>
/// 모든 프로젝타일 관리 클래스
/// </summary>
public abstract class ProjectileBase : AutoCachedMono
{
    //이 발사체를 생성한 타워
    protected TowerBase towerBase;
    //회전 방향
    private Vector3 saveDir;
    //충돌 이펙트 패스
    protected GameObject explosionEffect;
    //현재 표적
    private EnemyBase targetEnemy;
    //투사체 정보
    protected TowerBase.AttackData attackData;
    //적 트랜스폼 캐싱
    private Transform targetTransform;

    private bool hasHit;

    private float destroyTimer = 0f;

    private float collisonTimer = 0f;
    private float collisonCheck = 0.05f;

    protected override void Awake() {
        base.Awake();
    }

    private void Start() {
        if (explosionEffect == null)
            explosionEffect = Resources.Load<GameObject>(Managers.Data.DefineData.PROJECTILE_EXPLOSION_PATH[(int)towerBase.TowerType]);
    }

    /// <summary>
    /// 발사체 생성 및 초기화
    /// </summary>
    public void Init(TowerBase towerBase, TowerBase.AttackData attackData) {
        hasHit = false;
        Managers.Projectile.AddProjectile(this);
        destroyTimer = 0f;
        collisonTimer = 0f;
        SoundManager.Instance.PlaySfx(Define.SFXType.FireProjectile);
        this.attackData = attackData;
        this.towerBase = towerBase;
        targetEnemy = towerBase.TargetEnemy;
        if (Util.IsEnemyNull(targetEnemy)) {
            Managers.Resources.Release(gameObject);
            return;
        }

        targetTransform = targetEnemy.myTransform;
        myTransform.position = this.towerBase.myTransform.position;

        Vector3 targetPosition = targetTransform.position;
        Vector3 direction = targetPosition - myTransform.position;
        saveDir = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    private void DestroyProjectile() {
        if (!gameObject.activeInHierarchy)
            return;

        Managers.Projectile.RemoveProjectile(this);
        Managers.Resources.Release(gameObject);
    }

    public void UpdateProjectile() {
        destroyTimer += Time.deltaTime;

        if(destroyTimer >= Managers.Data.DefineData.PROJECTILE_DESTROY_TIME) {
            destroyTimer = 0;
            DestroyProjectile();
            return;
        }

        if (hasHit)
            return;

        myTransform.position += saveDir * Managers.Data.DefineData.PROJECTILE_VELOCITY * Time.deltaTime;

        collisonTimer += Time.deltaTime;

        if(collisonTimer >= collisonCheck) {
            if (!Util.SqrDistanceCheck(myTransform.position, targetTransform.position,
                0.3f)) {

                Collison(targetEnemy);
                hasHit = true;
                return; // 종료
            }
            collisonTimer = 0f;
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
