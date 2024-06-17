using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 파이어스토어 데이터 관리 클래스
/// </summary>
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
                DebugWrapper.Log("Firebase Initialized");
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
        if (string.IsNullOrEmpty(collection) || string.IsNullOrEmpty(document) || string.IsNullOrEmpty(field)) {
            Debug.LogError("콜렉션, 도큐멘트 또는 필드 이름이 유효하지 않습니다.");
            return;
        }

        if (data == null) {
            Debug.LogError("저장할 데이터가 null입니다.");
            return;
        }

        try {
            Debug.LogError("데이터 저장 시도.");
            DocumentReference docRef = db.Collection(collection).Document(document);
            Dictionary<string, object> userData = new Dictionary<string, object>
            {
            { field, data }
        };

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted && !task.IsFaulted) {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists) {
                        docRef.UpdateAsync(userData).ContinueWithOnMainThread(updateTask => {
                            if (updateTask.IsCompleted && !updateTask.IsFaulted) {
                                DebugWrapper.Log($"{field}에 {data.ToString()} 업데이트 완료");
                            } else {
                                Debug.LogError($"{field}에 {data.ToString()} 업데이트 실패: " + updateTask.Exception?.Message);
                            }
                        });
                    } else {
                        docRef.SetAsync(userData).ContinueWithOnMainThread(setTask => {
                            if (setTask.IsCompleted && !setTask.IsFaulted) {
                                DebugWrapper.Log($"{field}에 {data.ToString()} 저장 완료");
                            } else {
                                Debug.LogError($"{field}에 {data.ToString()} 저장 실패: " + setTask.Exception?.Message);
                            }
                        });
                    }
                } else {
                    Debug.LogError($"문서 확인 실패: " + task.Exception?.Message);
                }
            });
        } catch (Exception e) {
            Debug.LogError($"Firestore 처리 중 예외 발생: {e.Message}");
        }
    }

    /// <summary>
    /// 파이어베이스에 정보 저장
    /// </summary>
    /// <param name="collection">저장할 콜렉션 명</param>
    /// <param name="document">저장할 도큐멘트 명</param>
    /// <param name="data">저장할 데이터 객체</param>
    public void SaveDataToFirestoreDocument(string collection, string document, object data) {
        if (string.IsNullOrEmpty(collection) || string.IsNullOrEmpty(document)) {
            Debug.LogError("콜렉션 또는 도큐멘트 이름이 유효하지 않습니다.");
            return;
        }

        if (data == null) {
            Debug.LogError("저장할 데이터가 null입니다.");
            return;
        }

        try {
            Debug.Log("데이터 저장 시도.");
            DocumentReference docRef = db.Collection(collection).Document(document);

            Dictionary<string, object> userData = new Dictionary<string, object>();

            // 데이터 객체의 속성을 딕셔너리로 변환
            foreach (var property in data.GetType().GetProperties()) {
                userData[property.Name] = property.GetValue(data);
            }

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task => {
                if (task.IsCompleted && !task.IsFaulted) {
                    DocumentSnapshot snapshot = task.Result;
                    if (snapshot.Exists) {
                        docRef.UpdateAsync(userData).ContinueWithOnMainThread(updateTask => {
                            if (updateTask.IsCompleted && !updateTask.IsFaulted) {
                                Debug.Log($"{document} 도큐먼트 업데이트 완료");
                            } else {
                                Debug.LogError($"{document} 도큐먼트 업데이트 실패: " + updateTask.Exception?.Message);
                            }
                        });
                    } else {
                        docRef.SetAsync(userData).ContinueWithOnMainThread(setTask => {
                            if (setTask.IsCompleted && !setTask.IsFaulted) {
                                Debug.Log($"{document} 도큐먼트 저장 완료");
                            } else {
                                Debug.LogError($"{document} 도큐먼트 저장 실패: " + setTask.Exception?.Message);
                            }
                        });
                    }
                } else {
                    Debug.LogError($"문서 확인 실패: " + task.Exception?.Message);
                }
            });
        } catch (Exception e) {
            Debug.LogError($"Firestore 처리 중 예외 발생: {e.Message}");
        }
    }


    /// <summary>
    /// 파이어베이스에서 정보 받아오기
    /// </summary>
    /// <param name="collection">정보를 받아올 콜렉션 명</param>
    /// <param name="document">정보를 받아올 도큐멘트 명</param>
    /// <param name="field">정보를 받아올 필드 명</param>
    /// <param name="callBack">정보를 저장할 콜백 함수</param>
    public async Task<object> LoadDataToFireStore(string collection, string document, string field) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            if (snapshot.TryGetValue(field, out object value)) {
                return value;
            } else {
                DebugWrapper.Log($"데이터 키{field}가 없습니다");
                return null;
            }
        } else {
            DebugWrapper.Log($"도큐멘트 {document}가 없습니다");
            return null;
        }
    }

    /// <summary>
    /// 특정 도큐멘트의 필드 모두 받아오기
    /// </summary>
    /// <param name="collection">콜렉션 명</param>
    /// <param name="document">도큐멘트 명</param>
    /// <returns></returns>
    public async Task<Dictionary<string, object>> LoadAllDataFromDocument(string collection, string document) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            Dictionary<string, object> allFields = snapshot.ToDictionary();
            return allFields;
        } else {
            DebugWrapper.Log($"도큐멘트 {document}가 없습니다");
            return null;
        }
    }
}