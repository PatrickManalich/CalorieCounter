using System.Collections.Generic;
using System.Linq;
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

        private List<List<GameObject>> _mealProportionRows = new List<List<GameObject>>();

        private List<List<GameObject>> _mealSuggestionRows = new List<List<GameObject>>();

        public void AddMealProportion(MealProportion mealProportion)
        {
            MealProportions.Add(mealProportion);

            GameObject servingAmountText = InstantiateScrollViewText();
            GameObject nameText = InstantiateScrollViewText();
            GameObject fatText = InstantiateScrollViewText();
            GameObject carbText = InstantiateScrollViewText();
            GameObject proteinText = InstantiateScrollViewText();
            GameObject calorieText = InstantiateScrollViewText();

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = GetMealSourceName(mealProportion);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            var mealProportionRow = new List<GameObject>() { servingAmountText, nameText, fatText, carbText, proteinText, calorieText };
            _mealProportionRows.Add(mealProportionRow);
            ScrollToBottom();
        }

        public void AddMealSuggestion(MealProportion mealSuggestion)
        {
            GameObject servingAmountText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);
            GameObject nameText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);
            GameObject fatText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);
            GameObject carbText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);
            GameObject proteinText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);
            GameObject calorieText = InstantiateScrollViewText(_suggestionScrollViewTextPrefab);

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = GetMealSourceName(mealSuggestion);
            fatText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealSuggestion.calories.ToString();

            var mealSuggestionRow = new List<GameObject>() { servingAmountText, nameText, fatText, carbText, proteinText, calorieText };
            _mealSuggestionRows.Add(mealSuggestionRow);
        }

        public void ClearMealProportions()
        {
            MealProportions.Clear();
            foreach(var scrollViewText in _mealProportionRows.SelectMany(r => r))
            {
                Destroy(scrollViewText);
            }
            _mealProportionRows.Clear();
        }

        public void ClearMealSuggestions()
        {
            foreach (var scrollViewText in _mealSuggestionRows.SelectMany(r => r))
            {
                Destroy(scrollViewText);
            }
            _mealSuggestionRows.Clear();
        }

        public override void DeleteRow(int rowIndex)
        {
            _totals.RemoveFromTotals(MealProportions[rowIndex]);
            MealProportions.RemoveAt(rowIndex);
            _mealProportionRows.RemoveAt(rowIndex);
            base.DeleteRow(rowIndex);
        }

    }
}
