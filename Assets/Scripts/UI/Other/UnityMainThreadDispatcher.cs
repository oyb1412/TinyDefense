using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 비동기 작업 중 메인쓰레드에서 진행해야하는 작업을
/// 보조해주는 클래스
/// </summary>
public class UnityMainThreadDispatcher : MonoBehaviour {
    //모든 작업들을 저장하는 큐
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
    /// 업데이트 함수에서 모든 작업을 지속적으로 진행
    /// </summary>
    public void Update() {
        lock (_executionQueue) {
            while (_executionQueue.Count > 0) {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    /// <summary>
    /// 작업을 큐에 저장
    /// </summary>
    /// <param name="action"></param>
    public void Enqueue(System.Action action) {
        lock (_executionQueue) {
            _executionQueue.Enqueue(action);
        }
    }

    /// <summary>
    /// 메인쓰레드 작업을 저장
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