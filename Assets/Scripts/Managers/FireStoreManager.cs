using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using static EnemyData;

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
        if (string.IsNullOrEmpty(collection) || string.IsNullOrEmpty(document) || string.IsNullOrEmpty(field)) {
            Debug.LogError("�ݷ���, ��ť��Ʈ �Ǵ� �ʵ� �̸��� ��ȿ���� �ʽ��ϴ�.");
            return;
        }

        if (data == null) {
            Debug.LogError("������ �����Ͱ� null�Դϴ�.");
            return;
        }

        try {
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
                                Debug.Log($"{field}�� {data.ToString()} ������Ʈ �Ϸ�");
                            } else {
                                Debug.LogError($"{field}�� {data.ToString()} ������Ʈ ����: " + updateTask.Exception?.Message);
                            }
                        });
                    } else {
                        docRef.SetAsync(userData).ContinueWithOnMainThread(setTask => {
                            if (setTask.IsCompleted && !setTask.IsFaulted) {
                                Debug.Log($"{field}�� {data.ToString()} ���� �Ϸ�");
                            } else {
                                Debug.LogError($"{field}�� {data.ToString()} ���� ����: " + setTask.Exception?.Message);
                            }
                        });
                    }
                } else {
                    Debug.LogError($"���� Ȯ�� ����: " + task.Exception?.Message);
                }
            });
        } catch (Exception e) {
            Debug.LogError($"Firestore ó�� �� ���� �߻�: {e.Message}");
        }
    }


    /// <summary>
    /// ���̾�̽����� ���� �޾ƿ���
    /// </summary>
    /// <param name="collection">������ �޾ƿ� �ݷ��� ��</param>
    /// <param name="document">������ �޾ƿ� ��ť��Ʈ ��</param>
    /// <param name="field">������ �޾ƿ� �ʵ� ��</param>
    /// <param name="callBack">������ ������ �ݹ� �Լ�</param>
    public async Task<object> LoadDataToFireStore(string collection, string document, string field) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            if (snapshot.TryGetValue(field, out object value)) {
                return value;
            } else {
                Debug.Log($"������ Ű{field}�� �����ϴ�");
                return null;
            }
        } else {
            Debug.Log($"��ť��Ʈ {document}�� �����ϴ�");
            return null;
        }
    }

    public async Task<Dictionary<string, object>> LoadAllDataFromDocument(string collection, string document) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists) {
            Dictionary<string, object> allFields = snapshot.ToDictionary();
            return allFields;
        } else {
            Debug.Log($"��ť��Ʈ {document}�� �����ϴ�");
            return null;
        }
    }


    public async Task<EnemyStatusData> LoadEnemyDataAsync(string collection, string document) {
        DocumentReference docRef = db.Collection(collection).Document(document);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        if (snapshot.Exists) {
            EnemyStatusData enemyData = snapshot.ConvertTo<EnemyStatusData>();
            return enemyData;
        } else {
            Debug.LogError("Document does not exist!");
            return null;
        }
    }
}