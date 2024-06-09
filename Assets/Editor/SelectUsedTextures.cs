using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SelectUsedTextures : EditorWindow {
    private string folderPath = "Assets/"; // 기본 폴더 경로

    [MenuItem("Tools/Select Textures In Folder")]
    static void Init() {
        // 창을 열기 위한 인스턴스 생성
        SelectUsedTextures window = (SelectUsedTextures)EditorWindow.GetWindow(typeof(SelectUsedTextures));
        window.Show();
    }

    void OnGUI() {
        GUILayout.Label("Select Textures In Folder", EditorStyles.boldLabel);
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Select Textures")) {
            SelectTextures(folderPath);
        }
    }

    static void SelectTextures(string path) {
        if (!AssetDatabase.IsValidFolder(path)) {
            Debug.LogError("Invalid folder path: " + path);
            return;
        }

        List<Object> textures = new List<Object>();

        // 폴더 내부의 모든 파일과 하위 폴더를 순회
        string[] allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (string file in allFiles) {
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".tga") || file.EndsWith(".psd") || file.EndsWith(".gif") || file.EndsWith(".bmp")) {
                string relativePath = "Assets" + file.Substring(Application.dataPath.Length);
                Object texture = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
                if (texture is Texture) // 텍스처인지 확인
                {
                    textures.Add(texture);
                }
            }
        }

        // 텍스처 파일들을 선택 상태로 만들기
        Selection.objects = textures.ToArray();

        DebugWrapper.Log(textures.Count + " textures selected.");
    }
}

