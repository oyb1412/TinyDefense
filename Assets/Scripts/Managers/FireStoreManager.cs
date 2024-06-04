using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class FireStoreManager {
    private FirebaseFirestore db;

    /// <summary>
    /// ���̾���, ���̾�̽� �, ���̾�̽� �Ƴ���ƽ�� �ʱ�ȭ
    /// </summary>
    public void Init() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted) {
                Debug.LogError("Failed to check and fix dependencies: " + task.Exception);
                return;
            }

            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase Initialized");
                db = FirebaseFirestore.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(false);
                Managers.Auth.Auth = FirebaseAuth.DefaultInstance;
            } else {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                return;
            }
        });
    }

    /// <summary>
    /// ���̾�̽��� ���� ����
    /// </summary>
    /// <param name="collection">������ �ݷ��� ��</param>
    /// <param name="document">������ ��ť��Ʈ ��</param>
    /// <param name="field">������ �ʵ� ��</param>
    /// <param name="data">������ ������</param>
    public void SaveDataToFirestore(string collection, string document, string field, object data) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { field, data }
        };

        docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
                Debug.Log($"{field}�� {data.ToString()}���� �Ϸ�");
            } else {
                Debug.LogError($"{field}�� {data.ToString()}���� ����: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// ���̾�̽����� ���� �޾ƿ���
    /// </summary>
    /// <param name="collection">������ �޾ƿ� �ݷ��� ��</param>
    /// <param name="document">������ �޾ƿ� ��ť��Ʈ ��</param>
    /// <param name="field">������ �޾ƿ� �ʵ� ��</param>
    /// <param name="callBack">������ ������ �ݹ� �Լ�</param>
    public async void LoadDataToFireStore(string collection, string document, string field,
        UnityAction<object> callBack) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            if (snapshot.TryGetValue(field, out object value)) {
                callBack?.Invoke(value);
            } else {
                Debug.Log($"������ Ű{field}�� �����ϴ�");
            }
        } else {
            Debug.Log($"��ť��Ʈ {document}�� �����ϴ�");
        }
    }

    //public void AddUserDataToFirestore(int score) {
    //    DocumentReference docRef = db.Collection("ScoreData").Document(Managers.Auth.User.Email);
    //    Dictionary<string, object> userData = new Dictionary<string, object>
    //    {
    //        { "Score", score }
    //    };

    //    docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
    //        if (task.IsCompleted) {
    //            Debug.Log("Firestore�� ������ �߰� �Ϸ�");
    //        } else {
    //            Debug.LogError("Firestore�� ������ �߰� ����: " + task.Exception);
    //        }
    //    });
    //}

    //public async void GetKeyData(UnityAction<string> callBack) {
    //    DocumentReference docRef = db.Collection("KeyData").Document("TqFvLDUVjjYWMfHDdMrX");
    //    DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

    //    if (snapshot.Exists) {
    //        Debug.Log("Document data:");
    //        if (snapshot.TryGetValue("DataKey", out string value)) {
    //            callBack?.Invoke(value);
    //            Debug.Log($"DataKey: {value}");
    //        } else {
    //            Debug.Log("Field 'DataKey' not found.");
    //        }
    //    } else {
    //        Debug.Log("No such document!");
    //    }
    //}
}