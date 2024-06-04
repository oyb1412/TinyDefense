using UnityEngine;
using Firebase.Auth;
using UnityEngine.Events;
using Firebase;
using System.Collections.Generic;
using System.Threading;
public class FirebaseAuthManager {
    public FirebaseAuth Auth { get; set; }
    public FirebaseUser User { get; private set; }

    public void Registretion(string email, string password, UnityAction<bool> action) {
        Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.Log("ȸ������ ���");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }
            if (task.IsFaulted) {
                Debug.Log("ȸ������ ����");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            RunOnMainThread(() => action?.Invoke(true));
            Debug.Log("ȸ������ �Ϸ�");
        });
    }

    public void Login(string email, string password, UnityAction<bool> action) {
        Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                RunOnMainThread(() => action?.Invoke(false));
                Debug.Log("�α��� ���");
                return;
            }
            if (task.IsFaulted) {
                Debug.Log("�α��� ����");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            Debug.Log("�α��� �Ϸ�");
            RunOnMainThread(() => action?.Invoke(true));
        });
    }

    private void RunOnMainThread(System.Action action) {
        if (action == null) return;
        if (Thread.CurrentThread.ManagedThreadId == 1) {
            action();
        } else {
            UnityMainThreadDispatcher.Instance.Enqueue(action);
        }
    }
}


