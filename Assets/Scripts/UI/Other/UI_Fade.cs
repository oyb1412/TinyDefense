using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// ��� ���̵� ���� Ŭ����
/// </summary>
public class UI_Fade : MonoBehaviour {
    public static UI_Fade Instance;
    //���̵� �̹���
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
    /// ���̵� ��
    /// ���̵� ���� �� �� �̵�
    /// </summary>
    /// <param name="type">�̵��� ��</param>
    public void ActivationFade(Define.SceneType type) {
        fadeImage.DOFade(1f, Define.FADE_TIME).SetUpdate(true).OnComplete(() => SceneManager.LoadScene(type.ToString()));
    }

    /// <summary>
    /// ���̵� �ƿ�
    /// </summary>
    public void DeActivationFade() {
        fadeImage.DOFade(0f, Define.FADE_TIME).SetUpdate(true);
    }

    /// <summary>
    /// ���̵� ��
    /// ���̵� ���� �� �ݹ��Լ� ȣ��
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