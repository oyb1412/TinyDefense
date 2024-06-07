using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// �ε� �����̴�
/// </summary>
public class UI_LoadingSlider : MonoBehaviour {
    //�ε� ��
    private Slider loadingSlider;
    //�ε� �ؽ�Ʈ
    [SerializeField]private UI_LoadingText loadingText;

    private void Awake() {
        loadingSlider = GetComponent<Slider>();
    }

    /// <summary>
    /// �ε� ����
    /// �ε����� ��� �ε� ���� �� ��ε�
    /// </summary>
    /// <param name="value">��ǥ �ε� value</param>
    /// <param name="callBack">�ε� �Ϸ� �� ������ �ݹ�</param>
    public void SetLoading(float value, UnityAction callBack) {
        StopAllCoroutines();
        StartCoroutine(Co_Loading(value, callBack));
    }

    /// <summary>
    /// �ε� �ڷ�ƾ
    /// ��ǥ �ε� value���� �ε� ����
    /// �ε� �Ϸ� �� �ݹ��Լ� ����
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