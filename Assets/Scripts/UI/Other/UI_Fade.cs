using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// 모든 페이드 관리 클래스
/// </summary>
public class UI_Fade : MonoBehaviour {
    public static UI_Fade Instance;
    //페이드 이미지
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
    }

    /// <summary>
    /// 페이드 인
    /// 페이드 종료 시 씬 이동
    /// </summary>
    /// <param name="type">이동할 씬</param>
    public void ActivationFade(Define.SceneType type) {
        fadeImage.DOFade(1f, Define.FADE_TIME).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(type.ToString()));
    }

    /// <summary>
    /// 페이드 아웃
    /// </summary>
    public void DeActivationFade() {
        fadeImage.DOFade(0f, Define.FADE_TIME).SetUpdate(true);
    }

    /// <summary>
    /// 페이드 인
    /// 페이드 종료 시 콜백함수 호출
    /// </summary>
    /// <param name="fadeInCallBack"></param>
    public void ActivationAndDeActivationFade(UnityAction fadeInCallBack) {
        fadeImage.DOFade(1f, Define.FADE_TIME).SetUpdate(true).OnComplete(() =>
        {
            fadeInCallBack?.Invoke();
            fadeImage.DOFade(0f, Define.FADE_TIME);
        });
    }
}