using CalorieCounter.MealEntries;
using CalorieCounter.MealEntries.MealPatterns;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CalorieCounter.EditorExtensions
{
    [InitializeOnLoad]
	public static class MealPatternValidationChecker
	{
        
        static MealPatternValidationChecker()
		{
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
		}

        private static void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.EnteredPlayMode)
            {
                var dayMealPatterns = Resources.LoadAll(GlobalPaths.DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
                foreach (var dayMealPattern in dayMealPatterns)
                {
                    CheckIfMealPatternHasBeenValidated(dayMealPattern.name, dayMealPattern.mealSuggestion);
                }

                var dayTypeMealPatterns = Resources.LoadAll(GlobalPaths.DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
                foreach (var dayTypeMealPattern in dayTypeMealPatterns)
                {
                    CheckIfMealPatternHasBeenValidated(dayTypeMealPattern.name, dayTypeMealPattern.mealSuggestion);
                }

                var groupMealPatterns = Resources.LoadAll(GlobalPaths.GroupMealPatternsPath, typeof(GroupMealPattern)).Cast<GroupMealPattern>().ToList();
                foreach (var groupMealPattern in groupMealPatterns)
                {
                    foreach(var mealSuggestion in groupMealPattern.mealSuggestions)
                    {
                        CheckIfMealPatternHasBeenValidated(groupMealPattern.name, mealSuggestion);
                    }
                }
            }
        }

        private static void CheckIfMealPatternHasBeenValidated(string mealPatternName, MealSuggestion mealSuggestion)
        {
            if (mealSuggestion.mealProportion.calories <= 0)
            {
                Debug.LogWarning($"Meal pattern: {mealPatternName} has zero calories, make sure you've validated");
            }
        }
	}
}

