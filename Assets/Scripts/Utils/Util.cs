using DG.Tweening;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// �ֳʹ� �� ���� üũ
    /// </summary>
    /// <param name="enemy">üũ�� �ֳʹ�</param>
    /// <returns></returns>
    public static bool IsEnemyNull(EnemyBase enemy) {
        return enemy == null || !enemy.EnemyStatus.IsLive || !enemy.gameObject.activeInHierarchy || enemy.EnemyStatus.CurrentHp <= 0;
    }

    /// <summary>
    /// �̵� ������ �������� ��������Ʈ ������
    /// </summary>
    /// <param name="right">�������� �����ִ°�?</param>
    public static void ChangeFlip(Transform trans, Define.Direction dir) {
        if (dir == Define.Direction.Right)
            trans.localScale = new Vector2(-Managers.Data.DefineData.ENEMY_DEFAULT_SCALE, Managers.Data.DefineData.ENEMY_DEFAULT_SCALE);
        else if(dir == Define.Direction.Left)
            trans.localScale = new Vector2(Managers.Data.DefineData.ENEMY_DEFAULT_SCALE, Managers.Data.DefineData.ENEMY_DEFAULT_SCALE);
    }

    /// <summary>
    /// Ʈ���� ����
    /// </summary>
    /// <param name="tween">������ Ʈ����</param>
    public static void ResetTween(Tween tween) {
        if (tween == null)
            return;

        tween.Kill();
        tween = null;
    }
}
