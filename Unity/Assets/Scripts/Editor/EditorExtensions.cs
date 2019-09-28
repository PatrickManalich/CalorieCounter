using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CalorieCounter
{
    public static class EditorExtensions
    {

        [MenuItem("Calorie Counter/Dirty Save Active Scene %#d")]
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

        [MenuItem("Calorie Counter/JSON Updater %#j")]
        public static void JsonUpdater()
        {
            if (Application.isPlaying)
                return;

            Debug.LogError("JSON Updater hasn't been implemented");
        }
    }
}