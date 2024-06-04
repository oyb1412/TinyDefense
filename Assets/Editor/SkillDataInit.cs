using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillData))]
public class SkillDataInit : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        SkillData myScript = (SkillData)target;
        if (GUILayout.Button("Initialize")) {
            myScript.Init();
            EditorUtility.SetDirty(myScript);
        }
    }
}