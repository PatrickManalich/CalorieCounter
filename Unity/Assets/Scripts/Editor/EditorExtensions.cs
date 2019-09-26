using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Old = CalorieCounterOld.MealSources;
using New = CalorieCounter.MealSources;


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

            var oldMealSourcesDictionary = JsonConverter.Import<Dictionary<MealSourceType, SortedList<string, Old.MealSource>>>(GlobalPaths.MealSourcesFilePath);
            var newMealSourcesDictionary = new Dictionary<MealSourceType, Dictionary<string, New.MealSource>>();
            var newMealSourceNamesDictionary = new Dictionary<MealSourceType, Dictionary<string, string>>();

            foreach (MealSourceType mealSourceType in Enum.GetValues(typeof(MealSourceType)))
            {
                if (!oldMealSourcesDictionary.ContainsKey(mealSourceType))
                    continue;

                var oldMealSourcesSortedList = oldMealSourcesDictionary[mealSourceType];
                var newMealSources = new Dictionary<string, New.MealSource>();
                var newMealSourceNames = new Dictionary<string, string>();

                foreach (var oldMealSource in oldMealSourcesSortedList.Values)
                {
                    var newMealSource = new New.MealSource(oldMealSource.ServingSize, oldMealSource.Fat, oldMealSource.Carbs, oldMealSource.Protein, oldMealSource.Description, oldMealSource.MealSourceType);
                    newMealSources.Add(newMealSource.id, newMealSource);
                    newMealSourceNames.Add(newMealSource.id, oldMealSource.Name);
                }
                newMealSourcesDictionary.Add(mealSourceType, newMealSources);
                newMealSourceNamesDictionary.Add(mealSourceType, newMealSourceNames);
            }
            JsonConverter.Export(newMealSourcesDictionary, GlobalPaths.MealSourcesFilePath);
            JsonConverter.Export(newMealSourceNamesDictionary, GlobalPaths.MealSourceNamesFilePath);
        }
    }
}
