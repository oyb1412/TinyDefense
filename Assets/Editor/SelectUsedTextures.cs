using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SelectUsedTextures : EditorWindow {
    private string folderPath = "Assets/"; // �⺻ ���� ���

    [MenuItem("Tools/Select Textures In Folder")]
    static void Init() {
        // â�� ���� ���� �ν��Ͻ� ����
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

        // ���� ������ ��� ���ϰ� ���� ������ ��ȸ
        string[] allFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        foreach (string file in allFiles) {
            if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".tga") || file.EndsWith(".psd") || file.EndsWith(".gif") || file.EndsWith(".bmp")) {
                string relativePath = "Assets" + file.Substring(Application.dataPath.Length);
                Object texture = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
                if (texture is Texture) // �ؽ�ó���� Ȯ��
                {
                    textures.Add(texture);
                }
            }
        }

        // �ؽ�ó ���ϵ��� ���� ���·� �����
        Selection.objects = textures.ToArray();

        DebugWrapper.Log(textures.Count + " textures selected.");
    }
}

