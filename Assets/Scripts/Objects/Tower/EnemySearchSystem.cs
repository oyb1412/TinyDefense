using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EnemySearchSystem : MonoBehaviour
{
    
    private TowerBase towerBase;
    public SpriteRenderer RangeSprite { get; private set; }
   
    private SelectArrow selectArrow;
    
    private Tween rangeTween;
       
    private Coroutine rotateCoroutine;

    private void Awake() {
        towerBase = GetComponentInParent<TowerBase>();
        RangeSprite = GetComponentInChildren<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
        RangeSprite.enabled = false;
    }

    private IEnumerator Co_RangeSpriteRotate() {
        while(true) {
            if(RangeSprite.enabled)
                RangeSprite.transform.localRotation *= Quaternion.Euler(0, 0, Managers.Data.DefineData.TOWER_RANGE_ROTATE_SPEED * Time.deltaTime);

            yield return null;
        }
    }

    /// <summary>
    /// 타겟 적이 존재하지 않을 때, 주변 적 서치 시도
    /// </summary>
    /// <returns>서치 결과</returns>
    public EnemyBase SearchEnemy() {
        if (towerBase == null)
            return null;

        //필드에 존재하는 적 리스트를 받아온다.
        var enemyList = Managers.Enemy.EnemyList;

        //적 리스트를 순회하며, 공격 사거리 내의 적을 서치
        for (int i = enemyList.Count - 1; i >= 0; i--) {
            if (Util.IsEnemyNull(enemyList[i]))
                continue;

            if (Vector2.Distance(transform.position, enemyList[i].transform.position) >= towerBase.TowerStatus.AttackRange * Managers.Data.DefineData.TOWER_RANGE)
                continue;

            //성공시 서치한 적 리턴
            return enemyList[i];
        }

        //실패시 null 리턴
        return null;
    }

    /// <summary>
    /// 내가 타겟팅한 적이 공격 사거리 내에 있나 체크
    /// </summary>
    /// <param name="target">타겟팅한 적</param>
    /// <returns>타겟팅한 적</returns>
    public EnemyBase TargetEnemyCheck(EnemyBase target) {
        if(target == null) 
            return null;

        //적이 공격 범위를 벗어났으면, null 리턴
        if(Vector2.Distance(transform.position, target.transform.position) >= towerBase.TowerStatus.AttackRange * Managers.Data.DefineData.TOWER_RANGE) {
            return null;
        }

        //아니라면 그대로 리턴
        return target;
    }

    
    public void Activation() {
        SpritesActivation();

      
        float pixelsPerUnit = RangeSprite.sprite.pixelsPerUnit;

        
        float spriteWidth = RangeSprite.sprite.rect.width;
        float spriteHeight = RangeSprite.sprite.rect.height;

       
        float spriteUnitWidth = spriteWidth / pixelsPerUnit;
        float spriteUnitHeight = spriteHeight / pixelsPerUnit;

        
        float scaleX = (towerBase.TowerStatus.AttackRange) / spriteUnitWidth;
        float scaleY = (towerBase.TowerStatus.AttackRange) / spriteUnitHeight;

        Vector3 rangeScale = Vector3.zero;


        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => rangeScale, x => rangeScale = x, new Vector3(scaleX, scaleY, 1), .5f)
            .OnUpdate(() => RangeSprite.transform.localScale = rangeScale);

        if(rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(Co_RangeSpriteRotate());
    }

  
    public void DeActivation() {
        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => RangeSprite.transform.localScale, x => RangeSprite.transform.localScale = x,
            Vector3.zero, .5f)
            .OnComplete(() => RangeSprite.enabled = false);

        selectArrow.DeActivation();

        if (rotateCoroutine != null) {
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
        }
    }

   
    private void SpritesActivation() {
        RangeSprite.enabled = true;
        selectArrow.Activation();
    }
}
