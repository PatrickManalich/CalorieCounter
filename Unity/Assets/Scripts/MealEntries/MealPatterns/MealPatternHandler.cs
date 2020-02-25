using CalorieCounter.MealSources;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{
    public class MealPatternHandler : MonoBehaviour
    {

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealProportionsScrollView> { }

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default;

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        private const string DayMealPatternsPath = "ScriptableObjects/DayMealPatterns";
        private const string DayTypeMealPatternsPath = "ScriptableObjects/DayTypeMealPatterns";

        private List<DayMealPattern> _dayMealPatterns = default;
        private List<DayTypeMealPattern> _dayTypeMealPatterns = default;

        private void Start()
        {
            _dayMealPatterns = Resources.LoadAll(DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
            _dayTypeMealPatterns = Resources.LoadAll(DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
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
                    try
                    {
                        var mealSource = _mealSourcesAdapter.GetMealSource(dayMealPattern.mealSourceId);
                        var mealSuggestion = new MealProportion(dayMealPattern.servingAmount, mealSource);
                        _scrollViewDictionary[dayMealPattern.mealSourceType].AddMealSuggestion(mealSuggestion);
                    }
                    catch
                    {
                        Debug.LogWarning($"Error trying to add meal suggestion: {dayMealPattern.name}");
                    }

                }
            }

            // Handle day type meal patterns
            foreach (var dayTypeMealPattern in _dayTypeMealPatterns)
            {
                if (dayTypeMealPattern.dayType == _dayTypeDropdown.CurrentDayType)
                {
                    try
                    {
                        var mealSource = _mealSourcesAdapter.GetMealSource(dayTypeMealPattern.mealSourceId);
                        var mealSuggestion = new MealProportion(dayTypeMealPattern.servingAmount, mealSource);
                        _scrollViewDictionary[dayTypeMealPattern.mealSourceType].AddMealSuggestion(mealSuggestion);
                    }
                    catch
                    {
                        Debug.LogWarning($"Error trying to add meal suggestion: {dayTypeMealPattern.name}");
                    }

                }
            }
        }
    }
}
