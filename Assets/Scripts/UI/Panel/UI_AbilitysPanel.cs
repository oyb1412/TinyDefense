using UnityEngine;

/// <summary>
/// 모든 어빌리티 표기 판넬
/// </summary>
public class UI_AbilitysPanel : MonoBehaviour {
    public static UI_AbilitysPanel Instance;
    //하위의 각 어빌리티 아이콘
    private UI_AbilityIcon[] abilityIcons;
    //보유한 어빌리티가 없을 시 표시할 판넬
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
    /// 어빌리티 아이콘 세팅
    /// 어빌리티 추가시 호출
    /// </summary>
    /// <param name="ability">추가할 어빌리티</param>
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