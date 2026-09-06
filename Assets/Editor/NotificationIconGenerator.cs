using System.IO;
using UnityEditor;
using UnityEngine;

public class NotificationIconGenerator : EditorWindow
{
    private Texture2D sourceTexture;

    private readonly int[] sizes =
    {

        48, // xhdpi
        
        192  // xxxhdpi
    };

    private readonly string[] folders =
    {

        "drawable-xhdpi",

        "drawable-xxxhdpi"
    };

    [MenuItem("Tools/Android/Generate Notification Icons")]
    static void Open()
    {
        GetWindow<NotificationIconGenerator>("Notification Icon Generator");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("Source Image", EditorStyles.boldLabel);

        sourceTexture = (Texture2D)EditorGUILayout.ObjectField(
            sourceTexture,
            typeof(Texture2D),
            false);

        GUILayout.Space(10);

        EditorGUILayout.HelpBox(
            "Select a white transparent PNG (preferably 512x512).\n\n" +
            "Click Generate to create:\n" +

            "drawable-xhdpi\n" +

            "drawable-xxxhdpi",
            MessageType.Info);

        GUILayout.Space(15);

        GUI.enabled = sourceTexture != null;

        if (GUILayout.Button("Generate Notification Icons", GUILayout.Height(40)))
        {
            GenerateIcons();
        }

        GUI.enabled = true;
    }

    void GenerateIcons()
    {
        string assetPath = AssetDatabase.GetAssetPath(sourceTexture);

        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        bool oldReadable = importer.isReadable;

        if (!oldReadable)
        {
            importer.isReadable = true;
            importer.SaveAndReimport();
        }

        Texture2D readable = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

        string baseFolder = "Assets/Brightmoon/MobNotifications";

        if (!Directory.Exists(baseFolder))
            Directory.CreateDirectory(baseFolder);

        for (int i = 0; i < sizes.Length; i++)
        {
            string folder = Path.Combine(baseFolder, folders[i]);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            Texture2D resized = ScaleTexture(readable, sizes[i], sizes[i]);

            File.WriteAllBytes(
                Path.Combine(folder, "ic_stat_notification.png"),
                resized.EncodeToPNG());

            DestroyImmediate(resized);
        }

        if (!oldReadable)
        {
            importer.isReadable = false;
            importer.SaveAndReimport();
        }

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog(
            "Success",
            "Notification icons generated successfully!",
            "OK");
    }

    Texture2D ScaleTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);

        Graphics.Blit(source, rt);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return tex;
    }
}