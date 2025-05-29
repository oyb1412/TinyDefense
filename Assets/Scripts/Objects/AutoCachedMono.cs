using UnityEngine;

public class AutoCachedMono : MonoBehaviour {
    public Transform myTransform;

    protected virtual void Awake() {
        myTransform = transform; 
    }
}