using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene CurrentScene => GameObject.FindFirstObjectByType(typeof(BaseScene)).GetComponent<BaseScene>();

    public void LoadScene(Define.SceneType type)
    {
        SceneManager.LoadScene(type.ToString());
    }
}
