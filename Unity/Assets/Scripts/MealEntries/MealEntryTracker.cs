using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntryTracker : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _fatText = default;

        [SerializeField]
        private TextMeshProUGUI _carbsText = default;

        [SerializeField]
        private TextMeshProUGUI _proteinText = default;

        [SerializeField]
        private TextMeshProUGUI _caloriesText = default;

        private Meal _totalMeal = default;

        public void AddMealProportion(Meal mealProportion) {
            _totalMeal += mealProportion;
            RefreshText();
        }

        public void SubtractMealProportion(Meal mealProportion) {
            _totalMeal -= mealProportion;
            RefreshText();
        }

        private void RefreshText() {
            _fatText.text = _totalMeal.Fat.ToString() + "/0";
            _carbsText.text = _totalMeal.Carbs.ToString() + "/0";
            _proteinText.text = _totalMeal.Protein.ToString() + "/0";
            _caloriesText.text = _totalMeal.Calories.ToString() + "/0";
        }
    }
}
