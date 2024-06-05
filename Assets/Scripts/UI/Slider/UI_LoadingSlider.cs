using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

public class UI_LoadingSlider : MonoBehaviour {
    private Slider loadingSlider;
    private UI_LoadingText loadingText;

    private void Awake() {
        loadingSlider = GetComponent<Slider>();
        loadingText = GetComponentInChildren<UI_LoadingText>();
    }
    
    private IEnumerator Co_Loading(float value, UnityAction callBack) {
        while(loadingSlider.value <= value) {
            loadingSlider.value += Time.deltaTime;
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

    public void SetLoading(float value, UnityAction callBack) {
        StopAllCoroutines();
        StartCoroutine(Co_Loading(value, callBack));
    }

}