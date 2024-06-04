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
                Debug.Log("회원가입 취소");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }
            if (task.IsFaulted) {
                Debug.Log("회원가입 실패");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            RunOnMainThread(() => action?.Invoke(true));
            Debug.Log("회원가입 완료");
        });
    }

    public void Login(string email, string password, UnityAction<bool> action) {
        Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                RunOnMainThread(() => action?.Invoke(false));
                Debug.Log("로그인 취소");
                return;
            }
            if (task.IsFaulted) {
                Debug.Log("로그인 실패");
                RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            Debug.Log("로그인 완료");
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


