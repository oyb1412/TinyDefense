using UnityEngine;

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

    /// <summary>
    /// 발사체 생성 및 초기화
    /// </summary>
    public void Init(TowerBase towerBase, TowerBase.AttackData attackData) {
        this.attackData = attackData;
        this.towerBase = towerBase;
        targetEnemy = towerBase.TargetEnemy;
        transform.position = this.towerBase.transform.position;
        explosionEffect = Resources.Load<GameObject>(Define.PROJECTILE_EXPLOSION_PATH[(int)towerBase.TowerType]);
    }

    /// <summary>
    /// 발사체 자동 삭제 
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
    /// 투사체 충돌 시
    /// 일반 투사체는 데미지 + 소멸
    /// 폭발 투사체는 소멸
    /// </summary>
    protected virtual void Collison(EnemyBase enemy) {
        CreateExplosion();
        Managers.Resources.Destroy(gameObject);

        //스턴 적용
        if(attackData.IsStun) {
            enemy.DebuffManager.AddDebuff(new StunDebuff(Define.ABILITY_STUN_DEFAULT_TIME), enemy);
        }
    }

    /// <summary>
    /// 폭발 이펙트 생성
    /// </summary>
    protected abstract void CreateExplosion();
}
