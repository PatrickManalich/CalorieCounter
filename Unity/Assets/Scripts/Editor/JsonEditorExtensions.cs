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

            foreach (FileInfo fileInfo in source.GetFiles())
            {
                fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), true);
            }

            foreach (DirectoryInfo targetSubDirectory in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDirectory = target.CreateSubdirectory(targetSubDirectory.Name);
                CopyAll(targetSubDirectory, nextTargetSubDirectory);
            }
        }
    }
}