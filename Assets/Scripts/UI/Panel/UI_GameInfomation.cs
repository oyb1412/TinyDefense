using TMPro;
using UnityEngine;
/// <summary>
/// ���� ���� ǥ�� UI
/// </summary>
public class UI_GameInfomation : MonoBehaviour {
    //���� ��� ǥ�� tmp
    [SerializeField] private TextMeshProUGUI _currentGoldTMP;
    //���� ���ӷ��� ǥ�� tmp
    [SerializeField] private TextMeshProUGUI _gameLevelTMP;
    //óġ �� ǥ�� tmp
    [SerializeField] private TextMeshProUGUI _killNumberTMP;
    //�ʵ忡 �����ϴ� �� �� ǥ�� tmp
    [SerializeField] private TextMeshProUGUI _enemyNumberTMP;
    //���� ������� �ð� ǥ��
    [SerializeField] private TextMeshProUGUI _nextRoundtimeTMP;

    /// <summary>
    /// tmp�� �⺻ ��ġ ����
    /// �� ��ġ �׼ǿ� tmp ����
    /// </summary>
    private void Start() {
        _currentGoldTMP.text = Managers.Game.CurrentGold.ToString();
        _killNumberTMP.text = "0";
        _gameLevelTMP.text = string.Format(Define.MENT_GAME_ROUND, 0);
        _enemyNumberTMP.text = string.Format(Define.MENT_GAME_ENEMY_NUMBER, 0, Define.ENEMY_MAX_COUNT);

        Managers.Game.CurrentGoldAction += ((gold) => _currentGoldTMP.text = gold.ToString());
        Managers.Game.CurrentKillNumberAction += ((kill) => _killNumberTMP.text = kill.ToString());
        Managers.Enemy.EnemyNumberAction += SetEnemyNumber;
        Managers.Game.CurrentGameLevelAction += ((level) => _gameLevelTMP.text = string.Format(Define.MENT_GAME_ROUND, level));
    }

    private void Update() {
        _nextRoundtimeTMP.text = (Mathf.RoundToInt(Managers.Game.GameLevelTimer)).ToString() + "s";
    }

    private void SetEnemyNumber(int count) {
        _enemyNumberTMP.text = string.Format(Define.MENT_GAME_ENEMY_NUMBER, count, Define.ENEMY_MAX_COUNT);
    }
}