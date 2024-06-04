using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager 
{
    public Object Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if (index > 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 다수의 객체의 풀을 미리 생성
    /// </summary>
    public void SetPooling(GameObject go, int count) {
         Destroy(Managers.Pool.Activation(go, count).gameObject);
    }

    public GameObject Instantiate(string path, Transform parent = null) {
        GameObject obj = Load<GameObject>($"Prefabs/{path}").GameObject();

        if (obj == null) {
            Debug.Log($"Failed Search Path : {path}");
            return null;
        }

        if (obj.GetComponent<Poolable>() != null)
            return Managers.Pool.Activation(obj).gameObject;

        GameObject go = Object.Instantiate(obj, parent);
        go.name = obj.name;
        return go;
    }

    public GameObject Instantiate(GameObject obj, int count = 5, Transform parent = null) {
        if (obj.GetComponent<Poolable>() != null)
            return Managers.Pool.Activation(obj, count).gameObject;

        GameObject go = Object.Instantiate(obj, parent);
        go.name = obj.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            Debug.Log($"Failed Search GameObject : {go.name}");
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
