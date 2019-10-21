using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMealsScrollView : AbstractScrollView {

        public List<MealProportion> MealProportions { get; private set; } = new List<MealProportion>();

        [SerializeField]
        protected Totals _totals = default;

        protected abstract string GetMealSourceName(MealProportion mealProportion);

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

            ScrollToBottom();
        }

        public void SubtractMealProportion(MealProportion mealProportion)
        {
            int mealProportionIndex = MealProportions.FindIndex(x => x == mealProportion);
            MealProportions.RemoveAt(mealProportionIndex);

            int childStartIndex = mealProportionIndex * _content.constraintCount;
            for (int i = 0; i < _content.constraintCount; i++)
            {
                int childIndex = childStartIndex + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
        }

        public void ClearMealProportions()
        {
            foreach (Transform child in _content.transform)
            {
                Destroy(child.gameObject);
            }
            MealProportions.Clear();
        }

        public override void DeleteRow(int rowIndex)
        {
            var destroyedMealProportion = MealProportions[rowIndex];
            SubtractMealProportion(destroyedMealProportion);
            _totals.RemoveFromTotals(destroyedMealProportion);
            base.DeleteRow(rowIndex);
        }
    }
}
