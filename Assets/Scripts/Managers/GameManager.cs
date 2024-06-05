using System;
using UnityEngine;

/// <summary>
/// ���� �Ŵ���
/// �ý����� ������ ����
/// </summary>
public class GameManager {
    //���� ���� ����
    private bool _isPlaying;
    //���� �������� ����� ���� Ÿ�̸�
    public float GameLevelTimer { get; private set; }
    //���� ���� ���
    private int currentGold;
    //���� ���� ����
    private int _currentGameLevel;
    //�� ų ��
    private int currentKillNumber;
    //��� ���� �׼�
    public Action<int> CurrentGoldAction;
    //���ӷ��� ���� �׼�
    public Action<int> CurrentGameLevelAction;
    //ų �� ���� �׼�
    public Action<int> CurrentKillNumberAction;
    //���� ���� �ӵ�
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

    //���� ���� ����
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
    /// ���� ������ ������ ����� �ڵ����� �̺�Ʈ ȣ��
    /// </summary>
    public int CurrentGameLevel {
        get { return _currentGameLevel; }
        set {
            _currentGameLevel = value;
            CurrentGameLevelAction?.Invoke(_currentGameLevel);
        }
    }

    /// <summary>
    /// ų���� ������ ����� �ڵ����� �̺�Ʈ ȣ��
    /// </summary>
    public int CurrentKillNumber {
        get { return currentKillNumber; }
        set {
            currentKillNumber = value;
            CurrentKillNumberAction?.Invoke(currentKillNumber);
        }
    }

    /// <summary>
    /// ���� ��忡 ������ ����� �ڵ����� �̺�Ʈ ȣ��
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
    /// �ʱ�ȭ �� ���� ����
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
    /// �����Ƽ ȹ�� ������ �������� üũ
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
    /// �ʿ� �����ϴ� �� �� üũ
    /// </summary>
    /// <param name="count">���� ��</param>
    private void CheckEnemyNumber(int count) {
        if (Define.ENEMY_MAX_COUNT > count)
            return;

        if(IsPlaying) {
            GetRankingData();
            IsPlaying = false;
        }
    }

    /// <summary>
    /// ���� ������, ��ŷ �����͸� Load
    /// ���� ������ ��ŷ �����Ͱ� �� ������ Save
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
    /// Ÿ�̸Ӹ� �������� ���� ���� ����
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