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
        }

        [MenuItem(MenuItemDirectory + "JSON Updater %#j")]
        public static void JsonUpdater()
        {
            if (Application.isPlaying)
                return;

            Debug.LogError("JSON Updater hasn't been implemented");
        }
    }
}