using UnityEngine;
using System.Collections;

/// <summary>
/// ���� ���۽�, Ǯ�� ��ü ����
/// </summary>
public class StartPoolManager : MonoBehaviour {
    public static StartPoolManager Instance;

    /// <summary>
    /// ������ Ǯ�� ��ü Ŭ����
    /// </summary>
    [System.Serializable]
    public class Pool {
        //������ ���ӿ�����Ʈ
        public GameObject obj;
        //������ ����
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
    /// �����ͻ󿡼� ������ ��� ��ü �̸� ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartPoolAsync() {
        for (int i = 0; i < pool.Length; i++) {
            Managers.Resources.SetPooling(pool[i].obj, pool[i].poolCount);
            yield return null; 
        }
    }
}