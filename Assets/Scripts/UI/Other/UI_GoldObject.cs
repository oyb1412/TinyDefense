using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// 적 사망시 호출되는 골드 오브젝트 클래스
/// </summary>
public class UI_GoldObject : MonoBehaviour {
    private TextMeshProUGUI goldTMP;


    private void Awake() {
        if(goldTMP == null)
            goldTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// 초기화 및 이동, 삭제 콜백 예약
    /// </summary>
    /// <param name="pos">오브젝트 위치</param>
    public void Init(Vector3 pos, int gold, bool plus) {
        SoundManager.Instance.PlaySfx(Define.SFXType.GetGold);
        transform.position = pos;
        string ment = plus ? Managers.Data.DefineData.MENT_PLUS_GOLD : Managers.Data.DefineData.MENT_GOLD;
        goldTMP.text = string.Format(ment, gold.ToString());
        transform.DOMove(transform.position + Vector3.up, 1f).OnComplete(() => Managers.Resources.Release(gameObject));
    }
}