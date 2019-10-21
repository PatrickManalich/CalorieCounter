using CalorieCounter.MealEntries;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class CustomMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private CustomMealProportionInputFields _customMealInputFields = default;

        private const string CustomMealSourceName = "Custom Meal";

        protected override string GetMealSourceName(MealProportion mealProportion)
        {
            return CustomMealSourceName;
        }

        public void AddCustomMealProportionFromInputFields() {
            MealProportion mealProportion = _customMealInputFields.GetCustomMealProportionFromInputFields();
            AddMealProportion(mealProportion);
            _totals.AddToTotals(mealProportion);
        }
    }
}
