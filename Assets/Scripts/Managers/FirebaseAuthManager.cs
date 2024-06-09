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
                DebugWrapper.Log("ȸ������ ���");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }
            if (task.IsFaulted) {
                DebugWrapper.Log("ȸ������ ����");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(true));
            DebugWrapper.Log("ȸ������ �Ϸ�");
        });
    }

    public void Login(string email, string password, UnityAction<bool> action) {
        Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                DebugWrapper.Log("�α��� ���");
                return;
            }
            if (task.IsFaulted) {
                DebugWrapper.Log("�α��� ����");
                UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(false));
                return;
            }

            User = task.Result.User;
            DebugWrapper.Log("�α��� �Ϸ�");
            UnityMainThreadDispatcher.Instance.RunOnMainThread(() => action?.Invoke(true));
        });
    }

    /// <summary>
    /// �α׾ƿ�
    /// </summary>
    public void Logout() {
        try {
            Auth.SignOut();
            User = null;
            DebugWrapper.Log("�α׾ƿ� �Ϸ�");
        } catch (System.Exception ex) {
            Debug.LogError("�α׾ƿ� �� ���� �߻�: " + ex.Message);
        }
    }


    
}


