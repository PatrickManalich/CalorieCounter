using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using OldMealEntries = CalorieCounterOld.MealEntries;
using NewMealEntries = CalorieCounter.MealEntries;
using NewMealSources = CalorieCounter.MealSources;
using System.IO;
using System.Linq;

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

            var newMealSourcesDictionary = JsonConverter.Import<Dictionary<MealSourceType, Dictionary<string, NewMealSources.MealSource>>>(GlobalPaths.MealSourcesFilePath);
            var newMealSourceNamesDictionary = JsonConverter.Import<Dictionary<MealSourceType, Dictionary<string, string>>>(GlobalPaths.MealSourceNamesFilePath);

            var dateTime = new DateTime(2019, 6, 2);
            var endDateTime = new DateTime(2019, 9, 27);
            while (dateTime <= endDateTime)
            {
                var oldMealEntry = JsonConverter.Import<OldMealEntries.MealEntry>(GetMealEntryPath(dateTime));
                if(oldMealEntry == default)
                {
                    Debug.Log($"Couldn't find {GetMealEntryPath(dateTime)}");
                    dateTime = dateTime.AddDays(1);
                    continue;
                }

                var oldMealProportionsDictionary = oldMealEntry.MealProportionsDictionary;
                var newMealProportionsDictionary = new Dictionary<MealSourceType, List<NewMealEntries.MealProportion>>();

                foreach (MealSourceType mealSourceType in Enum.GetValues(typeof(MealSourceType)))
                {
                    if (!oldMealProportionsDictionary.ContainsKey(mealSourceType))
                        continue;

                    var oldMealProportions = oldMealProportionsDictionary[mealSourceType];
                    var newMealProportions = new List<NewMealEntries.MealProportion>();
                    if (mealSourceType != MealSourceType.Custom)
                    {
                        var newMealSources = newMealSourcesDictionary[mealSourceType];
                        var newMealSourceNames = newMealSourceNamesDictionary[mealSourceType];

                        foreach (var oldMealProportion in oldMealProportions)
                        {
                            var oldMealSource = oldMealProportion.MealSource;
                            var id = newMealSourceNames.FirstOrDefault(x => x.Value == oldMealSource.Name).Key;
                            var newMealSource = newMealSources[id];
                            newMealSource.mealSourceType = mealSourceType;
                            var newMealProportion = new NewMealEntries.MealProportion(oldMealProportion.ServingAmount, newMealSource);
                            newMealProportions.Add(newMealProportion);
                        }
                    }
                    else
                    {
                        foreach (var oldMealProportion in oldMealProportions)
                        {
                            var oldMealSource = oldMealProportion.MealSource;
                            var newMealSource = NewMealSources.MealSource.CreateCustomMealSource(oldMealSource.Fat, oldMealSource.Carbs, oldMealSource.Protein);
                            var newMealProportion = new NewMealEntries.MealProportion(oldMealProportion.ServingAmount, newMealSource);
                            newMealProportions.Add(newMealProportion);
                        }
                    }

                    newMealProportionsDictionary.Add(mealSourceType, newMealProportions);
                }

                var newMealEntry = new NewMealEntries.MealEntry(oldMealEntry.DateTime, oldMealEntry.DayType, oldMealEntry.TotalFat, oldMealEntry.TotalCarbs, oldMealEntry.TotalProtein, oldMealEntry.TotalCalories, newMealProportionsDictionary);
                JsonConverter.Export(newMealEntry, GetMealEntryPath(dateTime));
                dateTime = dateTime.AddDays(1);
            }
        }

        private static string GetMealEntryPath(DateTime dateTime)
        {
            string mealEntryFileDate = "-" + dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.MealEntryFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDir, mealEntryFileName);
        }
    }
}