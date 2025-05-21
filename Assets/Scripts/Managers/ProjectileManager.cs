using System.Collections.Generic;

/// <summary>
/// 모든 타워 관리 매니저
/// </summary>
public class ProjectileManager {
    //생성된 타워 리스트
    public List<ProjectileBase> ProjectileList { get; private set; }

    public void Clear() {
        ProjectileList = new List<ProjectileBase>(300);
    }

    public void OnUpdate() {
        for (int i = ProjectileList.Count - 1; i >= 0; i--) {
            var projectile = ProjectileList[i];
            projectile.UpdateProjectile();
        }
    }

    /// <summary>
    /// 리스트에 타워 추가
    /// 타워 생성시 호출
    /// </summary>
    /// <param name="go">추가할 타워</param>
    public void AddProjectile(ProjectileBase go) {
        if (ProjectileList.Contains(go))
            return;

        ProjectileList.Add(go);
    }

    /// <summary>
    /// 리스트에서 타워 제거
    /// 타워 제거시 호출
    /// </summary>
    /// <param name="go">제거할 타워</param>
    public void RemoveProjectile(ProjectileBase go) {
        if (!ProjectileList.Contains(go))
            return;

        ProjectileList.Remove(go);
    }
}