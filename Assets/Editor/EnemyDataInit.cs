using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataInit : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EnemyData myScript = (EnemyData)target;
        if (GUILayout.Button("Initialize")) {
            myScript.Init();
            EditorUtility.SetDirty(myScript);
        }
    }
}
