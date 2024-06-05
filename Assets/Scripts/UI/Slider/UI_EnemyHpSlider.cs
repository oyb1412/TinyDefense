using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 애너미 HP 슬라이더 관리
/// </summary>
public class UI_EnemyHpSlider : MonoBehaviour {
    //부모 애너미
    private EnemyBase enemyBase;
    //hp슬라이더
    private Slider hpSlider;
    //hp슬라이더 이미지
    private Image fillImage;
    //부모 애너미 최대 hp
    private float maxHp;

    /// <summary>
    /// 첫 생성 시 초기화
    /// </summary>
    private void Awake() {
        enemyBase = GetComponentInParent<EnemyBase>();
        hpSlider = GetComponent<Slider>();
        fillImage = hpSlider.fillRect.GetComponent<Image>();
    }

    /// <summary>
    /// 재생성 시 초기화
    /// </summary>
    private void OnEnable() {
        Init();
    }

    /// <summary>
    /// 슬라이더 초기화
    /// </summary>
    private void Init() {
        maxHp = enemyBase.EnemyStatus.MaxHp;

        if (maxHp == 0)
            maxHp = 1;

        hpSlider.value = maxHp / maxHp;

        if(float.IsNaN(hpSlider.value)) {
            hpSlider.value = 1f;
            maxHp = 1f;
        }
        else {
            enemyBase.EnemyStatus.SetHpAction -= HpSliderEvent;
            enemyBase.EnemyStatus.SetHpAction += HpSliderEvent;
        }
            
        fillImage.color = Color.green;
    }

    /// <summary>
    /// 체력 변화 시 마다 호출
    /// 슬라이더 fill 변경 및 색 변경
    /// </summary>
    /// <param name="hp">현재 체력</param>
    private void HpSliderEvent(float hp) {
        if (hp <= 0)
            hp = 0;

        hpSlider.value = hp / maxHp;

        if (hpSlider.value < 0.3f) {
            fillImage.color = Color.red;
            return;
        }
        if (hpSlider.value < 0.6f) {
            fillImage.color = Define.COLOR_ORANGE;
            return;
        }

        fillImage.color = Color.green;
    }
}