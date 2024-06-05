using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get; protected set; } = Define.SceneType.None;

    public virtual void Init() { }

    protected virtual void Awake() { }

    private void Start()
    {
        Init();
    }
}
