using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class MainScene : BaseScene {
    public override void Clear() {
    }

    public override void Init() {
        base.Init();
        UI_Fade.Instance.DeActivationFade();
        StartCoroutine(Co_FirebaseInit());


    }

    private IEnumerator Co_FirebaseInit() {
        Managers.FireStore.Init();
        yield return new WaitUntil(() => Managers.Auth.Auth != null);
        Managers.FireStore.LoadDataToFireStore(Define.TAG_KEY_DATA, Define.TAG_KEY_DOCUMENT, Define.TAG_DATA_KEY, Managers.Data.GetKey);
        //Managers.FireStore.GetKeyData(Managers.Data.GetKey);
        UI_Fade.Instance.DeActivationFade();
        Managers.FireStore.SaveDataToFirestore("EnemyData", "EnemyMaxHp", "EnemyMaxHp", 100);
        Managers.FireStore.SaveDataToFirestore("EnemyData", "EnemyReward", "EnemyReward", 3);
    }
}