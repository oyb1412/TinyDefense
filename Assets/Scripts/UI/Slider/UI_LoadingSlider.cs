using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_LoadingSlider : MonoBehaviour {
    private Slider loadingSlider;

    private void Awake() {
        loadingSlider = GetComponent<Slider>();
    }

    private void Start() {
        Managers.Data.LoadingAction -= Loading;
        Managers.Data.LoadingAction += Loading;
    }


    private void Loading(float value) {
        loadingSlider.value = value;
        if(value >= 1f) {
            Managers.Data.LoadingAction -= Loading;
            transform.parent.gameObject.SetActive(false);
        }
    }
}