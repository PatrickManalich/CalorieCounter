using UnityEngine;

namespace CalorieCounter.MealEntries.CommonMeals {

    public class SubmitButton : MonoBehaviour {

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private MealSourceDropdown _mealSourceDropdown = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        public void TryAddingMealProportion() {
            MealProportion mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, _mealSourceDropdown.SelectedMealSource);
            _mealEntryHandler.AddMealProportion(mealProportion);
            _scrollView.AddMealProportion(mealProportion);
            _mealSourceDropdown.ResetDropdown();
            _servingAmountDropdown.ResetDropdown();
        }
    }
}
