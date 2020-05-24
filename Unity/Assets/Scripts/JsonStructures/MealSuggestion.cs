using Newtonsoft.Json;
using System;
using UnityEngine;

namespace CalorieCounter.MealEntries
{

    [Serializable]
    public class MealSuggestion : IEquatable<MealSuggestion>
    {

        public MealProportion mealProportion;

        [HideInInspector]
        public MealPatternType mealPatternType;

        public int priority;

        [JsonConstructor]
        public MealSuggestion(MealProportion mealProportion, MealPatternType mealPatternType, int priority)
        {
            this.mealProportion = mealProportion;
            this.mealPatternType = mealPatternType;
            this.priority = priority;
        }

        public static bool operator ==(MealSuggestion mealSuggestion1, MealSuggestion mealSuggestion2)
        {
            if (mealSuggestion1 is null)
            {
                if (mealSuggestion2 is null)
                {
                    return true;
                }
                return false;
            }
            return mealSuggestion1.Equals(mealSuggestion2);
        }

        public static bool operator !=(MealSuggestion mealSuggestion1, MealSuggestion mealSuggestion2)
        {
            return !(mealSuggestion1 == mealSuggestion2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MealSuggestion);
        }

        public override int GetHashCode()
        {
            return (mealProportion, mealPatternType).GetHashCode();
        }

        public override string ToString()
        {
            return $"Meal Proportion: {mealProportion}, Meal Pattern Type: {mealPatternType}, Priority: {priority}";
        }

        public bool Equals(MealSuggestion other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            return (mealProportion == other.mealProportion) && (mealPatternType == other.mealPatternType) && (priority == other.priority);
        }
    }
}
