using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour {
    private static readonly Queue<System.Action> _executionQueue = new Queue<System.Action>();
    public static UnityMainThreadDispatcher Instance;


    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Update() {
        lock (_executionQueue) {
            while (_executionQueue.Count > 0) {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(System.Action action) {
        lock (_executionQueue) {
            _executionQueue.Enqueue(action);
        }
    }
}