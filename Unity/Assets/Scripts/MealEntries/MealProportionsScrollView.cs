using CalorieCounter.MealSources;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(ScrollViewAssistant))]
    public class MealProportionsScrollView : MonoBehaviour {

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

        public ScrollViewAssistant ScrollViewAssistant { get; private set; }

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        public List<MealSuggestion> MealSuggestions { get; private set; } = new List<MealSuggestion>();

        [SerializeField]
        private GameObject _scrollViewSuggestionTextPrefab = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        private const string CustomMealSourceName = "Custom Meal";

        public void AddMealProportion(MealProportion mealProportion)
        {
            MealProportions.Add(mealProportion);

            int siblingStartIndex = MealProportions.IndexOf(mealProportion) * ScrollViewAssistant.Content.constraintCount;
            GameObject servingAmountText = ScrollViewAssistant.InstantiateScrollViewText(siblingStartIndex);
            GameObject nameText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = ScrollViewAssistant.InstantiateScrollViewText(++siblingStartIndex);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourceType == MealSourceType.Custom ? CustomMealSourceName : MealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            ScrollViewAssistant.ScrollToBottom();
            MealProportionModified?.Invoke(this, new MealProportionModifiedEventArgs(MealProportionModifiedType.Added, _mealSourceType, mealProportion));
            ScrollViewAssistant.InvokeRowAdded(siblingStartIndex);
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

            var mealProportion = mealSuggestion.mealProportion;
            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourceType == MealSourceType.Custom ? CustomMealSourceName : MealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();
        }

        public void ClearMealProportions()
        {
            var mealProportionsCount = MealProportions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealProportionsCount; i++)
            {
                ScrollViewAssistant.RemoveRow(0);
            }
        }

        public void AcceptSuggestion(int rowIndex)
        {
            if (rowIndex >= MealProportions.Count)
            {
                var mealSuggestion = MealSuggestions[rowIndex - MealProportions.Count];
                ScrollViewAssistant.RemoveRow(rowIndex);
                AddMealProportion(mealSuggestion.mealProportion);
            }
        }

        public void ClearMealSuggestions()
        {
            var mealSuggestionsCount = MealSuggestions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealSuggestionsCount; i++)
            {
                ScrollViewAssistant.RemoveRow(MealProportions.Count);
            }
        }

        private void Awake()
        {
            ScrollViewAssistant = GetComponent<ScrollViewAssistant>();
        }

        private void Start()
        {
            ScrollViewAssistant.RowRemoved += ScrollViewAssistant_OnRowRemoved;
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_OnCurrentDayTypeChanged;
        }

        private void OnDestroy()
        {
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_OnCurrentDayTypeChanged;
            ScrollViewAssistant.RowRemoved -= ScrollViewAssistant_OnRowRemoved;
        }

        private void ScrollViewAssistant_OnRowRemoved(object sender, ScrollViewAssistant.RowChangedEventArgs e)
        {
            if (MealProportions.Count > 0 && e.RowIndex < MealProportions.Count)
            {
                var removedMealProportion = MealProportions[e.RowIndex];
                MealProportions.Remove(removedMealProportion);
                MealProportionModified?.Invoke(this, new MealProportionModifiedEventArgs(MealProportionModifiedType.Removed, _mealSourceType, removedMealProportion));
            }
            else if (MealSuggestions.Count > 0 && e.RowIndex >= MealProportions.Count)
            {
                var removedMealSuggestion = MealSuggestions[e.RowIndex - MealProportions.Count];
                MealSuggestions.Remove(removedMealSuggestion);
                MealSuggestionRemoved?.Invoke(this, new MealSuggestionRemovedEventArgs(_mealSourceType, removedMealSuggestion));
            }
        }

        private void DayTypeDropdown_OnCurrentDayTypeChanged()
        {
            if(!_dayTypeDropdown.IsCurrentDayTypeRestOrTraining)
            {
                ClearMealProportions();
            }
        }
    }
}
