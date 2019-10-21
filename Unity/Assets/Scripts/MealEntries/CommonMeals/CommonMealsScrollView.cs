using CalorieCounter.MealSources;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class CommonMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default; 

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _namedMealSourceDropdown = default;

        protected override string GetMealSourceName(MealProportion mealProportion)
        {
            return _mealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
        }

        public void AddCommonMealProportionFromDropdowns() {
            var mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, _namedMealSourceDropdown.SelectedNamedMealSource.mealSource);
            AddMealProportion(mealProportion);
            _totals.AddToTotals(mealProportion);
        }
    }
}
