using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ŷ �� ������ ���� Ŭ����
/// </summary>
public class UI_RankingContentPanel : MonoBehaviour {
    //��ŷ ǥ�� �ؽ�Ʈ
    private TextMeshProUGUI rankingTMP;
    //�̸� ǥ�� �ؽ�Ʈ
    private TextMeshProUGUI nameTMP;
    //���� ǥ�� �ؽ�Ʈ
    private TextMeshProUGUI scoreTMP;
    //��ŷ ǥ�� �̹��� ������Ʈ
    private Image rankingIcon;
    //3�������� ��������Ʈ
    private Sprite[] topIcon;
    //�� �� ��������Ʈ
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
    /// Ȱ��ȭ �� �� �ؽ�Ʈ, �̹����� ������ �Ҵ�
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