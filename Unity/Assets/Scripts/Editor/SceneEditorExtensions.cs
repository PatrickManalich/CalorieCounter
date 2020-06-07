
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class SceneEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Scene/";

        [MenuItem(MenuItemDirectory + "Dirty Save All Open Scenes %#d")]
        public static void DirtySaveAllOpenScenes()
        {
            if (Application.isPlaying)
                return;

            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetSceneAt(i));
                EditorSceneManager.SaveScene(EditorSceneManager.GetSceneAt(i));
            }
            Debug.Log($"Dirty saved {EditorSceneManager.sceneCount} scene(s)");
        }
    }
}