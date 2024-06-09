using UnityEngine;
public class UI_ChangeTutorialButton : UI_Button {
    [SerializeField] private int direction;
    [SerializeField] private GameObject[] tutorialPanels;
    private int currentPanelNumber;
    private int maxPanelCount;

    public override void Init() {
        currentPanelNumber = 0;
        maxPanelCount = tutorialPanels.Length;
        buttonSfxType = Define.SFXType.SelectUIButton;
    }

    public override void Select() {
        for(int i = 0; i< tutorialPanels.Length; i++) {
            if (tutorialPanels[i].activeInHierarchy) 
            {
                currentPanelNumber = i;
                break;
            }
        }

        tutorialPanels[currentPanelNumber].gameObject.SetActive(false);
        DebugWrapper.Log($"{currentPanelNumber}번 판넬 해제");
        currentPanelNumber += direction;

        if(currentPanelNumber < 0) {
            currentPanelNumber = tutorialPanels.Length - 1;
        }
        else if(currentPanelNumber > maxPanelCount - 1) {
            currentPanelNumber = 0;
        }

        tutorialPanels[currentPanelNumber].gameObject.SetActive(true);
        DebugWrapper.Log($"{currentPanelNumber}번 판넬 활성화");
    }
}