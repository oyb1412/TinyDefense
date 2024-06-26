using TMPro;
using UnityEngine;
/// <summary>
/// 게임 정보 표기 UI
/// </summary>
public class UI_GameInfomation : MonoBehaviour {
    //보유 골드 표기 tmp
    [SerializeField] private TextMeshProUGUI _currentGoldTMP;
    //현재 게임레벨 표기 tmp
    [SerializeField] private TextMeshProUGUI _gameLevelTMP;
    //처치 수 표기 tmp
    [SerializeField] private TextMeshProUGUI _killNumberTMP;
    //필드에 존재하는 적 수 표기 tmp
    [SerializeField] private TextMeshProUGUI _enemyNumberTMP;
    //다음 라운드까지 시간 표기
    [SerializeField] private TextMeshProUGUI _nextRoundtimeTMP;

    /// <summary>
    /// tmp에 기본 수치 대입
    /// 각 수치 액션에 tmp 구독
    /// </summary>
    private void Start() {
        _currentGoldTMP.text = Managers.Game.CurrentGold.ToString();
        _killNumberTMP.text = "0";
        _gameLevelTMP.text = string.Format(Managers.Data.DefineData.MENT_GAME_ROUND, 0);
        _enemyNumberTMP.text = string.Format(Managers.Data.DefineData.MENT_GAME_ENEMY_NUMBER, 0, Managers.Data.DefineData.ENEMY_MAX_COUNT);

        Managers.Game.CurrentGoldAction += ((gold) => _currentGoldTMP.text = gold.ToString());
        Managers.Game.CurrentKillNumberAction += ((kill) => _killNumberTMP.text = kill.ToString());
        Managers.Enemy.EnemyNumberAction += SetEnemyNumber;
        Managers.Game.CurrentGameLevelAction += ((level) => _gameLevelTMP.text = string.Format(Managers.Data.DefineData.MENT_GAME_ROUND, level));
    }

    private void Update() {
        _nextRoundtimeTMP.text = (Mathf.RoundToInt(Managers.Game.GameLevelTimer)).ToString() + "s";
    }

    private void SetEnemyNumber(int count) {
        _enemyNumberTMP.text = string.Format(Managers.Data.DefineData.MENT_GAME_ENEMY_NUMBER, count, Managers.Data.DefineData.ENEMY_MAX_COUNT);
    }
}