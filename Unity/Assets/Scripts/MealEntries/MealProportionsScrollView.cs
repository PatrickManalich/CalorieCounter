using CalorieCounter.MealSources;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealProportionsScrollView : ScrollView
    {

        public class MealProportionModifiedEventArgs : EventArgs
        {
            public MealProportionModifiedType MealProportionModifiedType { get; }
            public MealSourceType MealSourceType { get; }
            public MealProportion MealProportion { get; }

            public MealProportionModifiedEventArgs(MealProportionModifiedType mealProportionModifiedType, MealSourceType mealSourceType, MealProportion mealProportion)
            {
                MealProportionModifiedType = mealProportionModifiedType;
                MealSourceType = mealSourceType;
                MealProportion = mealProportion;
            }
        }

        public class MealSuggestionRemovedEventArgs : EventArgs
        {
            public MealSourceType MealSourceType { get; }
            public MealSuggestion RemovedMealSuggestion { get; }

            public MealSuggestionRemovedEventArgs(MealSourceType mealSourceType, MealSuggestion removedMealSuggestion)
            {
                MealSourceType = mealSourceType;
                RemovedMealSuggestion = removedMealSuggestion;
            }
        }

        public event EventHandler<MealProportionModifiedEventArgs> MealProportionModified;
        public event EventHandler<MealSuggestionRemovedEventArgs> MealSuggestionRemoved;

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        public List<MealSuggestion> MealSuggestions { get; private set; } = new List<MealSuggestion>();

        [SerializeField]
        private GameObject _scrollViewSuggestionTextPrefab = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        public void AddMealProportion(MealProportion mealProportion, bool scrollToBottom = true)
        {
            MealProportions.Add(mealProportion);

            int siblingStartIndex = MealProportions.IndexOf(mealProportion) * ScrollViewAssistant.Content.constraintCount;
            GameObject servingAmountText = ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex);
            GameObject nameText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.ServingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourceType == MealSourceType.Custom ? NamedMealSource.CustomMealSourceName : MealSourcesAdapter.GetMealSourceName(mealProportion.MealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();

            if (scrollToBottom)
            {
                ScrollViewAssistant.ScrollToBottom();
            }
            MealProportionModified?.Invoke(this, new MealProportionModifiedEventArgs(MealProportionModifiedType.Added, _mealSourceType, mealProportion));
            InvokeRowAdded(siblingStartIndex);
        }

        public void AddMealSuggestion(MealSuggestion mealSuggestion)
        {
            MealSuggestions.Add(mealSuggestion);

            int siblingStartIndex = (MealProportions.Count + MealSuggestions.Count - 1) * ScrollViewAssistant.Content.constraintCount;
            GameObject servingAmountText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, siblingStartIndex);
            GameObject nameText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, ++siblingStartIndex);
            GameObject fatText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, ++siblingStartIndex);
            GameObject carbText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, ++siblingStartIndex);
            GameObject proteinText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, ++siblingStartIndex);
            GameObject calorieText = ScrollViewAssistant.InstantiateScrollViewText(_scrollViewSuggestionTextPrefab, ++siblingStartIndex);

            var mealProportion = mealSuggestion.MealProportion;
            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.ServingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourceType == MealSourceType.Custom ? NamedMealSource.CustomMealSourceName : MealSourcesAdapter.GetMealSourceName(mealProportion.MealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();
        }

        public void RemoveRow(int rowIndex)
        {
            if (MealProportions.Count > 0 && rowIndex < MealProportions.Count)
            {
                var removedMealProportion = MealProportions[rowIndex];
                MealProportions.Remove(removedMealProportion);
                MealProportionModified?.Invoke(this, new MealProportionModifiedEventArgs(MealProportionModifiedType.Removed, _mealSourceType, removedMealProportion));
            }
            else if (MealSuggestions.Count > 0 && rowIndex >= MealProportions.Count)
            {
                var removedMealSuggestion = MealSuggestions[rowIndex - MealProportions.Count];
                MealSuggestions.Remove(removedMealSuggestion);
                MealSuggestionRemoved?.Invoke(this, new MealSuggestionRemovedEventArgs(_mealSourceType, removedMealSuggestion));
            }
            ScrollViewAssistant.RemoveRow(rowIndex);
            InvokeRowRemoved(rowIndex);
        }

        public void ClearMealProportions()
        {
            var mealProportionsCount = MealProportions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealProportionsCount; i++)
            {
                RemoveRow(0);
            }
        }

        public void AcceptSuggestion(int rowIndex)
        {
            if (rowIndex >= MealProportions.Count)
            {
                var mealProportion = MealSuggestions[rowIndex - MealProportions.Count].MealProportion;
                RemoveRow(rowIndex);
                AddMealProportion(mealProportion);
            }
        }

        public void ClearMealSuggestions()
        {
            var mealSuggestionsCount = MealSuggestions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealSuggestionsCount; i++)
            {
                RemoveRow(MealProportions.Count);
            }
        }

        private void Start()
        {
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_OnCurrentDayTypeChanged;
        }

        private void OnDestroy()
        {
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_OnCurrentDayTypeChanged;
        }

        private void DayTypeDropdown_OnCurrentDayTypeChanged(object sender, EventArgs e)
        {
            if(!_dayTypeDropdown.IsCurrentDayTypeRestOrTraining)
            {
                ClearMealProportions();
            }
        }
    }
}
