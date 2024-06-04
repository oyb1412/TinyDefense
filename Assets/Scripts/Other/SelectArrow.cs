using DG.Tweening;
using UnityEngine;

public class SelectArrow : MonoBehaviour {
    //타워 선택 화살표 스프라이트
    private SpriteRenderer arrowSprite;
    //화살표 스프라이트 트위닝
    private Tween moveTween;
    private Vector3 arrowOriginalPosition;

    private void Awake() {
        arrowSprite = GetComponent<SpriteRenderer>();
        arrowOriginalPosition = arrowSprite.transform.localPosition;
        arrowSprite.enabled = false;
    }

    public void Activation() {
        arrowSprite.enabled = true;
        Util.ResetTween(moveTween);

        arrowSprite.transform.localPosition = arrowOriginalPosition;
        moveTween = arrowSprite.transform.DOMove(arrowSprite.transform.position + Vector3.up * 0.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void DeActivation() {
        Util.ResetTween(moveTween);
        arrowSprite.transform.localPosition = arrowOriginalPosition;
        arrowSprite.enabled = false;
    }
}