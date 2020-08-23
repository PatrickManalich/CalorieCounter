using CalorieCounter.MealSources;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CalorieCounter.MealEntries.MealPatterns
{
    public class MealPatternHandler : MonoBehaviour
    {

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private MealProportionsScrollViewDictionary _mealProportionsScrollViewDictionary = default;

        private const int PrioritizedMealSuggestionLimit = 6;

        private Dictionary<MealSourceType, List<MealSuggestion>> _mealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private Dictionary<MealSourceType, List<MealSuggestion>> _removedMealSuggestionsDictionary = new Dictionary<MealSourceType, List<MealSuggestion>>();
        private List<DayMealPattern> _dayMealPatterns;
        private List<DayTypeMealPattern> _dayTypeMealPatterns;
        private List<GroupMealPattern> _groupMealPatterns;

        private void Start()
        {
            foreach (var mealSourceType in _mealProportionsScrollViewDictionary.Keys)
            {
                _mealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
                _removedMealSuggestionsDictionary.Add(mealSourceType, new List<MealSuggestion>());
            }

            _dayMealPatterns = Resources.LoadAll(GlobalPaths.DayMealPatternsPath, typeof(DayMealPattern)).Cast<DayMealPattern>().ToList();
            _dayTypeMealPatterns = Resources.LoadAll(GlobalPaths.DayTypeMealPatternsPath, typeof(DayTypeMealPattern)).Cast<DayTypeMealPattern>().ToList();
            _groupMealPatterns = Resources.LoadAll(GlobalPaths.GroupMealPatternsPath, typeof(GroupMealPattern)).Cast<GroupMealPattern>().ToList();

            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_OnCurrentDayTypeChanged;
            SubscribeToMealProportionScrollViewEvents();

            Refresh();
        }

        private void OnDestroy()
        {
            UnsubscribeToMealProportionScrollViewEvents();
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_OnCurrentDayTypeChanged;
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
        }

        private void Date_OnCurrentDateTimeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void DayTypeDropdown_OnCurrentDayTypeChanged(object sender, EventArgs e)
        {
            Refresh();
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
            _removedMealSuggestionsDictionary[e.MealSourceType].Add(e.RemovedMealSuggestion);
            Refresh();
        }

        private void Refresh()
        {
            foreach (var mealSourceType in _mealProportionsScrollViewDictionary.Keys)
            {
                UnsubscribeToMealProportionScrollViewEvents();
                _mealProportionsScrollViewDictionary[mealSourceType].ClearMealSuggestions();
                SubscribeToMealProportionScrollViewEvents();
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
                    AddMealSuggestionToList(dayMealPattern.serializableMealSuggestion.GetMealSuggestion());
                }
            }
        }

        private void AddDayTypeMealPatternSuggestionsToLists()
        {
            foreach (var dayTypeMealPattern in _dayTypeMealPatterns)
            {
                if (dayTypeMealPattern.dayType == _dayTypeDropdown.CurrentDayType)
                {
                    AddMealSuggestionToList(dayTypeMealPattern.serializableMealSuggestion.GetMealSuggestion());
                }
            }
        }

        private void AddGroupMealPatternSuggestionsToLists()
        {
            foreach (var groupMealPattern in _groupMealPatterns)
            {
                var mealSuggestions = groupMealPattern.serializableMealSuggestions.Select(s => s.GetMealSuggestion()).ToList();
                foreach (var mealSuggestion in mealSuggestions)
                {
                    var mealSource = mealSuggestion.MealProportion.MealSource;
                    var mealProportions = _mealProportionsScrollViewDictionary[mealSource.MealSourceType].MealProportions;
                    if (mealProportions.Exists(m => m.MealSource == mealSource))
                    {
                        var otherMealSuggestions = mealSuggestions.Where(m => m != mealSuggestion);
                        var uniqueOtherMealSuggestions = new List<MealSuggestion>();
                        foreach(var otherMealSuggestion in otherMealSuggestions)
                        {
                            if (!mealProportions.Exists(m => m.MealSource == otherMealSuggestion.MealProportion.MealSource))
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
                foreach (var mealSourceType in _mealProportionsScrollViewDictionary.Keys)
                {
                    // Meal suggestions with lower priority values will come before meal suggestions with higher priority values
                    var mealProportionsScrollView = _mealProportionsScrollViewDictionary[mealSourceType];
                    var mealSuggestions = _mealSuggestionsDictionary[mealSourceType];
                    var count = Mathf.Min(PrioritizedMealSuggestionLimit, mealSuggestions.Count);
                    var prioritizedMealSuggestions = mealSuggestions.OrderBy(m => m.Priority).ToList().GetRange(0, count);

                    foreach (var prioritizedMealSuggestion in prioritizedMealSuggestions)
                    {
                        mealProportionsScrollView.AddMealSuggestion(prioritizedMealSuggestion);
                    }
                }
            }
        }

        private void AddMealSuggestionToList(MealSuggestion mealSuggestion)
        {
            var mealProportion = mealSuggestion.MealProportion;
            var mealSource = mealProportion.MealSource;
            var mealSourceType = mealSource.MealSourceType;

            var mealProportionsScrollView = _mealProportionsScrollViewDictionary[mealSourceType];
            var mealSuggestions = _mealSuggestionsDictionary[mealSourceType];
            var isUniqueMealSuggestion = !mealProportionsScrollView.MealProportions.Exists(m => m.MealSource == mealSource) &&
                !mealSuggestions.Exists(m => m.MealProportion.MealSource == mealSource);
            var isNotRemovedMealSuggestion = !_removedMealSuggestionsDictionary[mealSourceType].Contains(mealSuggestion);
            var doesExist = MealSourcesAdapter.DoesMealSourceExist(mealSource);

            if (isUniqueMealSuggestion && isNotRemovedMealSuggestion && doesExist)
            {
                mealSuggestions.Add(mealSuggestion);
            }
            else if (!doesExist)
            {
                Debug.LogWarning($"Meal source with ID: {mealSource.Id} does not exist");
            }
        }

        private void SubscribeToMealProportionScrollViewEvents()
        {
            foreach (var scrollView in _mealProportionsScrollViewDictionary.Values)
            {
                scrollView.MealProportionModified += ScrollView_OnMealProportionModified;
                scrollView.MealSuggestionRemoved += ScrollView_OnMealSuggestionRemoved;
            }
        }

        private void UnsubscribeToMealProportionScrollViewEvents()
        {
            foreach (var scrollView in _mealProportionsScrollViewDictionary.Values)
            {
                scrollView.MealSuggestionRemoved -= ScrollView_OnMealSuggestionRemoved;
                scrollView.MealProportionModified -= ScrollView_OnMealProportionModified;
            }
        }
    }
}
