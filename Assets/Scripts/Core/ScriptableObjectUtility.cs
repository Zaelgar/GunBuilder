using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
	// https://wiki.unity3d.com/index.php/CreateScriptableObjectAsset
	// Modified to return the asset instance and place in specific path
	// Added function to save an already instantiated asset to disk.

	public static T CreateAsset<T>(string _path) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == "")
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != "")
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;

		return asset;
	}

	public static T SaveAsset<T>(T asset, string assetPathAndName) where T : ScriptableObject
    {
		if(asset == null)
        {
			Debug.LogError("No asset to save to disk!");
			return null;
        }
		if(assetPathAndName == "")
        {
			Debug.LogError("No path or name! Cannot save asset!");
			return null;
        }

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;

		return asset;
	}
}