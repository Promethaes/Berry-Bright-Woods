
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Helper
{
    //https://answers.unity.com/questions/486545/getting-all-assets-of-the-specified-type.html
    public static List<T> FindScriptableObjectByType<T>() where T : ScriptableObject
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }
        return assets;
    }
}