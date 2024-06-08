using UnityEngine;
using DG.Tweening;
using TMPro;

/// <summary>
/// �� ����� ȣ��Ǵ� ��� ������Ʈ Ŭ����
/// </summary>
public class UI_GoldObject : MonoBehaviour {
    private TextMeshProUGUI goldTMP;


    private void Awake() {
        if(goldTMP == null)
            goldTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// �ʱ�ȭ �� �̵�, ���� �ݹ� ����
    /// </summary>
    /// <param name="pos">������Ʈ ��ġ</param>
    public void Init(Vector3 pos, int gold, bool plus) {
        SoundManager.Instance.PlaySfx(Define.SFXType.GetGold);
        transform.position = pos;
        string ment = plus ? Managers.Data.DefineData.MENT_PLUS_GOLD : Managers.Data.DefineData.MENT_GOLD;
        goldTMP.text = string.Format(ment, gold.ToString());
        transform.DOMove(transform.position + Vector3.up, 1f).OnComplete(() => Managers.Resources.Release(gameObject));
    }
}