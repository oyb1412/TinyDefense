using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UI_Fade : MonoBehaviour {
    public static UI_Fade Instance;
    private Image fadeImage;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        fadeImage = GetComponentInChildren<Image>();
        fadeImage.color = Color.black;
    }

    public void ActivationFade(Define.SceneType type) {
        fadeImage.DOFade(1f, Define.FADE_TIME).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(type.ToString()));
    }

    public void DeActivationFade() {
        fadeImage.DOFade(0f, Define.FADE_TIME);
    }
}