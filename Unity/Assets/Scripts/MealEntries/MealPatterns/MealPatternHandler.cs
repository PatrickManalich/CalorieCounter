using CalorieCounter.MealSources;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{
    public class MealPatternHandler : MonoBehaviour
    {

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealProportionsScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        private List<DayMealPattern> _dayMealPatterns = default;
        private List<DayTypeMealPattern> _dayTypeMealPatterns = default;

        private void Start()
        {
            var dayMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayMealPatternsDirectoryName);
            _dayMealPatterns = Resources.LoadAll(dayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();

            var dayTypeMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayTypeMealPatternsDirectoryName);
            _dayTypeMealPatterns = Resources.LoadAll(dayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();

            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_CurrentDayTypeChanged;
        }

        private void OnDestroy()
        {
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_CurrentDayTypeChanged;
        }

        private void DayTypeDropdown_CurrentDayTypeChanged() {
            if (_dayTypeDropdown.CurrentDayType == DayType.None || _dayTypeDropdown.CurrentDayType == DayType.Vacation)
            {
                foreach (var mealSource in _scrollViewDictionary.Keys)
                {
                    _scrollViewDictionary[mealSource].ClearMealSuggestions();
                }
                return;
            }

            // Handle day meal patterns
            var dayOfTheWeek = (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), _date.CurrentDateTime.DayOfWeek.ToString());
            foreach (var dayMealPattern in _dayMealPatterns)
            {
                if (dayMealPattern.daysOfTheWeek.HasFlag(dayOfTheWeek))
                {
                    AddMealSuggestionToScrollView(dayMealPattern.mealSuggestion);
                }
            }

            // Handle day type meal patterns
            foreach (var dayTypeMealPattern in _dayTypeMealPatterns)
            {
                if (dayTypeMealPattern.dayType == _dayTypeDropdown.CurrentDayType)
                {
                    AddMealSuggestionToScrollView(dayTypeMealPattern.mealSuggestion);
                }
            }
        }

        private void AddMealSuggestionToScrollView(MealSuggestion mealSuggestion)
        {
            try
            {
                var mealProportion = mealSuggestion.mealProportion;
                var mealSourceType = mealProportion.mealSource.mealSourceType;
                _scrollViewDictionary[mealSourceType].AddMealSuggestion(mealSuggestion);
            }
            catch
            {
                Debug.LogWarning($"Error trying to add meal suggestion");
            }
        }
    }
}
