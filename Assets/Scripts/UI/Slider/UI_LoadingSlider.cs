using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 로딩 슬라이더
/// </summary>
public class UI_LoadingSlider : MonoBehaviour {
    //로딩 바
    private Slider loadingSlider;
    //로딩 텍스트
    [SerializeField]private UI_LoadingText loadingText;

    private void Awake() {
        loadingSlider = GetComponent<Slider>();
    }

    /// <summary>
    /// 로딩 시작
    /// 로딩중인 경우 로딩 정지 후 재로딩
    /// </summary>
    /// <param name="value">목표 로딩 value</param>
    /// <param name="callBack">로딩 완료 후 실행할 콜백</param>
    public void SetLoading(float value, UnityAction callBack) {
        StopAllCoroutines();
        StartCoroutine(Co_Loading(value, callBack));
    }

    /// <summary>
    /// 로딩 코루틴
    /// 목표 로딩 value까지 로딩 진행
    /// 로딩 완료 시 콜백함수 실행
    /// </summary>
    /// <param name="value"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    private IEnumerator Co_Loading(float value, UnityAction callBack) {
        while(loadingSlider.value <= value) {
            loadingSlider.value += Time.deltaTime * 0.5f;
            loadingText.SetLoadingText(loadingSlider.value);
            if(loadingSlider.value >= 1f ) {
                loadingSlider.value = 1f;
                loadingText.SetLoadingText(loadingSlider.value);
                loadingText.CompleteLoading();
                callBack?.Invoke();
                break;
            }
            yield return null;
        }
    }
}