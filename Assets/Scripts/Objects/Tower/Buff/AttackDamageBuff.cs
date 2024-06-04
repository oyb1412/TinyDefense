/// <summary>
/// 데미지 증가 버프
/// </summary>
public class AttackDamageBuff : Buff {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="buffValue">버프 밸류</param>
    /// <param name="buffTime">버프 지속시간</param>
    public AttackDamageBuff(float buffValue, float buffTime) {
        BuffValue = buffValue;
        BuffTime = buffTime;
        Type = Define.BuffType.AttackDamageUp;
    }
}