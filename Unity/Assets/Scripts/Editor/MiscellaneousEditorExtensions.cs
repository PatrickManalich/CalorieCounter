using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class MiscellaneousEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/";

        [MenuItem(MenuItemDirectory + "Dirty Save Active Scene %#d")]
        public static void DirtySaveAllOpenScenes()
        {
            if (Application.isPlaying)
                return;

            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetSceneAt(i));
                EditorSceneManager.SaveScene(EditorSceneManager.GetSceneAt(i));
            }
            Debug.Log("Active scene dirty saved");
        }

        [MenuItem(MenuItemDirectory + "Copy Release JSON %#r")]
        public static void CopyReleaseJson()
        {
            if (Application.isPlaying)
                return;

            var editorJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), GlobalPaths.JsonDirectoryName));
            var releaseJsonDirectoryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\",
                    GlobalPaths.CalorieCounterReleaseDirectoryName, GlobalPaths.ReleaseDirectoryName, GlobalPaths.JsonDirectoryName));


            var diSource = new DirectoryInfo(releaseJsonDirectoryPath);
            var diTarget = new DirectoryInfo(editorJsonDirectoryPath);

            CopyAll(diSource, diTarget);
            Debug.Log($"Copied {releaseJsonDirectoryPath} into {editorJsonDirectoryPath}");
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