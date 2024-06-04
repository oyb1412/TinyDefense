using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerData))]
public class TowerDataInit : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        TowerData myScript = (TowerData)target;
        if (GUILayout.Button("Initialize")) {
            myScript.Init();
            EditorUtility.SetDirty(myScript);  
        }
    }
}
