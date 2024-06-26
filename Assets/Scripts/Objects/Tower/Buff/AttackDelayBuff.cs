/// <summary>
/// 공격 딜레이 감소 버프
/// </summary>
public class AttackDelayBuff : Buff {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="buffValue">버프 밸류</param>
    /// <param name="buffTime">버프 지속시간</param>
    public AttackDelayBuff(float buffValue, float buffTime) {
        BuffValue = buffValue;
        BuffTime = buffTime;
        Type = Define.BuffType.AttackDelayDown;
    }
}