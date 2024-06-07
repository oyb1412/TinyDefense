using UnityEngine;

/// <summary>
/// 모든 발사체 충돌 이펙트 관리
/// </summary>
public class ProjectileEffectBase : MonoBehaviour {
    //공격 데이터
    protected TowerBase.AttackData attackData;
    //이펙트 스프라이트 렌터러
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        if(spriteRenderer == null) 
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 이펙트 제거(애니메이션 이벤트 콜백으로 호출)
    /// </summary>
    public virtual void DestroyEvent() {
        Managers.Resources.Release(gameObject);
    }

    /// <summary>
    /// 생성 시 초기화
    /// 공격 정보를 바탕으로
    /// 이펙트 색 변경
    /// </summary>
    /// <param name="towerBase">발사한 타워</param>
    /// <param name="attackData">공격 데이터</param>
    /// <param name="pos">이펙트 생성 위치</param>
    public virtual void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        this.attackData = attackData;
        transform.position = pos;

        if(this.attackData.IsCritical)
            spriteRenderer.color = Color.red;
        else if(this.attackData.IsMiss) 
            spriteRenderer.color = Color.black;
        else
            spriteRenderer.color = Color.white;
    }
}