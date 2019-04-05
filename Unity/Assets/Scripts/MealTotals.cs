using TMPro;
using UnityEngine;

namespace CalorieCounter {

    public class MealTotals : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _fatText;

        [SerializeField]
        private TextMeshProUGUI _carbsText;

        [SerializeField]
        private TextMeshProUGUI _proteinText;

        [SerializeField]
        private TextMeshProUGUI _caloriesText;

        private Meal _mealTotals = default;

        public void AddMealProportion(Meal mealProportion) {
            _mealTotals += mealProportion;
            RefreshText();
        }

        public void SubtractMealProportion(Meal mealProportion) {
            _mealTotals -= mealProportion;
            RefreshText();
        }

        private void RefreshText() {
            _fatText.text = _mealTotals.Fat.ToString() + "/0";
            _carbsText.text = _mealTotals.Carbs.ToString() + "/0";
            _proteinText.text = _mealTotals.Protein.ToString() + "/0";
            _caloriesText.text = _mealTotals.Calories.ToString() + "/0";
        }
    }
}
