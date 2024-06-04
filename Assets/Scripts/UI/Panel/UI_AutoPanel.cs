using UnityEngine;
using DG.Tweening;
public class UI_AutoPanel : MonoBehaviour {
    private UI_Movement movement;

    private void Awake() {
        movement = GetComponent<UI_Movement>();
    }
    public void Activation() {
        gameObject.SetActive(true);
        movement.Activation();
    }

    public void DeActivation() {
        movement.DeActivation(() => gameObject.SetActive(false));
    }
}