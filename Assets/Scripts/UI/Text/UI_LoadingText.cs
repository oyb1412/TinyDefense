using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UI_LoadingText : MonoBehaviour {
    [SerializeField] private string mainText;
    [SerializeField] private string clearText;
    private Text text;
    private TextMeshProUGUI loadingText;

    private void Awake() {
        text = GetComponent<Text>();
        loadingText = GetComponentInChildren<TextMeshProUGUI>();
        text.DOText(mainText, 1.5f).SetLoops(-1, LoopType.Restart);
    }

    public void SetLoadingText(float value) {
        int integerValue = Mathf.RoundToInt(value * 100);
        loadingText.text = string.Format($"{integerValue.ToString()}%");
    }


    public void CompleteLoading() {
        DOTween.KillAll();
        text.text = clearText;
    }

}