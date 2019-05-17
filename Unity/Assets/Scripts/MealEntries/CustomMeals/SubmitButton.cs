using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class SubmitButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        public void TryAddingMealProportionFromInputFields() {
            MealProportion customMealProportion = _scrollView.GetCustomMealProportionFromInputFields();
            _mealEntryHandler.AddMealProportion(customMealProportion);
            _scrollView.AddMealProportion(customMealProportion);
        }
    }
}
