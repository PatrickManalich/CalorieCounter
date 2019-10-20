using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{

    [CreateAssetMenu(fileName = "DayMealPattern", menuName = "Calorie Counter/Day Meal Pattern")]
    public class DayMealPattern : ScriptableObject
    {
        [EnumFlag("Days Of The Week")]
        public DaysOfTheWeek daysOfTheWeek;
        public MealSourceType mealSourceType;
        public float servingAmount;
        public string mealSourceId;
    }
}


