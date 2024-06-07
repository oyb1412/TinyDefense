using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private void Start() {
        var data = new TestDefine();
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter> { new Vector3Converter(), new ColorConverter() }
        });

        //var data = Managers.Data.LoadJson<TestDefine>("TestDefine");

        StartCoroutine(Co_Init(jsonData));
    }

    /// <summary>
    /// 시작 데이터 로드
    /// 각 데이터 완료 시 로딩바 증가
    /// 데이터 로드 완료시 로컬에 저장
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_Init(object data) {
        yield return StartCoroutine(Co_FirebaseInit());
        var das = Managers.Data.DecryptionLoadJson<TestDefine>("TestDefine");

        //var task = Managers.FireStore.LoadDataToFireStore("DefineData", "DefineData", "DefineData");
        //yield return new WaitUntil(() => task.IsCompleted);

        //if(task.Result != null) {
        //    Managers.Data.DecryptionSaveJson("TestDefine", task.Result.ToString());
        //}

        //yield return new WaitUntil(() => Managers.Data.CheckPathFile("TestDefine"));

    }

    /// <summary>
    /// 파이어베이스 초기화
    /// 파이어베이스에서 암호 키 로드
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_FirebaseInit() {
        Managers.FireStore.Init();
        yield return new WaitUntil(() => Managers.Auth.Auth != null);
        GetDataKey();
        yield return new WaitUntil(() => Managers.Data.Key != null);
    }


    /// <summary>
    /// 파이어베이스에서 암호 키 로드
    /// </summary>
    private async void GetDataKey() {
        Managers.Data.Key = (string)await Managers.FireStore.LoadDataToFireStore(Define.TAG_KEY_DATA, Define.TAG_KEY_DOCUMENT, Define.TAG_DATA_KEY);
    }
}