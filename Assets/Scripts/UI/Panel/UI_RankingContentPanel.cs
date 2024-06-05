using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankingContentPanel : MonoBehaviour {
    private TextMeshProUGUI rankingTMP;
    private TextMeshProUGUI nameTMP;
    private TextMeshProUGUI scoreTMP;
    private Image rankingIcon;

    private Sprite[] topIcon;
    private Sprite otherIcon;
    private void Awake() {
        topIcon = new Sprite[3];
        otherIcon = Resources.Load<Sprite>("Sprite/UI/OtherIcon");
        for(int i = 0; i < topIcon.Length; i++) {
            topIcon[i] = Resources.Load<Sprite>($"Sprite/UI/TopIcon{i}");
        }
    }

    public void Activation(string name, int score, int ranking) {
        gameObject.SetActive(true);
        if(rankingTMP == null) {
            rankingTMP = GetComponentsInChildren<TextMeshProUGUI>()[0];
        }
        if (nameTMP == null) {
            nameTMP = GetComponentsInChildren<TextMeshProUGUI>()[1];
        }
        if (rankingIcon == null) {
            rankingIcon = GetComponentsInChildren<Image>()[1];
        }
        if (scoreTMP == null) {
            scoreTMP = GetComponentsInChildren<TextMeshProUGUI>()[2];
        }


        nameTMP.text = name;
        rankingTMP.text = ranking.ToString();
        scoreTMP.text = string.Format("Score : {0}", score);

        if (ranking < 3) {
            rankingIcon.sprite = topIcon[ranking];
            rankingTMP.text = string.Empty;
        }
        else {
            rankingIcon.sprite = otherIcon;
        }

    }

}