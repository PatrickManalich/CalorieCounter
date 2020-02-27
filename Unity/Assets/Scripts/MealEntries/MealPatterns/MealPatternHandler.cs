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

        private const int MealSuggestionLimit = 4;

        private Dictionary<MealSourceType, List<MealSuggestion>> _mealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private List<DayMealPattern> _dayMealPatterns = default;
        private List<DayTypeMealPattern> _dayTypeMealPatterns = default;

        private void Start()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
            }

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

        private void DayTypeDropdown_CurrentDayTypeChanged()
        {
            foreach (var mealSource in _scrollViewDictionary.Keys)
            {
                _scrollViewDictionary[mealSource].ClearMealSuggestions();
            }

            if (_dayTypeDropdown.CurrentDayType == DayType.None || _dayTypeDropdown.CurrentDayType == DayType.Vacation)
            {
                return;
            }

            foreach (var mealSourceType in _mealSuggestionsDictionary.Keys)
            {
                _mealSuggestionsDictionary[mealSourceType].RemoveAll(m => m.mealPatternType == MealPatternType.DayType);
            }

            // Add day meal patterns to lists
            var dayOfTheWeek = (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), _date.CurrentDateTime.DayOfWeek.ToString());
            foreach (var dayMealPattern in _dayMealPatterns)
            {
                if (dayMealPattern.daysOfTheWeek.HasFlag(dayOfTheWeek))
                {
                    AddMealSuggestionToList(dayMealPattern.mealSuggestion);
                }
            }

            // Add day type meal patterns to lists
            foreach (var dayTypeMealPattern in _dayTypeMealPatterns)
            {
                if (dayTypeMealPattern.dayType == _dayTypeDropdown.CurrentDayType)
                {
                    AddMealSuggestionToList(dayTypeMealPattern.mealSuggestion);
                }
            }

            // Process all lists, up to meal suggestion limit
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                var mealProportionsScrollView = _scrollViewDictionary[mealSourceType];
                var mealSuggestions = _mealSuggestionsDictionary[mealSourceType];
                var mealSuggestionsAdded = 0;
                while (mealSuggestionsAdded < MealSuggestionLimit && mealSuggestionsAdded < mealSuggestions.Count)
                {
                    mealProportionsScrollView.AddMealSuggestion(mealSuggestions[mealSuggestionsAdded]);
                    mealSuggestionsAdded++;
                }
            }
        }

        private void AddMealSuggestionToList(MealSuggestion mealSuggestion)
        {
            var mealProportion = mealSuggestion.mealProportion;
            var mealSource = mealProportion.mealSource;
            var mealSourceType = mealSource.mealSourceType;

            var mealProportionsScrollView = _scrollViewDictionary[mealSourceType];
            var mealSuggestions = _mealSuggestionsDictionary[mealSourceType];
            var isUniqueMealSuggestion = !mealProportionsScrollView.MealProportions.Exists(m => m.mealSource == mealSource) &&
                !mealSuggestions.Exists(m => m.mealProportion.mealSource == mealSource);

            if (isUniqueMealSuggestion)
            {
                mealSuggestions.Add(mealSuggestion);
            }
        }
    }
}
