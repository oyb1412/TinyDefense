using UnityEngine;
using DG.Tweening;

/// <summary>
/// 주변 적 감지 클래스
/// </summary>
public class EnemySearchSystem : MonoBehaviour
{
    //부모 타워
    private TowerBase towerBase;
    //타워 사거리 스프라이트
    public SpriteRenderer RangeSprite { get; private set; }
    //타워 선택 마크
    private SelectArrow selectArrow;
    //타워 선택 및 선택해제 트윈
    private Tween rangeTween;
    //애너미 레이어
    private LayerMask enemyLayer;

    private void Awake() {
        towerBase = GetComponentInParent<TowerBase>();
        RangeSprite = GetComponentInChildren<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
        RangeSprite.enabled = false;
        enemyLayer = LayerMask.GetMask(Define.TAG_ENEMY);
    }

    /// <summary>
    /// 공격 사거리 내의 적 서치
    /// </summary>
    /// <returns></returns>
    public EnemyBase SearchEnemy() {
        if (towerBase == null)
            return null;

        var hit = Physics2D.CircleCast(towerBase.transform.position, towerBase.TowerStatus.AttackRange * 0.3f, Vector2.zero, enemyLayer);

        if(hit.collider != null) {
            return hit.collider.GetComponent<EnemyBase>();
        }
        return null;
    }

    /// <summary>
    /// 현재 타겟 애너미가 사거리 내에 있는지 체크
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public EnemyBase TargetEnemyCheck(EnemyBase target) {
        if(target == null) 
            return null;

        if(Vector2.Distance(towerBase.transform.position, target.transform.position) >= towerBase.TowerStatus.AttackRange * 0.2f) {
            return null;
        }

        return target;
    }

    /// <summary>
    /// 타워 선택시 사거리 및 마크 표시
    /// </summary>
    public void Activation() {
        SpritesActivation();

        // 스프라이트의 Pixels Per Unit 값 가져오기
        float pixelsPerUnit = RangeSprite.sprite.pixelsPerUnit;

        // 스프라이트의 실제 크기 (픽셀 단위) 구하기
        float spriteWidth = RangeSprite.sprite.rect.width;
        float spriteHeight = RangeSprite.sprite.rect.height;

        // 스프라이트의 크기와 콜라이더의 반지름에 맞게 localScale 조정
        float spriteUnitWidth = spriteWidth / pixelsPerUnit;
        float spriteUnitHeight = spriteHeight / pixelsPerUnit;

        // 스프라이트의 유닛 크기 비율
        float scaleX = (towerBase.TowerStatus.AttackRange) / spriteUnitWidth;
        float scaleY = (towerBase.TowerStatus.AttackRange) / spriteUnitHeight;

        Vector3 rangeScale = Vector3.zero;


        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => rangeScale, x => rangeScale = x, new Vector3(scaleX, scaleY, 1), .5f)
            .OnUpdate(() => RangeSprite.transform.localScale = rangeScale);
    }

    /// <summary>
    /// 타워 선택 해제시 마크 및 사거리 비활성화
    /// </summary>
    public void DeActivation() {
        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => RangeSprite.transform.localScale, x => RangeSprite.transform.localScale = x,
            Vector3.zero, .5f)
            .OnComplete(() => RangeSprite.enabled = false);

        selectArrow.DeActivation();
    }

    /// <summary>
    /// 사거리 스프라이트 활성화
    /// </summary>
    private void SpritesActivation() {
        RangeSprite.enabled = true;
        selectArrow.Activation();
    }
}
