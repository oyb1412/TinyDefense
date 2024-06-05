using DG.Tweening;
using UnityEngine;

public class SelectArrow : MonoBehaviour {
    //Ÿ�� ���� ȭ��ǥ ��������Ʈ
    private SpriteRenderer arrowSprite;
    //ȭ��ǥ ��������Ʈ Ʈ����
    private Tween moveTween;
    //ȭ��ǥ �⺻ ��ġ
    private Vector3 arrowOriginalPosition;

    private void Awake() {
        arrowSprite = GetComponent<SpriteRenderer>();
        arrowOriginalPosition = arrowSprite.transform.localPosition;
        arrowSprite.enabled = false;
    }

    /// <summary>
    /// ȭ��ǥ Ȱ��ȭ
    /// Ʈ�� �ʱ�ȭ �� ����
    /// </summary>
    public void Activation() {
        arrowSprite.enabled = true;
        Util.ResetTween(moveTween);

        arrowSprite.transform.localPosition = arrowOriginalPosition;
        moveTween = arrowSprite.transform.DOMove(arrowSprite.transform.position + Vector3.up * 0.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// ȭ��ǥ ��Ȱ��ȭ
    /// Ʈ�� �ʱ�ȭ
    /// </summary>
    public void DeActivation() {
        Util.ResetTween(moveTween);
        arrowSprite.transform.localPosition = arrowOriginalPosition;
        arrowSprite.enabled = false;
    }
}