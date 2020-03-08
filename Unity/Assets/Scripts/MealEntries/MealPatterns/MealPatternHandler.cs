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

        private const int PrioritizedMealSuggestionLimit = 4;

        private Dictionary<MealSourceType, List<MealSuggestion>> _mealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private Dictionary<MealSourceType, List<MealSuggestion>> _removedMealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private List<DayMealPattern> _dayMealPatterns = default;
        private List<DayTypeMealPattern> _dayTypeMealPatterns = default;
        private int _mealSuggestionClearCount = 0;

        private void Start()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
                _removedMealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
            }

            var dayMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayMealPatternsDirectoryName);
            _dayMealPatterns = Resources.LoadAll(dayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();

            var dayTypeMealPatternsPath = Path.Combine(GlobalPaths.ScriptableObjectsDirectoryName, GlobalPaths.DayTypeMealPatternsDirectoryName);
            _dayTypeMealPatterns = Resources.LoadAll(dayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();

            _date.CurrentDateTimeChanged += RefreshDayMealPatterns;
            _dayTypeDropdown.CurrentDayTypeChanged += RefreshDayTypeMealPatterns;
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealProportionModified += ScrollView_OnMealProportionModified;
                scrollView.MealSuggestionRemoved += ScrollView_OnMealSuggestionRemoved;
            }

            RefreshAllMealPatterns();
        }

        private void OnDestroy()
        {
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealSuggestionRemoved -= ScrollView_OnMealSuggestionRemoved;
                scrollView.MealProportionModified -= ScrollView_OnMealProportionModified;
            }
            _dayTypeDropdown.CurrentDayTypeChanged -= RefreshDayTypeMealPatterns;
            _date.CurrentDateTimeChanged -= RefreshDayMealPatterns;
        }

        private void RefreshDayMealPatterns()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionClearCount += _scrollViewDictionary[mealSourceType].MealSuggestions.Count;
                _scrollViewDictionary[mealSourceType].ClearMealSuggestions();
                _mealSuggestionsDictionary[mealSourceType].RemoveAll(m => m.mealPatternType == MealPatternType.Day);
                _removedMealSuggestionsDictionary[mealSourceType].Clear();
            }

            AddDayMealPatternSuggestionsToLists();
            AddMealSuggestionsToScrollViews();
        }

        private void RefreshDayTypeMealPatterns()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionClearCount += _scrollViewDictionary[mealSourceType].MealSuggestions.Count;
                _scrollViewDictionary[mealSourceType].ClearMealSuggestions();
                _mealSuggestionsDictionary[mealSourceType].RemoveAll(m => m.mealPatternType == MealPatternType.DayType);
            }

            AddDayTypeMealPatternSuggestionsToLists();
            AddMealSuggestionsToScrollViews();
        }

        private void ScrollView_OnMealSuggestionRemoved(object sender, MealProportionsScrollView.MealSuggestionRemovedEventArgs e)
        {
            if (_mealSuggestionClearCount != 0)
            {
                _mealSuggestionClearCount--;
                return;
            }
            _removedMealSuggestionsDictionary[e.MealSourceType].Add(e.RemovedMealSuggestion);
            RefreshAllMealPatterns();
        }

        private void ScrollView_OnMealProportionModified(object sender, MealProportionsScrollView.MealProportionModifiedEventArgs e)
        {
            if(e.MealProportionModifiedType == MealProportionModifiedType.Added)
            {
                RefreshAllMealPatterns();
            }
        }

        private void RefreshAllMealPatterns()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionClearCount += _scrollViewDictionary[mealSourceType].MealSuggestions.Count;
                _scrollViewDictionary[mealSourceType].ClearMealSuggestions();
                _mealSuggestionsDictionary[mealSourceType].Clear();
            }

            AddDayMealPatternSuggestionsToLists();
            AddDayTypeMealPatternSuggestionsToLists();
            AddMealSuggestionsToScrollViews();
        }

        private void AddDayMealPatternSuggestionsToLists()
        {
            var dayOfTheWeek = (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), _date.CurrentDateTime.DayOfWeek.ToString());
            foreach (var dayMealPattern in _dayMealPatterns)
            {
                if (dayMealPattern.daysOfTheWeek.HasFlag(dayOfTheWeek))
                {
                    AddMealSuggestionToList(dayMealPattern.mealSuggestion);
                }
            }
        }

        private void AddDayTypeMealPatternSuggestionsToLists()
        {
            foreach (var dayTypeMealPattern in _dayTypeMealPatterns)
            {
                if (dayTypeMealPattern.dayType == _dayTypeDropdown.CurrentDayType)
                {
                    AddMealSuggestionToList(dayTypeMealPattern.mealSuggestion);
                }
            }
        }

        private void AddMealSuggestionsToScrollViews()
        {
            if (_dayTypeDropdown.CurrentDayType != DayType.None && _dayTypeDropdown.CurrentDayType != DayType.Vacation)
            {
                foreach (var mealSourceType in _scrollViewDictionary.Keys)
                {
                    // Meal suggestions with lower priority values will come before meal suggestions with higher priority values
                    var mealProportionsScrollView = _scrollViewDictionary[mealSourceType];
                    var mealSuggestions = _mealSuggestionsDictionary[mealSourceType];
                    var count = Mathf.Min(PrioritizedMealSuggestionLimit, mealSuggestions.Count);
                    var prioritizedMealSuggestions = mealSuggestions.OrderBy(m => m.priority).ToList().GetRange(0, count);

                    foreach (var prioritizedMealSuggestion in prioritizedMealSuggestions)
                    {
                        mealProportionsScrollView.AddMealSuggestion(prioritizedMealSuggestion);
                    }
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
            var isNotRemovedMealSuggestion = !_removedMealSuggestionsDictionary[mealSourceType].Contains(mealSuggestion);
            var doesExist = MealSourcesAdapter.DoesMealSourceExist(mealSource);

            if (isUniqueMealSuggestion && isNotRemovedMealSuggestion && doesExist)
            {
                mealSuggestions.Add(mealSuggestion);
            }
            else if (!doesExist)
            {
                Debug.LogWarning($"Meal source with ID: {mealSource.id} does not exist");
            }
        }
    }
}
