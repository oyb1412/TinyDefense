/// <summary>
/// 애너미 스테이트 머신
/// 애너미가 컴포넌트로 보유
/// </summary>
public class EnemyStateMachine {
    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="enemyBase">적용 애너미</param>
    public EnemyStateMachine(EnemyBase enemyBase) {
        this.enemyBase = enemyBase;
    }

    //애너미 상태
    private Define.EnemyState enemyState = Define.EnemyState.Run;
    private EnemyBase enemyBase;

    /// <summary>
    /// 애너미 상태 변경 및 
    /// 애니메이션 변경
    /// </summary>
    /// <param name="state">변경할 상태</param>
    public void ChangeState(Define.EnemyState state) {
        enemyState = state;
        switch(enemyState) {
            case Define.EnemyState.Run:
                enemyBase.SetAnimation(Managers.Data.DefineData.TAG_STUN, false);
                enemyBase.StartMove();
                break;
            case Define.EnemyState.Dead:
                enemyBase.SetAnimation(Managers.Data.DefineData.TAG_STUN, false);
                enemyBase.StopAllCoroutines();
                enemyBase.SetAnimation(Managers.Data.DefineData.TAG_DEAD);
                break;
            case Define.EnemyState.Stun:
                enemyBase.SetAnimation(Managers.Data.DefineData.TAG_STUN, true);
                enemyBase.StopCoroutine(enemyBase.MoveCoroutine);
                break;
        }
    }
}
