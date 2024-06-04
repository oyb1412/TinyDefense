using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    //풀링 객체 종류만큼 생성되는 클래스
    class Pool
    {
        //풀링 객체
        public GameObject Original { get; private set; }
        //풀링 객체들을 모아둘 부모 객체
        public Transform Root { get; private set; }
        //풀링 객체들을 보관해둘 스택
        //스택이던 큐던 크게 상관없다.
        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        //어떤 객체가 처음으로 생성되면, 그 객체의 풀 클래스를 생성
        public void Init(GameObject original, int count = 5)
        {
            //객체를 저장
            Original = original;
            //객체들을 모아둘 부모 객체를 생성 & 이름 지정
            Root = new GameObject().transform;
            Root.name = original.name + "_root";

            //자신이 지정한 카운트 수 만큼 객체를 생성
            //객체 생성 후, 객체들을 스택에 저장
            for (int i = 0; i < count; i++)
            {
                Release(Create());
            }
        }

        //객체 생성
        private Poolable Create()
        {
            GameObject go = Object.Instantiate(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        //객체 부모생성 및 스택에 저장
        public void Release(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            _poolStack.Push(poolable);
        }

        //풀에 담겨있는 객체를 재사용하기위해 스택에서 추출
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
    /// 사용이 끝난 풀링 객체를 비활성화 및 다시 스택에 저장 
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
    /// 풀링 객체를 사용하기 위해 스택에서 추출
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
    /// 풀링 객체가 처음으로 생성되었을때, 그 객체의 풀 클래스를 생성
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
