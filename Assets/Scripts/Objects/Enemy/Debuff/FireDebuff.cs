using UnityEngine;
/// <summary>
/// 파이어 지속데미지 디버프
/// </summary>
public class FireDebuff : DamageDebuff {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="damage">디버프 데미지</param>
    /// <param name="time">디버프 지속시간</param>
    public FireDebuff(float damage, float time) {
        Type = Define.DebuffType.Fire;
        DebuffValue = damage;
        DebuffTime = time;
    }
}