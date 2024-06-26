using DG.Tweening;
using UnityEngine;

public class SelectArrow : MonoBehaviour {
    //타워 선택 화살표 스프라이트
    private SpriteRenderer arrowSprite;
    //화살표 스프라이트 트위닝
    private Tween moveTween;
    //화살표 기본 위치
    private Vector3 arrowOriginalPosition;

    private void Awake() {
        arrowSprite = GetComponent<SpriteRenderer>();
        arrowOriginalPosition = arrowSprite.transform.localPosition;
        arrowSprite.enabled = false;
    }

    /// <summary>
    /// 화살표 활성화
    /// 트윈 초기화 및 실행
    /// </summary>
    public void Activation() {
        arrowSprite.enabled = true;
        Util.ResetTween(moveTween);

        arrowSprite.transform.localPosition = arrowOriginalPosition;
        moveTween = arrowSprite.transform.DOMove(arrowSprite.transform.position + Vector3.up * 0.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 화살표 비활성화
    /// 트윈 초기화
    /// </summary>
    public void DeActivation() {
        Util.ResetTween(moveTween);
        arrowSprite.transform.localPosition = arrowOriginalPosition;
        arrowSprite.enabled = false;
    }
}