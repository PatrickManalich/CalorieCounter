using CalorieCounter.MealSources;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealProportionsScrollView : AbstractScrollView {

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        [SerializeField]
        private GameObject _suggestionScrollViewTextPrefab = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private bool _useCustomMealSourceName = default;

        [DisplayBasedOnBool("_useCustomMealSourceName", false)]
        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default;

        private const string CustomMealSourceName = "Custom Meal";

        private List<MealProportion> _mealSuggestions = new List<MealProportion>();

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
            nameText.GetComponent<TextMeshProUGUI>().text = _useCustomMealSourceName ? CustomMealSourceName : _mealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            ScrollToBottom();
        }

        public void AddMealSuggestion(MealProportion mealSuggestion)
        {
            _mealSuggestions.Add(mealSuggestion);

            int siblingStartIndex = (MealProportions.Count + _mealSuggestions.Count - 1) * _content.constraintCount;
            GameObject servingAmountText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, siblingStartIndex);
            GameObject nameText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject fatText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject carbText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject proteinText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);
            GameObject calorieText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab, ++siblingStartIndex);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _useCustomMealSourceName ? CustomMealSourceName : _mealSourcesAdapter.GetMealSourceName(mealSuggestion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.calories.ToString();
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
                var mealSuggestion = _mealSuggestions[rowIndex - MealProportions.Count];
                DeleteRow(rowIndex);
                AddMealProportion(mealSuggestion);
            }
        }

        public void ClearMealSuggestions()
        {
            var mealSuggestionsCount = _mealSuggestions.Count;   // Cache since we're changing list
            for (int i = 0; i < mealSuggestionsCount; i++)
            {
                DeleteRow(MealProportions.Count);
            }
        }

        public override void DeleteRow(int rowIndex)
        {
            if (MealProportions.Count > 0 && rowIndex < MealProportions.Count)
            {
                _totals.RemoveFromTotals(MealProportions[rowIndex]);
                MealProportions.RemoveAt(rowIndex);
            }
            else if (_mealSuggestions.Count > 0 && rowIndex >= MealProportions.Count)
            {
                _mealSuggestions.RemoveAt(rowIndex - MealProportions.Count);
            }
            base.DeleteRow(rowIndex);
        }

    }
}
