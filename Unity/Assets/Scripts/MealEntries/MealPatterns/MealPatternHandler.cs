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
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        private const int PrioritizedMealSuggestionLimit = 4;

        private Dictionary<MealSourceType, List<MealSuggestion>> _mealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private Dictionary<MealSourceType, List<MealSuggestion>> _removedMealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private List<DayMealPattern> _dayMealPatterns;
        private List<DayTypeMealPattern> _dayTypeMealPatterns;
        private List<GroupMealPattern> _groupMealPatterns;
        private int _mealSuggestionClearCount = 0;

        private void Start()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
                _removedMealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
            }

            _dayMealPatterns = Resources.LoadAll(GlobalPaths.DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
            _dayTypeMealPatterns = Resources.LoadAll(GlobalPaths.DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
            _groupMealPatterns = Resources.LoadAll(GlobalPaths.GroupMealPatternsPath, typeof(GroupMealPattern)).Cast<GroupMealPattern>().ToList();

            _date.CurrentDateTimeChanged += Refresh;
            _dayTypeDropdown.CurrentDayTypeChanged += Refresh;
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealProportionModified += ScrollView_OnMealProportionModified;
                scrollView.MealSuggestionRemoved += ScrollView_OnMealSuggestionRemoved;
            }

            Refresh();
        }

        private void OnDestroy()
        {
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealSuggestionRemoved -= ScrollView_OnMealSuggestionRemoved;
                scrollView.MealProportionModified -= ScrollView_OnMealProportionModified;
            }
            _dayTypeDropdown.CurrentDayTypeChanged -= Refresh;
            _date.CurrentDateTimeChanged -= Refresh;
        }

        private void ScrollView_OnMealProportionModified(object sender, MealProportionsScrollView.MealProportionModifiedEventArgs e)
        {
            if (e.MealProportionModifiedType == MealProportionModifiedType.Added)
            {
                Refresh();
            }
        }

        private void ScrollView_OnMealSuggestionRemoved(object sender, MealProportionsScrollView.MealSuggestionRemovedEventArgs e)
        {
            if (_mealSuggestionClearCount != 0)
            {
                _mealSuggestionClearCount--;
                return;
            }
            _removedMealSuggestionsDictionary[e.MealSourceType].Add(e.RemovedMealSuggestion);
            Refresh();
        }

        private void Refresh()
        {
            foreach (var mealSourceType in _scrollViewDictionary.Keys)
            {
                _mealSuggestionClearCount += _scrollViewDictionary[mealSourceType].MealSuggestions.Count;
                _scrollViewDictionary[mealSourceType].ClearMealSuggestions();
                _mealSuggestionsDictionary[mealSourceType].Clear();
            }

            AddDayMealPatternSuggestionsToLists();
            AddDayTypeMealPatternSuggestionsToLists();
            AddGroupMealPatternSuggestionsToLists();
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

        private void AddGroupMealPatternSuggestionsToLists()
        {
            foreach (var groupMealPattern in _groupMealPatterns)
            {
                foreach (var mealSuggestion in groupMealPattern.mealSuggestions)
                {
                    var mealSource = mealSuggestion.mealProportion.mealSource;
                    var mealProportions = _scrollViewDictionary[mealSource.mealSourceType].MealProportions;
                    if (mealProportions.Exists(m => m.mealSource == mealSource))
                    {
                        var otherMealSuggestions = groupMealPattern.mealSuggestions.Where(m => m != mealSuggestion);
                        var uniqueOtherMealSuggestions = new List<MealSuggestion>();
                        foreach(var otherMealSuggestion in otherMealSuggestions)
                        {
                            if (!mealProportions.Exists(m => m.mealSource == otherMealSuggestion.mealProportion.mealSource))
                            {
                                uniqueOtherMealSuggestions.Add(otherMealSuggestion);
                            }
                        }

                        foreach (var uniqueOtherMealSuggestion in uniqueOtherMealSuggestions)
                        {
                            AddMealSuggestionToList(uniqueOtherMealSuggestion);
                        }
                        break;
                    }
                }
            }
        }

        private void AddMealSuggestionsToScrollViews()
        {
            if (_dayTypeDropdown.IsCurrentDayTypeRestOrTraining)
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
