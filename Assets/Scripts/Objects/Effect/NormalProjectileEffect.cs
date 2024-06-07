using UnityEngine;

/// <summary>
/// ���������� ���� �Ϲ����� ����ü �浹 ����Ʈ
/// </summary>
public class NormalProjectileEffect : ProjectileEffectBase {

    /// <summary>
    /// ����Ʈ ���� �� �ʱ�ȭ
    /// </summary>
    /// <param name="towerBase">�߻��� Ÿ��</param>
    /// <param name="attackData">���� ������</param>
    /// <param name="pos">����Ʈ ���� ��ġ</param>
    public override void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        SoundManager.Instance.PlaySfx(Define.SFXType.HitProjectile);
        base.Init(towerBase, attackData, pos);
    }
}