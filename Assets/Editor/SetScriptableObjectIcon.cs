using UnityEditor;
using UnityEngine;

public static class SetScriptableObjectIcon
{
    [MenuItem("Assets/Set Custom Icon", true)]
    static bool Validate()
    {
        return Selection.activeObject is ScriptableObject;
    }

    [MenuItem("Assets/Set Custom Icon")]
    static void SetIcon()
    {
        var asset = Selection.activeObject;

        // Load your icon
        Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
            "Assets/Editor/Icons/MyIcon.png");

        if (icon == null)
        {
            Debug.LogError("Couldn't find icon.");
            return;
        }

        EditorGUIUtility.SetIconForObject(asset, icon);

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}