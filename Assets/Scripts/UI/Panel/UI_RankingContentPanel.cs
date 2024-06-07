using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 랭킹 각 콘텐츠 관리 클래스
/// </summary>
public class UI_RankingContentPanel : MonoBehaviour {
    //랭킹 표기 텍스트
    private TextMeshProUGUI rankingTMP;
    //이름 표기 텍스트
    private TextMeshProUGUI nameTMP;
    //점수 표기 텍스트
    private TextMeshProUGUI scoreTMP;
    //랭킹 표기 이미지 컴포넌트
    private Image rankingIcon;
    //3위까지의 스프라이트
    private Sprite[] topIcon;
    //그 외 스프라이트
    private Sprite otherIcon;

    private void Awake() {
        topIcon = new Sprite[3];
        if(otherIcon == null) {
            otherIcon = Resources.Load<Sprite>(Define.SPRITE_OTHER_ICON);

            for (int i = 0; i < topIcon.Length; i++) {
                topIcon[i] = Resources.Load<Sprite>(string.Format(Define.SPRITE_TOP_ICON, i));
            }
        }
       
    }

    /// <summary>
    /// 활성화 시 각 텍스트, 이미지에 데이터 할당
    /// </summary>
    /// <param name="name"></param>
    /// <param name="score"></param>
    /// <param name="ranking"></param>
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
        scoreTMP.text = string.Format(Define.RANKING_SCORE, score);

        if (ranking < 3) {
            rankingIcon.sprite = topIcon[ranking];
            rankingTMP.text = string.Empty;
        }
        else {
            rankingIcon.sprite = otherIcon;
        }
    }
}