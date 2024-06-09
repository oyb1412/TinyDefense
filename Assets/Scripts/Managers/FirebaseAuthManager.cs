using UnityEngine;
using Firebase.Auth;
using UnityEngine.Events;
using System.Threading;
public class FirebaseAuthManager {
    public FirebaseAuth Auth { get; set; }
    public FirebaseUser User { get; private set; }

    public void Registretion(string email, string password, UnityAction<bool> action) {
        Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                DebugWrapper.Log("회원가입 취소");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }
            if (task.IsFaulted) {
                DebugWrapper.Log("회원가입 실패");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(true));
            DebugWrapper.Log("회원가입 완료");
        });
    }

    public void Login(string email, string password, UnityAction<bool> action) {
        Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                DebugWrapper.Log("로그인 취소");
                return;
            }
            if (task.IsFaulted) {
                DebugWrapper.Log("로그인 실패");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            DebugWrapper.Log("로그인 완료");
            UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(true));
        });
    }

    /// <summary>
    /// 로그아웃
    /// </summary>
    public void Logout() {
        try {
            Auth.SignOut();
            User = null;
            DebugWrapper.Log("로그아웃 완료");
        } catch (System.Exception ex) {
            Debug.LogError("로그아웃 중 오류 발생: " + ex.Message);
        }
    }


    
}


