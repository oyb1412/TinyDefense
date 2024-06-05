using UnityEngine;

/// <summary>
/// ��� �����Ƽ ǥ�� �ǳ�
/// </summary>
public class UI_AbilitysPanel : MonoBehaviour {
    public static UI_AbilitysPanel Instance;
    //������ �� �����Ƽ ������
    private UI_AbilityIcon[] abilityIcons;
    //������ �����Ƽ�� ���� �� ǥ���� �ǳ�
    [SerializeField] private GameObject emptyPanel;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        abilityIcons = GetComponentsInChildren<UI_AbilityIcon>();
    }

    private void Start() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �����Ƽ ������ ����
    /// �����Ƽ �߰��� ȣ��
    /// </summary>
    /// <param name="ability">�߰��� �����Ƽ</param>
    public void AddAbilityIcon(IAbility ability) {
        emptyPanel.SetActive(false);
        
        foreach (var icon in abilityIcons) {
            if(icon.IconImage.sprite == null &&
                icon.IconImage.sprite != ability.AbilityValue.Sprite) {
                icon.SetAbilityIcon(ability.AbilityValue);
 
                return;
            }
        }
    }
}