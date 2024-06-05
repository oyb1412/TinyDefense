using System;
using UnityEngine;

/// <summary>
/// 게임 매니저
/// 시스템적 데이터 관리
/// </summary>
public class GameManager {
    //게임 진행 여부
    private bool _isPlaying;
    //게임 레벨증가 계산을 위한 타이머
    public float GameLevelTimer { get; private set; }
    //현재 보유 골드
    private int currentGold;
    //현재 게임 레벨
    private int _currentGameLevel;
    //총 킬 수
    private int currentKillNumber;
    //골드 변경 액션
    public Action<int> CurrentGoldAction;
    //게임레벨 변경 액션
    public Action<int> CurrentGameLevelAction;
    //킬 수 변경 액션
    public Action<int> CurrentKillNumberAction;
    //현재 게임 속도
    private Define.GameSpeed gameSpeed;

    public Define.GameSpeed GameSpeed {
        get { return gameSpeed; }
        set {
            if (value == Define.GameSpeed.Default)
                Time.timeScale = 1f;
            else if (value == Define.GameSpeed.Fast)
                Time.timeScale = 2f;
            else
                Time.timeScale = 0f;

            gameSpeed = value;
        }
    }

    //게임 진행 여부
    public bool IsPlaying {
        get { return _isPlaying; }
        set {
            if (_isPlaying == value)
                return;

            _isPlaying = value;

            if (_isPlaying)
                Time.timeScale = gameSpeed == Define.GameSpeed.Default ? 1f : 2f;
            else
                Time.timeScale = 0f;
        }
    }

    /// <summary>
    /// 게임 레벨에 변경이 생기면 자동으로 이벤트 호출
    /// </summary>
    public int CurrentGameLevel {
        get { return _currentGameLevel; }
        set {
            _currentGameLevel = value;
            CurrentGameLevelAction?.Invoke(_currentGameLevel);
        }
    }

    /// <summary>
    /// 킬수에 변경이 생기면 자동으로 이벤트 호출
    /// </summary>
    public int CurrentKillNumber {
        get { return currentKillNumber; }
        set {
            currentKillNumber = value;
            CurrentKillNumberAction?.Invoke(currentKillNumber);
        }
    }

    /// <summary>
    /// 보유 골드에 변경이 생기면 자동으로 이벤트 호출
    /// </summary>
    public int CurrentGold {
        get { return currentGold; }
        set {
            currentGold = value;
            CurrentGoldAction?.Invoke(currentGold);
        }
    }
    public void Clear() {
        CurrentGameLevelAction = null;
        CurrentGoldAction = null;
        CurrentKillNumberAction = null;
    }

    /// <summary>
    /// 초기화 및 게임 시작
    /// </summary>
    public void Init() {
        Managers.Enemy.EnemyNumberAction += CheckEnemyNumber;
        IsPlaying = true;
        GameLevelTimer = Define.GAMELEVEL_UP_TICK;
        CurrentGold = Define.GAME_START_GOLD;
        _currentGameLevel = 0;
        currentKillNumber = 0;
        GameSpeed = Define.GameSpeed.Default;
    }

    /// <summary>
    /// 어빌리티 획득 가능한 라운드인지 체크
    /// </summary>
    public bool CanGetAbility() {
        if(_currentGameLevel == 1) {
            IsPlaying = false;
            return true;
        }

        if (_currentGameLevel % Define.ABILITY_REWARD_ROUND == 0
                &&IsPlaying) {
            IsPlaying = false;
            return true;
        }
        return false;
    }


    /// <summary>
    /// 맵에 존재하는 적 수 체크
    /// </summary>
    /// <param name="count">적의 수</param>
    private void CheckEnemyNumber(int count) {
        if (Define.ENEMY_MAX_COUNT > count)
            return;

        if(IsPlaying) {
            GetRankingData();
            IsPlaying = false;
        }
    }

    /// <summary>
    /// 게임 오버시, 랭킹 데이터를 Load
    /// 현재 게임의 랭킹 데이터가 더 높을시 Save
    /// </summary>
    private async void GetRankingData() {
        var saveData = await Managers.FireStore.LoadDataToFireStore(Define.TAG_SCORE_DATA, Managers.Auth.User.Email,
                Define.TAG_SCORE);

        if(saveData != null) {
            int saveScore = Convert.ToInt32(saveData);

            if(saveScore < currentKillNumber) {
                Managers.FireStore.SaveDataToFirestore(Define.TAG_SCORE_DATA, Define.TAG_SCORE_DATA,
                Managers.Auth.User.Email, CurrentKillNumber);
            }
        }
        else {
            Managers.FireStore.SaveDataToFirestore(Define.TAG_SCORE_DATA, Define.TAG_SCORE_DATA,
                Managers.Auth.User.Email, CurrentKillNumber);
        }

        UI_GameOver.Instance.SetGameOverUI(true);
    }

    /// <summary>
    /// 타이머를 기준으로 게임 레벨 조정
    /// </summary>
    public void OnUpdate() {
        if (!IsPlaying)
            return;

        if (GameLevelTimer > 0) {
            GameLevelTimer -= Time.deltaTime;

            if (GameLevelTimer <= 0) {
                CurrentGameLevel++;
                GameLevelTimer = Define.GAMELEVEL_UP_TICK;
            }
        }
    }
}