using Newtonsoft.Json;
using System;

namespace CalorieCounter.MealEntries
{

    public class MealSuggestion : IEquatable<MealSuggestion>
    {

        public MealProportion MealProportion { get; }
        public int Priority { get; }

        public MealSuggestion() { }

        [JsonConstructor]
        public MealSuggestion(MealProportion mealProportion, int priority)
        {
            MealProportion = mealProportion;
            Priority = priority;
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
            return (MealProportion, Priority).GetHashCode();
        }

        public override string ToString()
        {
            return $"Meal Proportion: {MealProportion}, Priority: {Priority}";
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
            return (MealProportion == other.MealProportion) && (Priority == other.Priority);
        }
    }
}
