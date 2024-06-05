using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UIElements;

/// <summary>
/// 주변 적 감지 클래스
/// </summary>
public class EnemySearchSystem : MonoBehaviour
{
    //공격 대상
    public EnemyBase TargetEnemy {
        get { return targetEnemy; }
        private set {
            towerBase.SetTargetEnemy(value);

            targetEnemy = value;
        }
    }

    //공격 대상
    private EnemyBase targetEnemy;
    //부모 타워
    private TowerBase towerBase;
    //감지된 적 리스트
    private HashSet<EnemyBase> enemyList = new HashSet<EnemyBase>(Define.ENEMY_SEARCH_MAX_COUNT);

    //타워 사거리 스프라이트
    public SpriteRenderer RangeSprite { get; private set; }
    private SelectArrow selectArrow;

    private Tween rangeTween;

    private void Awake() {
        towerBase = GetComponentInParent<TowerBase>();
        RangeSprite = GetComponentInChildren<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
        RangeSprite.enabled = false;
    }

    private void Update() {
        if (Managers.Enemy.enemyList.Count == 0)
            return;

        var nullList = enemyList.Where(x => Util.IsEnemyNull(x)).ToList();

        foreach (var item in nullList) {
            enemyList.Remove(item);
        }

        foreach (EnemyBase enemy in Managers.Enemy.enemyList) {
            //거리계산
            if (Vector2.Distance(towerBase.transform.position, enemy.transform.position) < towerBase.TowerStatus.AttackRange / 3) {
                if (enemyList.Contains(enemy))
                    continue;

                if (enemyList.Count >= Define.ENEMY_SEARCH_MAX_COUNT)
                    continue;

                AddEnemyAndSort(enemy);
            }
            else {
                if (enemyList.Count == 0)
                    continue;

                if (!enemyList.Contains(enemy))
                    continue;

                if (enemy == TargetEnemy)
                    TargetEnemy = null;

                RemoveEnemyAndSort(enemy);
            }
        }
    }

    public EnemyBase GetRandomEnemy(EnemyBase enemy) {
        if(enemyList.Count > 0) {
            if (Vector2.Distance(towerBase.transform.position, enemy.transform.position)
                 < towerBase.TowerStatus.AttackRange / 3)
                return enemy;
        }
        return null;
    }

    /// <summary>
    /// 리스트에 적을 추가함과 동시에 체력순 Sort
    /// </summary>
    /// <param name="enemy">추가할 적</param>
    private void AddEnemyAndSort(EnemyBase enemy) {
        enemyList.Add(enemy);

        if (Util.IsEnemyNull(TargetEnemy)) 
        {
            TargetEnemy = enemyList.OrderBy(e => e.EnemyStatus.CurrentHp).FirstOrDefault();
        }
    }

    /// <summary>
    /// 리스트에서 적을 제거함과 동시에 체력순 Sort
    /// </summary>
    /// <param name="enemy">제거할 적</param>
    private void RemoveEnemyAndSort(EnemyBase enemy) {
        enemyList.Remove(enemy);

        if (enemyList.Count == 0)
            TargetEnemy = null;

        if (Util.IsEnemyNull(TargetEnemy) && enemyList.Count != 0) 
        {
            TargetEnemy = enemyList.OrderBy(e => e.EnemyStatus.CurrentHp).FirstOrDefault();
        }
    }

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

    public void DeActivation() {
        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => RangeSprite.transform.localScale, x => RangeSprite.transform.localScale = x,
            Vector3.zero, .5f)
            .OnComplete(() => RangeSprite.enabled = false);

        selectArrow.DeActivation();
    }

    private void SpritesActivation() {
        RangeSprite.enabled = true;
        selectArrow.Activation();
    }
}
