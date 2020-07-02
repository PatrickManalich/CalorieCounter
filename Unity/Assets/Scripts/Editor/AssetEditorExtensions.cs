using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public static class AssetEditorExtensions
    {
        private const string MenuItemDirectory = @"Calorie Counter/Assets/";

        [MenuItem(MenuItemDirectory + "Force Reserialize Selected Assets %&#r")]
        public static void ForceReserializeSelectedAssets()
        {
            var assetPaths = new List<string>();
            foreach (var obj in Selection.objects)
            {
                var assetPath = AssetDatabase.GetAssetPath(obj);
                assetPaths.Add(assetPath);
            }
            AssetDatabase.ForceReserializeAssets(assetPaths);
            Debug.Log($"Force reserialized {assetPaths.Count} asset(s)");
        }
    }
}