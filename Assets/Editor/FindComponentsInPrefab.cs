using UnityEngine;
using UnityEditor;

public class FindComponentsInPrefab : EditorWindow {
    private GameObject prefab;

    [MenuItem("Tools/Find Components In Prefab")]
    public static void ShowWindow() {
        GetWindow<FindComponentsInPrefab>("Find Components In Prefab");
    }

    void OnGUI() {
        GUILayout.Label("Find Components In Prefab", EditorStyles.boldLabel);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Find and Remove SpritePos Components")) {
            if (prefab != null) {
                FindAndRemoveComponents();
            } else {
                Debug.LogWarning("Please assign a prefab.");
            }
        }
    }

    void FindAndRemoveComponents() {
        string prefabPath = AssetDatabase.GetAssetPath(prefab);
        GameObject instance = (GameObject)PrefabUtility.LoadPrefabContents(prefabPath);

        if (instance != null) {
            SPUM_SpriteList[] components = instance.GetComponentsInChildren<SPUM_SpriteList>(true);
            foreach (SPUM_SpriteList component in components) {
                Debug.Log($"Removing Component: {component.GetType()} in GameObject: {component.gameObject.name}");
                DestroyImmediate(component, true);
            }

            PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
            PrefabUtility.UnloadPrefabContents(instance);
        }
    }
}
