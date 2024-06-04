using UnityEngine;

public class BuildEffect : MonoBehaviour {
    public void DestroyEvent() {
        Managers.Resources.Destroy(gameObject);
    }
}