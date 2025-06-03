using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LoginScene : BaseScene {
    //로그인 판넬
    [SerializeField] private GameObject _loginPanel;
    //게임 시작 메인 판넬
    [SerializeField] private GameObject _mainPanel;

    private TextMeshProUGUI userIndexTMP;
    /// <summary>
    /// 로그인 씬 초기화
    /// </summary>
    public override void Init() {
        base.Init();
        if (!SoundManager.Instance.GetBGMPlaying())
            SoundManager.Instance.SetBgm(true, Define.BGMType.Main);

        if(Time.timeScale == 0) {
            Time.timeScale = 1f;
        }

        userIndexTMP = GameObject.Find("Canvas_BG").GetComponentInChildren<TextMeshProUGUI>();

        //2025.06.02수정 간단 로그인
        //todo 로그아웃 기능 삭제해야함

        int localIndex = PlayerPrefs.GetInt("loginIndex");

        if (localIndex != 0)
        {
            userIndexTMP.text = "환영합니다. user" + localIndex + "님";
            AutoLogin(true);
            
        } else {
            StartCoroutine(Co_FirstLogin());
        }


        ////이미 로그인한 상태면 자동 로그인
        //if (Managers.Auth.User != null) {
        //    AutoLogin(true);
        //} 
        //else {
        //    var loginData = Managers.Data.LoadLoginData();
        //    //로그인 정보가 남아있을시 자동 로그인
        //    if (loginData != null) {
        //        Managers.Auth.Login(loginData.Email, loginData.Password, AutoLogin);
        //    } else {
        //        UI_Fade.Instance.DeActivationFade();
        //    }
        //}
    }

    private IEnumerator Co_FirstLogin() {
        yield return StartCoroutine(Co_GetUserID());
        AutoLogin(true);
    }

    private IEnumerator Co_GetUserID() {
        var userIndex = Managers.FireStore.LoadDataToFireStore("UserIndex", "UserIndex", "Index");
        yield return new WaitUntil(() => userIndex.IsCompleted);
        if (userIndex.IsFaulted) {
            DebugWrapper.Log("유저 인덱스 불러오기 실패");
            yield break;
        }

        if (userIndex.Result != null) {
            userIndexTMP.text = "환영합니다. user" + userIndex.Result + "님";
            PlayerPrefs.SetInt("loginIndex", Convert.ToInt32(userIndex.Result));
            Managers.FireStore.SaveDataToFirestore("UserIndex", "UserIndex", "Index", (Convert.ToInt32(userIndex.Result) + 1).ToString());
        }

        yield return null;
    }

    /// <summary>
    /// 자동 로그인
    /// </summary>
    /// <param name="trigger"></param>
    private void AutoLogin(bool trigger) {
        _loginPanel.SetActive(false);
        _mainPanel.SetActive(true);
        UI_Fade.Instance.DeActivationFade();
    }
}