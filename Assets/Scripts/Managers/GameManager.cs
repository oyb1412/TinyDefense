using DG.Tweening;
using System;
using UnityEngine;


public class GameManager {
    public Action<int> CurrentGameLevelAction;

    public Action<int> CurrentGoldAction;


    public Action<int> CurrentKillNumberAction;

    private bool _isPlaying;
    
    public float GameLevelTimer { get; private set; }
    
    private int currentGold;
    
    private int _currentGameLevel;
    
    private int currentKillNumber;
    

    
    private Define.GameSpeed gameSpeed;
   
    public Animator PortalAnimator { get; private set; }

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

    
    public int CurrentGameLevel {
        get { return _currentGameLevel; }
        set {
            _currentGameLevel = value;
            CurrentGameLevelAction?.Invoke(_currentGameLevel);
        }
    }

   
    public int CurrentKillNumber {
        get { return currentKillNumber; }
        set {
            currentKillNumber = value;
            CurrentKillNumberAction?.Invoke(currentKillNumber);
        }
    }

    
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

    
    public void Init() {
        Managers.Enemy.EnemyNumberAction += CheckEnemyNumber;
        IsPlaying = true;
        GameLevelTimer = Managers.Data.DefineData.FIRST_GAMELEVEL_UP_TICK;
        CurrentGold = Managers.Data.DefineData.GAME_START_GOLD;
        _currentGameLevel = 0;
        currentKillNumber = 0;
        GameSpeed = Define.GameSpeed.Default;

        if(PortalAnimator == null)
            PortalAnimator = GameObject.Find(Managers.Data.DefineData.TAG_PORTAL).GetComponent<Animator>();
    }

   
    public bool CanGetAbility() {
        if(_currentGameLevel == 1) {
            IsPlaying = false;
            return true;
        }

        if (_currentGameLevel % Managers.Data.DefineData.ABILITY_REWARD_ROUND == 0
                &&IsPlaying) {
            IsPlaying = false;
            return true;
        }
        return false;
    }


    
    private void CheckEnemyNumber(int count) {
        if (Managers.Data.DefineData.ENEMY_MAX_COUNT > count)
            return;

        if(IsPlaying) {
            SoundManager.Instance.SetBgm(false, Define.BGMType.Ingame);
            SoundManager.Instance.PlaySfx(Define.SFXType.GameOver);
            GetRankingData();
            IsPlaying = false;
        }
    }

    
    private async void GetRankingData() {
        var saveData = await Managers.FireStore.LoadDataToFireStore(Managers.Data.DefineData.TAG_SCORE_DATA, Managers.Auth.User.Email,
                Managers.Data.DefineData.TAG_SCORE);

        if(saveData != null) {
            int saveScore = Convert.ToInt32(saveData);

            if(saveScore < currentKillNumber) {
                Managers.FireStore.SaveDataToFirestore(Managers.Data.DefineData.TAG_SCORE_DATA, Managers.Data.DefineData.TAG_SCORE_DATA,
                Managers.Auth.User.Email, CurrentKillNumber);
            }
        }
        else {
            string[] name = Managers.Auth.User.Email.Split("@");
            Managers.FireStore.SaveDataToFirestore(Managers.Data.DefineData.TAG_SCORE_DATA, Managers.Data.DefineData.TAG_SCORE_DATA,
                name[0], CurrentKillNumber);
        }

        DOTween.KillAll();
        UI_GameOver.Instance.SetGameOverUI(true);
    }

    
    public void OnUpdate() {
        if (!IsPlaying)
            return;

        if (GameLevelTimer > 0) {
            GameLevelTimer -= Time.deltaTime;

            if (GameLevelTimer <= 0) {
                CurrentGameLevel++;
                PortalAnimator.enabled = true;
                GameLevelTimer = Managers.Data.DefineData.GAMELEVEL_UP_TICK;
            }
        }
    }
}