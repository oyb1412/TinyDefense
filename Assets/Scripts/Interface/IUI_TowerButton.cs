using UnityEngine;

/// <summary>
/// 타워와 관련된 버튼UI 관리 인터페이스
/// </summary>
public interface IUI_TowerButton {
    //활성화
    void Activation(Cell cellt);
    //비활성화
    void DeActivation();
}