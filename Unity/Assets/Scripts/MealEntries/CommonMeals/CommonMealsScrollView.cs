using CalorieCounter.MealSources;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class CommonMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default; 

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _namedMealSourceDropdown = default;

        [SerializeField]
        private Totals _totals = default;

        private List<MealProportion> _mealProportions = new List<MealProportion>();

        public void AddCommonMealProportionFromDropdowns() {
            var mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, _namedMealSourceDropdown.SelectedNamedMealSource.mealSource);
            AddMealProportion(mealProportion);
            _totals.AddToTotals(mealProportion);
        }

        public override List<MealProportion> GetMealProportions()
        {
            return _mealProportions;
        }

        public override void AddMealProportion(MealProportion mealProportion) {
            _mealProportions.Add(mealProportion);

            GameObject servingAmountText = InstantiateScrollViewText();
            GameObject nameText = InstantiateScrollViewText();
            GameObject fatText = InstantiateScrollViewText();
            GameObject carbText = InstantiateScrollViewText();
            GameObject proteinText = InstantiateScrollViewText();
            GameObject calorieText = InstantiateScrollViewText();

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            ScrollToBottom();
        }

        public override void SubtractMealProportion(MealProportion mealProportion) {
            int mealProportionIndex = _mealProportions.FindIndex(x => x == mealProportion);
            _mealProportions.RemoveAt(mealProportionIndex);

            int childStartIndex = mealProportionIndex * _content.constraintCount;
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = childStartIndex + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
        }

        public override void ClearMealProportions() {
            foreach(Transform child in _content.transform) {
                Destroy(child.gameObject);
            }
            _mealProportions.Clear();
        }

        public override void DeleteRow(int rowIndex)
        {
            var destroyedMealProportion = _mealProportions[rowIndex];
            SubtractMealProportion(destroyedMealProportion);
            _totals.RemoveFromTotals(destroyedMealProportion);
            base.DeleteRow(rowIndex);
        }
    }
}
