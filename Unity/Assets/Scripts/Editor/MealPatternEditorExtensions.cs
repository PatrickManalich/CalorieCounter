using CalorieCounter.MealEntries;
using CalorieCounter.MealEntries.MealPatterns;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    public static class MealPatternEditorExtensions
	{
        private const string MenuItemDirectory = @"Calorie Counter/Meal Patterns/";

        [MenuItem(MenuItemDirectory + "Validate Meal Patterns %#v")]
        public static void ValidateMealPatterns()
        {
            if (Application.isPlaying)
                return;

            var dayMealPatterns = Resources.LoadAll(GlobalPaths.DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
            foreach (var dayMealPattern in dayMealPatterns)
            {
                var mealSuggestion = dayMealPattern.mealSuggestion;
                var mealProportion = mealSuggestion.mealProportion;
                var mealSource = GetMealSource(mealProportion.mealSource.id);
                var validatedMealProportion = new MealProportion(mealProportion.servingAmount, mealSource);
                var validatedMealSuggestion = new MealSuggestion(validatedMealProportion, mealSuggestion.priority);
                dayMealPattern.mealSuggestion = validatedMealSuggestion;
                EditorUtility.SetDirty(dayMealPattern);
            }

            var dayTypeMealPatterns = Resources.LoadAll(GlobalPaths.DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
            foreach (var dayTypeMealPattern in dayTypeMealPatterns)
            {
                var mealSuggestion = dayTypeMealPattern.mealSuggestion;
                var mealProportion = mealSuggestion.mealProportion;
                var mealSource = GetMealSource(mealProportion.mealSource.id);
                var validatedMealProportion = new MealProportion(mealProportion.servingAmount, mealSource);
                var validatedMealSuggestion = new MealSuggestion(validatedMealProportion, mealSuggestion.priority);
                dayTypeMealPattern.mealSuggestion = validatedMealSuggestion;
                EditorUtility.SetDirty(dayTypeMealPattern);
            }

            var groupMealPatterns = Resources.LoadAll(GlobalPaths.GroupMealPatternsPath, typeof(GroupMealPattern)).Cast<GroupMealPattern>().ToList();
            foreach (var groupMealPattern in groupMealPatterns)
            {
                for(int i = 0; i < groupMealPattern.mealSuggestions.Count; i++)
                {
                    var mealSuggestion = groupMealPattern.mealSuggestions[i];
                    var mealProportion = mealSuggestion.mealProportion;
                    var mealSource = GetMealSource(mealProportion.mealSource.id);
                    groupMealPattern.mealSuggestions[i] = new MealSuggestion(new MealProportion(mealProportion.servingAmount, mealSource), mealSuggestion.priority);
                    EditorUtility.SetDirty(groupMealPattern);
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log("Validated all meal patterns");
        }

        private static MealSource GetMealSource(string id)
        {
            var mealSourcesDictionary = JsonConverter.ImportFile<Dictionary<MealSourceType, Dictionary<string, MealSource>>>(GlobalPaths.JsonMealSourcesFileName);

            MealSource mealSource = null;
            foreach (var mealSources in mealSourcesDictionary.Values)
            {
                if (mealSources.ContainsKey(id))
                {
                    mealSource = mealSources[id];
                    break;
                }
            }
            if(mealSource == null)
            {
                Debug.LogWarning($"Unable to find meal source for ID: {id}");
            }
            return mealSource;
        }
    }
}

