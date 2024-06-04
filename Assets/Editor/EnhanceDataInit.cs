using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnhanceData))]
public class EnhanceDataInit : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EnhanceData myScript = (EnhanceData)target;
        if (GUILayout.Button("Initialize")) {
            myScript.Init();
            EditorUtility.SetDirty(myScript);
        }
    }
}