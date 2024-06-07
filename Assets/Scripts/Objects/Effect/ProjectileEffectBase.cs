using UnityEngine;

/// <summary>
/// ��� �߻�ü �浹 ����Ʈ ����
/// </summary>
public class ProjectileEffectBase : MonoBehaviour {
    //���� ������
    protected TowerBase.AttackData attackData;
    //����Ʈ ��������Ʈ ���ͷ�
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        if(spriteRenderer == null) 
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ����Ʈ ����(�ִϸ��̼� �̺�Ʈ �ݹ����� ȣ��)
    /// </summary>
    public virtual void DestroyEvent() {
        Managers.Resources.Release(gameObject);
    }

    /// <summary>
    /// ���� �� �ʱ�ȭ
    /// ���� ������ ��������
    /// ����Ʈ �� ����
    /// </summary>
    /// <param name="towerBase">�߻��� Ÿ��</param>
    /// <param name="attackData">���� ������</param>
    /// <param name="pos">����Ʈ ���� ��ġ</param>
    public virtual void Init(TowerBase towerBase, TowerBase.AttackData attackData, Vector3 pos) {
        this.attackData = attackData;
        transform.position = pos;

        if(this.attackData.IsCritical)
            spriteRenderer.color = Color.red;
        else if(this.attackData.IsMiss) 
            spriteRenderer.color = Color.black;
        else
            spriteRenderer.color = Color.white;
    }
}