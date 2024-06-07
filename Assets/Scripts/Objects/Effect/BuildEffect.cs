using UnityEngine;

/// <summary>
/// 타워 생성, 판매, 업그레이드 이펙트
/// </summary>
public class BuildEffect : MonoBehaviour {
    /// <summary>
    /// 이벤트에서 콜백으로 호출
    /// </summary>
    public void DestroyEvent() {
        Managers.Resources.Release(gameObject);
    }
}