using CalorieCounter.MealSources;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealProportionsScrollView : ScrollView {

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
        private GameObject _suggestionScrollViewTextPrefab = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        private const string CustomMealSourceName = "Custom Meal";

        public override void DeleteRow(int rowIndex)
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
            base.DeleteRow(rowIndex);
        }

        public void AddMealProportion(MealProportion mealProportion)
        {
            MealProportions.Add(mealProportion);

            int siblingStartIndex = MealProportions.IndexOf(mealProportion) * Content.constraintCount;
            GameObject servingAmountText = InstantiateScrollViewText(siblingStartIndex);
            GameObject nameText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = InstantiateScrollViewText(++siblingStartIndex);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourceType == MealSourceType.Custom ? CustomMealSourceName : MealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            ScrollToBottom();
            MealProportionModified?.Invoke(this, new MealProportionModifiedEventArgs(MealProportionModifiedType.Added, _mealSourceType, mealProportion));
            InvokeRowChanged();
        }

        public void AddMealSuggestion(MealSuggestion mealSuggestion)
        {
            MealSuggestions.Add(mealSuggestion);

            int siblingStartIndex = (MealProportions.Count + MealSuggestions.Count - 1) * Content.constraintCount;
            GameObject servingAmountText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, siblingStartIndex);
            GameObject nameText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject fatText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject carbText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject proteinText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject calorieText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);

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
                DeleteRow(0);
            }
        }

        public void AcceptSuggestion(int rowIndex)
        {
            if (rowIndex >= MealProportions.Count)
            {
                var mealSuggestion = MealSuggestions[rowIndex - MealProportions.Count];
                DeleteRow(rowIndex);
                AddMealProportion(mealSuggestion.mealProportion);
            }
        }

        public void ClearMealSuggestions()
        {
            var mealSuggestionsCount = MealSuggestions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealSuggestionsCount; i++)
            {
                DeleteRow(MealProportions.Count);
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

        private void DayTypeDropdown_OnCurrentDayTypeChanged()
        {
            if(!_dayTypeDropdown.IsCurrentDayTypeRestOrTraining)
            {
                ClearMealProportions();
            }
        }
    }
}
