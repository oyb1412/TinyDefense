using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    //Ǯ�� ��ü ������ŭ �����Ǵ� Ŭ����
    class Pool
    {
        //Ǯ�� ��ü
        public GameObject Original { get; private set; }
        //Ǯ�� ��ü���� ��Ƶ� �θ� ��ü
        public Transform Root { get; private set; }
        //Ǯ�� ��ü���� �����ص� ����
        //�����̴� ť�� ũ�� �������.
        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        //� ��ü�� ó������ �����Ǹ�, �� ��ü�� Ǯ Ŭ������ ����
        public void Init(GameObject original, int count = 5)
        {
            //��ü�� ����
            Original = original;
            //��ü���� ��Ƶ� �θ� ��ü�� ���� & �̸� ����
            Root = new GameObject().transform;
            Root.name = original.name + "_root";

            //�ڽ��� ������ ī��Ʈ �� ��ŭ ��ü�� ����
            //��ü ���� ��, ��ü���� ���ÿ� ����
            for (int i = 0; i < count; i++)
            {
                Release(Create());
            }
        }

        //��ü ����
        private Poolable Create()
        {
            GameObject go = Object.Instantiate(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        //��ü �θ���� �� ���ÿ� ����
        public void Release(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            _poolStack.Push(poolable);
        }

        //Ǯ�� ����ִ� ��ü�� �����ϱ����� ���ÿ��� ����
        public Poolable Activation()
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();
            
            poolable.gameObject.SetActive(true);

            poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = Root;

            return poolable;
        }
    }
    
    private Transform _root;
    private Dictionary<string, Pool> _pools;

    public void Init()
    {
        _root = new GameObject("@Pool_root").transform;
        _pools = new Dictionary<string, Pool>();
    }

    /// <summary>
    /// ����� ���� Ǯ�� ��ü�� ��Ȱ��ȭ �� �ٽ� ���ÿ� ���� 
    /// </summary>
    /// <param name="poolable"></param>
    public void Release(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_pools.ContainsKey(name) == false)
        {
            Object.Destroy(poolable.gameObject);
            return;
        }
        
        _pools[name].Release(poolable);
    }

    /// <summary>
    /// Ǯ�� ��ü�� ����ϱ� ���� ���ÿ��� ����
    /// </summary>
    /// <param name="original"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public Poolable Activation(GameObject original, int count = 5)
    {
        if(_pools.ContainsKey(original.name) == false)
            CreatePool(original, count);

        return _pools[original.name].Activation();
    }

    /// <summary>
    /// Ǯ�� ��ü�� ó������ �����Ǿ�����, �� ��ü�� Ǯ Ŭ������ ����
    /// </summary>
    /// <param name="original"></param>
    /// <param name="count"></param>
    private void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original,count);
        pool.Root.parent = _root;
        
        _pools.Add(original.name, pool);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pools.ContainsKey(name) == false)
            return null;

        return _pools[name].Original;
    }
}
