using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// �񵿱� �۾� �� ���ξ����忡�� �����ؾ��ϴ� �۾���
/// �������ִ� Ŭ����
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour {
    //��� �۾����� �����ϴ� ť
    private static readonly Queue<System.Action> _executionQueue = new Queue<System.Action>();
    public static UnityMainThreadDispatcher Instance;


    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ������Ʈ �Լ����� ��� �۾��� ���������� ����
    /// </summary>
    public void Update() {
        lock (_executionQueue) {
            while (_executionQueue.Count > 0) {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    /// <summary>
    /// �۾��� ť�� ����
    /// </summary>
    /// <param name="action"></param>
    public void Enqueue(System.Action action) {
        lock (_executionQueue) {
            _executionQueue.Enqueue(action);
        }
    }

    /// <summary>
    /// ���ξ����� �۾��� ����
    /// </summary>
    /// <param name="action"></param>
    public void RunOnMainThread(System.Action action) {
        if (action == null) return;
        if (Thread.CurrentThread.ManagedThreadId == 1) {
            action();
        } else {
            Enqueue(action);
        }
    }
}