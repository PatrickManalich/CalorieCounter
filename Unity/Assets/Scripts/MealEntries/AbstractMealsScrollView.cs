using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMealsScrollView : AbstractScrollView {

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        [SerializeField]
        private GameObject _suggestionScrollViewTextPrefab = default;

        [SerializeField]
        protected Totals _totals = default;

        protected abstract string GetMealSourceName(MealProportion mealProportion);

        private List<MealProportion> _mealSuggestions = new List<MealProportion>();

        public void AddMealProportion(MealProportion mealProportion)
        {
            MealProportions.Add(mealProportion);

            int siblingStartIndex = MealProportions.IndexOf(mealProportion) * _content.constraintCount;
            GameObject servingAmountText = InstantiateScrollViewText(siblingStartIndex);
            GameObject nameText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject fatText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject carbText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject proteinText = InstantiateScrollViewText(++siblingStartIndex);
            GameObject calorieText = InstantiateScrollViewText(++siblingStartIndex);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = GetMealSourceName(mealProportion);
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
            nameText.GetComponent<TextMeshProUGUI>().text = GetMealSourceName(mealSuggestion);
            fatText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.calories.ToString();
        }

        public void ClearMealProportionsAndSuggestions()
        {
            var mealProportionsAndSuggestionsCount = MealProportions.Count + _mealSuggestions.Count;
            for (int i = 0; i < mealProportionsAndSuggestionsCount; i++)
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
                _totals.AddToTotals(mealSuggestion);
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
