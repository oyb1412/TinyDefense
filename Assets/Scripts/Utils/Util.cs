using DG.Tweening;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine;

public static class Util
{
    public static GameObject FindChild(GameObject go, string name, bool recursion) 
    {
        if (go == null || string.IsNullOrEmpty(name))
            return null;

        if (recursion) {
            foreach (Transform child in go.transform) {
                if (child.name == name)
                    return child.gameObject;

                GameObject found = FindChild(child.gameObject, name, recursion);
                if (found != null) return found;
            }
        } else {
            foreach (Transform child in go.transform) {
                if (child.name == name)
                    return child.gameObject;
            }
        }

        return null;
    }



    public static bool IsEnemyNull(EnemyBase enemy) {
        return enemy == null || !enemy.EnemyStatus.IsLive || !enemy.gameObject.activeInHierarchy;
    }


    /// <summary>
    /// 이동 방향을 기준으로 스프라이트 뒤집기
    /// </summary>
    /// <param name="right">오른쪽을 보고있는가?</param>
    public static void ChangeFlip(Transform trans, Define.Direction dir) {
        if (dir == Define.Direction.Right)
            trans.localScale = new Vector2(-Define.ENEMY_DEFAULT_SCALE, Define.ENEMY_DEFAULT_SCALE);
        else if(dir == Define.Direction.Left)
            trans.localScale = new Vector2(Define.ENEMY_DEFAULT_SCALE, Define.ENEMY_DEFAULT_SCALE);
    }

    public static void ResetTween(Tween tween) {
        if (tween == null)
            return;

        tween.Kill();
        tween = null;
    }

    public static ISelect MouseRaycast() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, LayerMask.GetMask(Define.TAG_GROUND));
        if(hit.collider != null && hit.collider.GetComponent<ISelect>() != null) {
            return hit.collider.GetComponent<ISelect>();
        }
       
        return null;
    }

    public static T GetorAddComponent<T>(GameObject go) where T : Component
    {
        var component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}
