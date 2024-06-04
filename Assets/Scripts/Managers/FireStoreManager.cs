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
    /// 파이어스토어, 파이어베이스 어스, 파이어베이스 아날리틱스 초기화
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
    /// 파이어베이스에 정보 저장
    /// </summary>
    /// <param name="collection">저장할 콜렉션 명</param>
    /// <param name="document">저장할 도큐멘트 명</param>
    /// <param name="field">저장할 필드 명</param>
    /// <param name="data">저장할 데이터</param>
    public void SaveDataToFirestore(string collection, string document, string field, object data) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { field, data }
        };

        docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
            if (task.IsCompleted) {
                Debug.Log($"{field}에 {data.ToString()}저장 완료");
            } else {
                Debug.LogError($"{field}에 {data.ToString()}저장 실패: " + task.Exception);
            }
        });
    }

    /// <summary>
    /// 파이어베이스에서 정보 받아오기
    /// </summary>
    /// <param name="collection">정보를 받아올 콜렉션 명</param>
    /// <param name="document">정보를 받아올 도큐멘트 명</param>
    /// <param name="field">정보를 받아올 필드 명</param>
    /// <param name="callBack">정보를 저장할 콜백 함수</param>
    public async void LoadDataToFireStore(string collection, string document, string field,
        UnityAction<object> callBack) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            if (snapshot.TryGetValue(field, out object value)) {
                callBack?.Invoke(value);
            } else {
                Debug.Log($"데이터 키{field}가 없습니다");
            }
        } else {
            Debug.Log($"도큐멘트 {document}가 없습니다");
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
    //            Debug.Log("Firestore에 데이터 추가 완료");
    //        } else {
    //            Debug.LogError("Firestore에 데이터 추가 실패: " + task.Exception);
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