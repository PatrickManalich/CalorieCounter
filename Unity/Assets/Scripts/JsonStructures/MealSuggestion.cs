using System;
using UnityEngine;

namespace CalorieCounter.MealEntries
{

    [Serializable]
    public class MealSuggestion
    {

        public MealProportion mealProportion;

        [HideInInspector]
        public MealPatternType mealPatternType;

        public int priority;

        public MealSuggestion(MealProportion mealProportion, MealPatternType mealPatternType, int priority)
        {
            this.mealProportion = mealProportion;
            this.mealPatternType = mealPatternType;
            this.priority = priority;
        }

        public static bool operator ==(MealSuggestion mealSuggestion1, MealSuggestion mealSuggestion2)
        {
            return mealSuggestion1.Equals(mealSuggestion2);
        }

        public static bool operator !=(MealSuggestion mealSuggestion1, MealSuggestion mealSuggestion2)
        {
            return !mealSuggestion1.Equals(mealSuggestion2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Meal Proportion: {mealProportion}, Meal Pattern Type: {mealPatternType}, Priority: {priority}";
        }
    }
}
