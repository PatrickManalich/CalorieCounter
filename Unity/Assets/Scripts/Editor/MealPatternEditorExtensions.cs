using CalorieCounter.MealEntries.MealPatterns;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public static class MealPatternEditorExtensions
	{
        private const string MenuItemDirectory = @"Calorie Counter/Meal Patterns/";

        private static readonly string DayMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayMealPatternsDirectoryName);
        private static readonly string DayTypeMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayTypeMealPatternsDirectoryName);

        [MenuItem(MenuItemDirectory + "Validate Meal Patterns %#v")]
        public static void ValidateMealPatterns()
        {
            if (Application.isPlaying)
                return;

            // Can't change intermediate local variables or else changes won't be dirtied and saved

            var dayMealPatterns = Resources.LoadAll(DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
            foreach (var dayMealPattern in dayMealPatterns)
            {
                var mealProportion = dayMealPattern.mealSuggestion.mealProportion;
                var mealSource = GetMealSource(mealProportion.mealSource.id);
                dayMealPattern.mealSuggestion.mealProportion = new MealEntries.MealProportion(mealProportion.servingAmount, mealSource);
                dayMealPattern.mealSuggestion.mealPatternType = MealPatternType.Day;
                EditorUtility.SetDirty(dayMealPattern);
            }

            var dayTypeMealPatterns = Resources.LoadAll(DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
            foreach (var dayTypeMealPattern in dayTypeMealPatterns)
            {
                var mealProportion = dayTypeMealPattern.mealSuggestion.mealProportion;
                var mealSource = GetMealSource(mealProportion.mealSource.id);
                dayTypeMealPattern.mealSuggestion.mealProportion = new MealEntries.MealProportion(mealProportion.servingAmount, mealSource);
                dayTypeMealPattern.mealSuggestion.mealPatternType = MealPatternType.DayType;
                EditorUtility.SetDirty(dayTypeMealPattern);
            }

            AssetDatabase.SaveAssets();
            Debug.Log("Validated all meal patterns");
        }

        private static MealSource GetMealSource(string id)
        {
            var mealSourcesDictionary = JsonConverter.ImportFile<Dictionary<MealSourceType, Dictionary<string, MealSource>>>(GlobalPaths.JsonMealSourcesFileName);

            MealSource mealSource = default;
            foreach (var mealSources in mealSourcesDictionary.Values)
            {
                if (mealSources.ContainsKey(id))
                {
                    mealSource = mealSources[id];
                    break;
                }
            }
            if(mealSource == default)
            {
                Debug.LogWarning($"Unable to find meal source for ID: {id}");
            }
            return mealSource;
        }
    }
}

