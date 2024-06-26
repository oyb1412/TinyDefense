using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager 
{
    /// <summary>
    /// 다수의 객체의 풀을 미리 생성
    /// </summary>
    public void SetPooling(GameObject go, int count) {
         Release(Managers.Pool.Activation(go, count).gameObject);
    }

    public GameObject Activation(string path, Transform parent = null) {
        GameObject obj = Resources.Load<GameObject>($"Prefabs/{path}");

        if (obj == null) {
            DebugWrapper.Log($"Failed Search Path : {path}");
            return null;
        }

        if (obj.GetComponent<Poolable>() != null)
            return Managers.Pool.Activation(obj).gameObject;

        GameObject go = Object.Instantiate(obj, parent);
        go.name = obj.name;
        return go;
    }

    public GameObject Activation(GameObject obj, int count = 5, Transform parent = null) {
        if (obj.GetComponent<Poolable>() != null)
            return Managers.Pool.Activation(obj, count).gameObject;

        GameObject go = Object.Instantiate(obj, parent);
        go.name = obj.name;
        return go;
    }

    public void Release(GameObject go)
    {
        if (go == null)
        {
            DebugWrapper.Log($"Failed Search GameObject : {go.name}");
            return;
        }

        Poolable poolable = go.GetComponent<Poolable>();

        if (poolable != null)
        {
            Managers.Pool.Release(poolable);
            return;
        }
        
        Object.Destroy(go);
    }
}
