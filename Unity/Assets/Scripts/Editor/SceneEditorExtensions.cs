using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace CalorieCounter.EditorExtensions
{
    public static class SceneEditorExtensions
    {

        private const string MenuItemDirectory = @"Calorie Counter/Scene/";

        [MenuItem(MenuItemDirectory + "Dirty Save All Open Scenes %&#d")]
        public static void DirtySaveAllOpenScenes()
        {
            if (Application.isPlaying)
                return;

            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                var scene = EditorSceneManager.GetSceneAt(i);
                if(scene.rootCount > 0)
                {
                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorSceneManager.SaveScene(scene);
                }
            }
            Debug.Log($"Dirty saved {EditorSceneManager.sceneCount} scene(s)");
        }
    }
}