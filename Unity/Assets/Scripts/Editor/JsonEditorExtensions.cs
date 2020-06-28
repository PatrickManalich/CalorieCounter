using CalorieCounter.Utilities;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class JsonEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Json/";

        [MenuItem(MenuItemDirectory + "Open Editor JSON")]
        public static void OpenEditorJson()
        {
            Application.OpenURL(JsonConverter.EditorJsonDirectoryPath);
            Debug.Log($"{JsonConverter.EditorJsonDirectoryPath} directory opened");
        }

        [MenuItem(MenuItemDirectory + "Open Release JSON")]
        public static void OpenReleaseJson()
        {
            Application.OpenURL(JsonConverter.ReleaseJsonDirectoryPath);
            Debug.Log($"{JsonConverter.ReleaseJsonDirectoryPath} directory opened");
        }

        [MenuItem(MenuItemDirectory + "Copy Release JSON %#j")]
        public static void CopyReleaseJson()
        {
            if (Application.isPlaying)
                return;

            var directoryInfoSource = new DirectoryInfo(JsonConverter.ReleaseJsonDirectoryPath);
            var directoryInfoTarget = new DirectoryInfo(JsonConverter.EditorJsonDirectoryPath);

            CopyAll(directoryInfoSource, directoryInfoTarget);
            Debug.Log($"Copied {JsonConverter.ReleaseJsonDirectoryPath} into {JsonConverter.EditorJsonDirectoryPath}");
        }

        [MenuItem(MenuItemDirectory + "JSON Updater")]
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