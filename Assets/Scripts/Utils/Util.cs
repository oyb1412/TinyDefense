using DG.Tweening;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// 애너미 널 상태 체크
    /// </summary>
    /// <param name="enemy">체크할 애너미</param>
    /// <returns></returns>
    public static bool IsEnemyNull(EnemyBase enemy) {
        return enemy == null || !enemy.EnemyStatus.IsLive || !enemy.gameObject.activeInHierarchy || enemy.EnemyStatus.CurrentHp <= 0;
    }

    /// <summary>
    /// 이동 방향을 기준으로 스프라이트 뒤집기
    /// </summary>
    /// <param name="right">오른쪽을 보고있는가?</param>
    public static void ChangeFlip(Transform trans, Define.Direction dir) {
        if (dir == Define.Direction.Right)
            trans.localScale = new Vector2(-Managers.Data.DefineData.ENEMY_DEFAULT_SCALE, Managers.Data.DefineData.ENEMY_DEFAULT_SCALE);
        else if(dir == Define.Direction.Left)
            trans.localScale = new Vector2(Managers.Data.DefineData.ENEMY_DEFAULT_SCALE, Managers.Data.DefineData.ENEMY_DEFAULT_SCALE);
    }

    /// <summary>
    /// 트위닝 리셋
    /// </summary>
    /// <param name="tween">리셋할 트위닝</param>
    public static void ResetTween(Tween tween) {
        if (tween == null)
            return;

        tween.Kill();
        tween = null;
    }
}
