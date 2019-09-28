using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;
using CalorieCounter.TargetEntries;
using CalorieCounter.ScaleEntries;

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

            var targetEntries = JsonConverter.Import<SortedList<DateTime, TargetEntry>>(GlobalPaths.TargetEntriesFilePath);
            var scaleEntries = JsonConverter.Import<SortedList<DateTime, ScaleEntry>>(GlobalPaths.ScaleEntriesFilePath);
            JsonConverter.Export(targetEntries, GlobalPaths.TargetEntriesFilePath);
            JsonConverter.Export(scaleEntries, GlobalPaths.ScaleEntriesFilePath);
        }
    }
}