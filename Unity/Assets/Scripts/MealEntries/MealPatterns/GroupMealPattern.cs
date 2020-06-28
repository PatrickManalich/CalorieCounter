using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{

    [CreateAssetMenu(fileName = "GroupMealPattern", menuName = "Calorie Counter/Group Meal Pattern")]
    public class GroupMealPattern : ScriptableObject
    {
        public List<MealSuggestion> mealSuggestions;
        public SerializableMealSuggestion[] serializableMealSuggestions;
    }
}


