using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ֳʹ� HP �����̴� ����
/// </summary>
public class UI_EnemyHpSlider : MonoBehaviour {
    //�θ� �ֳʹ�
    private EnemyBase enemyBase;
    //hp�����̴�
    private Slider hpSlider;
    //hp�����̴� �̹���
    private Image fillImage;
    //�θ� �ֳʹ� �ִ� hp
    private float maxHp;

    /// <summary>
    /// ù ���� �� �ʱ�ȭ
    /// </summary>
    private void Awake() {
        enemyBase = GetComponentInParent<EnemyBase>();
        hpSlider = GetComponent<Slider>();
        fillImage = hpSlider.fillRect.GetComponent<Image>();
    }

    /// <summary>
    /// ����� �� �ʱ�ȭ
    /// </summary>
    private void OnEnable() {
        Init();
    }

    /// <summary>
    /// �����̴� �ʱ�ȭ
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
    /// ü�� ��ȭ �� ���� ȣ��
    /// �����̴� fill ���� �� �� ����
    /// </summary>
    /// <param name="hp">���� ü��</param>
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