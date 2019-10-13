using CalorieCounter.MealEntries;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var riceWhiteId = "43ff7c5d-b870-40f9-90a8-c55a7afa3305";
            var newFat = 0f;
            var newCarbs = 144f;
            var newProtein = 12f;

            var mealEntries = JsonConverter.ImportMealEntries(true);
            foreach(var mealEntry in mealEntries.Values)
            {
                var mealProportionsDictionary = mealEntry.mealProportionsDictionary;
                foreach (MealSourceType mealSourceType in Enum.GetValues(typeof(MealSourceType)))
                {
                    if (!mealProportionsDictionary.ContainsKey(mealSourceType))
                        continue;

                    var oldMealProportions = mealProportionsDictionary[mealSourceType];
                    var newMealProportions = new List<MealProportion>();
                    foreach(var oldMealProportion in oldMealProportions)
                    {
                        var oldMealSource = oldMealProportion.mealSource;
                        if(oldMealSource.id == riceWhiteId)
                        {
                            var newMealSource = new MealSource(oldMealSource.servingSize, newFat, newCarbs, newProtein, oldMealSource.description, oldMealSource.mealSourceType)
                            {
                                id = oldMealSource.id
                            };
                            var newMealProportion = new MealProportion(oldMealProportion.servingAmount, newMealSource);
                            newMealProportions.Add(newMealProportion);
                        }
                        else
                        {
                            newMealProportions.Add(oldMealProportion);
                        }
                    }
                    mealEntry.mealProportionsDictionary[mealSourceType] = newMealProportions;
                    JsonConverter.ExportFile(mealEntry, JsonConverter.GetMealEntryPath(mealEntry.dateTime), true);
                }
            }
            Debug.Log("JSON Updater ran successfully");
        }
    }
}