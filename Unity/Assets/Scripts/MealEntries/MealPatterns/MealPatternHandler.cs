using CalorieCounter.MealSources;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{
    public class MealPatternHandler : MonoBehaviour
    {

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, AbstractMealsScrollView> { }

        [Header("Miscellaneous")]
        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default;

        [SerializeField]
        private Date _date = default;

        [Header("Meal Patterns")]
        [SerializeField]
        private List<DayMealPattern> _dayMealPatterns = default;

        [Header("Scroll View Dictionary")]
        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        public void AddMealSuggestions()
        {
            var dayOfTheWeek = (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), _date.CurrentDateTime.DayOfWeek.ToString());
            foreach(var dayMealPattern in _dayMealPatterns)
            {
                if (dayMealPattern.daysOfTheWeek.HasFlag(dayOfTheWeek))
                {
                    var mealSource = _mealSourcesAdapter.GetMealSource(dayMealPattern.mealSourceType, dayMealPattern.mealSourceId);
                    var mealSuggestion = new MealProportion(dayMealPattern.servingAmount, mealSource);
                    _scrollViewDictionary[dayMealPattern.mealSourceType].AddMealSuggestion(mealSuggestion);
                }
            }
        }
    }
}
