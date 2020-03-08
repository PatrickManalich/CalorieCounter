using CalorieCounter.MealSources;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealProportionsScrollView : AbstractScrollView {

        public class MealSuggestionRemovedEventArgs : EventArgs
        {
            public MealSourceType MealSourceType { get; private set; }
            public MealSuggestion RemovedMealSuggestion { get; private set; }

            public MealSuggestionRemovedEventArgs(MealSourceType mealSourceType, MealSuggestion removedMealSuggestion)
            {
                MealSourceType = mealSourceType;
                RemovedMealSuggestion = removedMealSuggestion;
            }
        }

        public event EventHandler<MealSuggestionRemovedEventArgs> MealSuggestionRemoved;
        public Action MealProportionAdded;

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        public List<MealSuggestion> MealSuggestions { get; private set; } = new List<MealSuggestion>();

        [SerializeField]
        private GameObject _suggestionScrollViewTextPrefab = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        private const string CustomMealSourceName = "Custom Meal";

        public void AddMealProportion(MealProportion mealProportion)
        {
            MealProportions.Add(mealProportion);
            _totals.AddToTotals(mealProportion);

            int siblingStartIndex = MealProportions.IndexOf(mealProportion) * _content.constraintCount;
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
            MealProportionAdded?.Invoke();
        }

        public void AddMealSuggestion(MealSuggestion mealSuggestion)
        {
            MealSuggestions.Add(mealSuggestion);

            int siblingStartIndex = (MealProportions.Count + MealSuggestions.Count - 1) * _content.constraintCount;
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

        public override void AcceptSuggestion(int rowIndex)
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

        public override void DeleteRow(int rowIndex)
        {
            base.DeleteRow(rowIndex);
            if (MealProportions.Count > 0 && rowIndex < MealProportions.Count)
            {
                _totals.RemoveFromTotals(MealProportions[rowIndex]);
                MealProportions.RemoveAt(rowIndex);
            }
            else if (MealSuggestions.Count > 0 && rowIndex >= MealProportions.Count)
            {
                var removedMealSuggestion = MealSuggestions[rowIndex - MealProportions.Count];
                MealSuggestions.Remove(removedMealSuggestion);
                MealSuggestionRemoved?.Invoke(this, new MealSuggestionRemovedEventArgs(_mealSourceType, removedMealSuggestion));
            }
        }
    }
}
