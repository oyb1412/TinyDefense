/// <summary>
/// 타워 상태머신
/// 타워가 컴포넌트로 보유
/// </summary>
public class TowerStateMachine {
    /// <summary>
    /// 초기화
    /// </summary>
    public TowerStateMachine(TowerBase towerBase) {
        this.towerBase = towerBase;
    }

    

    //타워 상태
    private Define.TowerState towerState = Define.TowerState.Idle;
    private TowerBase towerBase;

    public Define.TowerState GetState() { return towerState; }  

    /// <summary>
    /// 타워 상태 변경 및 
    /// 애니메이션 변경
    /// </summary>
    /// <param name="state">변경할 상태</param>
    public void ChangeState(Define.TowerState state) {
        towerState = state;

        switch (towerState) {
            case Define.TowerState.Idle:
                towerBase.SetAnimation(Define.TAG_MOVEMENT, false);
                break;
            case Define.TowerState.Movement:
                towerBase.SetAnimation(Define.TAG_MOVEMENT, true);
                break;
            case Define.TowerState.Attack:
                towerBase.SetAnimation(Define.TAG_ATTACK);
                break;
        }
    }
}
