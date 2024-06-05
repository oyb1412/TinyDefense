using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UIElements;

/// <summary>
/// �ֺ� �� ���� Ŭ����
/// </summary>
public class EnemySearchSystem : MonoBehaviour
{
    //���� ���
    public EnemyBase TargetEnemy {
        get { return targetEnemy; }
        private set {
            towerBase.SetTargetEnemy(value);

            targetEnemy = value;
        }
    }

    //���� ���
    private EnemyBase targetEnemy;
    //�θ� Ÿ��
    private TowerBase towerBase;
    //������ �� ����Ʈ
    private HashSet<EnemyBase> enemyList = new HashSet<EnemyBase>(Define.ENEMY_SEARCH_MAX_COUNT);

    //Ÿ�� ��Ÿ� ��������Ʈ
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
            //�Ÿ����
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
    /// ����Ʈ�� ���� �߰��԰� ���ÿ� ü�¼� Sort
    /// </summary>
    /// <param name="enemy">�߰��� ��</param>
    private void AddEnemyAndSort(EnemyBase enemy) {
        enemyList.Add(enemy);

        if (Util.IsEnemyNull(TargetEnemy)) 
        {
            TargetEnemy = enemyList.OrderBy(e => e.EnemyStatus.CurrentHp).FirstOrDefault();
        }
    }

    /// <summary>
    /// ����Ʈ���� ���� �����԰� ���ÿ� ü�¼� Sort
    /// </summary>
    /// <param name="enemy">������ ��</param>
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

        // ��������Ʈ�� Pixels Per Unit �� ��������
        float pixelsPerUnit = RangeSprite.sprite.pixelsPerUnit;

        // ��������Ʈ�� ���� ũ�� (�ȼ� ����) ���ϱ�
        float spriteWidth = RangeSprite.sprite.rect.width;
        float spriteHeight = RangeSprite.sprite.rect.height;

        // ��������Ʈ�� ũ��� �ݶ��̴��� �������� �°� localScale ����
        float spriteUnitWidth = spriteWidth / pixelsPerUnit;
        float spriteUnitHeight = spriteHeight / pixelsPerUnit;

        // ��������Ʈ�� ���� ũ�� ����
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
