using UnityEngine;
using DG.Tweening;

/// <summary>
/// �ֺ� �� ���� Ŭ����
/// </summary>
public class EnemySearchSystem : MonoBehaviour
{
    //�θ� Ÿ��
    private TowerBase towerBase;
    //Ÿ�� ��Ÿ� ��������Ʈ
    public SpriteRenderer RangeSprite { get; private set; }
    //Ÿ�� ���� ��ũ
    private SelectArrow selectArrow;
    //Ÿ�� ���� �� �������� Ʈ��
    private Tween rangeTween;
    //�ֳʹ� ���̾�
    private LayerMask enemyLayer;

    private void Awake() {
        towerBase = GetComponentInParent<TowerBase>();
        RangeSprite = GetComponentInChildren<SpriteRenderer>();
        selectArrow = GetComponentInChildren<SelectArrow>();
        RangeSprite.enabled = false;
        enemyLayer = LayerMask.GetMask(Define.TAG_ENEMY);
    }

    /// <summary>
    /// ���� ��Ÿ� ���� �� ��ġ
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
    /// ���� Ÿ�� �ֳʹ̰� ��Ÿ� ���� �ִ��� üũ
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
    /// Ÿ�� ���ý� ��Ÿ� �� ��ũ ǥ��
    /// </summary>
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

    /// <summary>
    /// Ÿ�� ���� ������ ��ũ �� ��Ÿ� ��Ȱ��ȭ
    /// </summary>
    public void DeActivation() {
        Util.ResetTween(rangeTween);
        rangeTween = DOTween.To(() => RangeSprite.transform.localScale, x => RangeSprite.transform.localScale = x,
            Vector3.zero, .5f)
            .OnComplete(() => RangeSprite.enabled = false);

        selectArrow.DeActivation();
    }

    /// <summary>
    /// ��Ÿ� ��������Ʈ Ȱ��ȭ
    /// </summary>
    private void SpritesActivation() {
        RangeSprite.enabled = true;
        selectArrow.Activation();
    }
}
