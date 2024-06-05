using UnityEngine;
using System.Collections;

/// <summary>
/// 게임 시작시, 풀링 객체 생성
/// </summary>
public class StartPoolManager : MonoBehaviour {
    public static StartPoolManager Instance;

    /// <summary>
    /// 생성할 풀링 객체 클래스
    /// </summary>
    [System.Serializable]
    public class Pool {
        //생성할 게임오브젝트
        public GameObject obj;
        //생성할 갯수
        public int poolCount;
    }

    public Pool[] pool;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else
            Destroy(gameObject);
    }

    /// <summary>
    /// 에디터상에서 지정한 모든 객체 미리 생성
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartPoolAsync() {
        for (int i = 0; i < pool.Length; i++) {
            Managers.Resources.SetPooling(pool[i].obj, pool[i].poolCount);
            yield return null; 
        }
    }
}