using CalorieCounter.Utilities;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class JsonEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Json/";

        [MenuItem(MenuItemDirectory + "Copy Release JSON %#r")]
        public static void CopyReleaseJson()
        {
            if (Application.isPlaying)
                return;

            var directoryInfoSource = new DirectoryInfo(JsonConverter.ReleaseJsonDirectoryPath);
            var directoryInfoTarget = new DirectoryInfo(JsonConverter.EditorJsonDirectoryPath);

            CopyAll(directoryInfoSource, directoryInfoTarget);
            Debug.Log($"Copied {JsonConverter.ReleaseJsonDirectoryPath} into {JsonConverter.EditorJsonDirectoryPath}");
        }

        [MenuItem(MenuItemDirectory + "JSON Updater %#j")]
        public static void JsonUpdater()
        {
            if (Application.isPlaying)
                return;

            Debug.LogWarning("JSON Updater not implemented");
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}