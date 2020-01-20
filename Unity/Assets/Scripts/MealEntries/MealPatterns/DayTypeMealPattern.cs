using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{

    [CreateAssetMenu(fileName = "DayTypeMealPattern", menuName = "Calorie Counter/Day Type Meal Pattern")]
    public class DayTypeMealPattern : ScriptableObject
    {
        public DayType dayType;
        public MealSourceType mealSourceType;
        public float servingAmount;
        public string mealSourceId;
    }
}


