using UnityEngine;
using System.Collections;

public class StartPoolManager : MonoBehaviour {
    public static StartPoolManager Instance;

    [System.Serializable]
    public class Pool {
        public GameObject obj;
        public int poolCount;
    }

    public Pool[] pool;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);
    }

    public IEnumerator StartPoolAsync() {
        for (int i = 0; i < pool.Length; i++) {
            Managers.Resources.SetPooling(pool[i].obj, pool[i].poolCount);
            yield return null; 
        }
    }
}