using TMPro;
using UnityEngine;

namespace CalorieCounter.Meals {

    public class Totals : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _fatText;

        [SerializeField]
        private TextMeshProUGUI _carbsText;

        [SerializeField]
        private TextMeshProUGUI _proteinText;

        [SerializeField]
        private TextMeshProUGUI _caloriesText;

        private Meal _totalsMeal = default;

        public void AddMealProportion(Meal mealProportion) {
            _totalsMeal += mealProportion;
            RefreshText();
        }

        public void SubtractMealProportion(Meal mealProportion) {
            _totalsMeal -= mealProportion;
            RefreshText();
        }

        private void RefreshText() {
            _fatText.text = _totalsMeal.Fat.ToString() + "/0";
            _carbsText.text = _totalsMeal.Carbs.ToString() + "/0";
            _proteinText.text = _totalsMeal.Protein.ToString() + "/0";
            _caloriesText.text = _totalsMeal.Calories.ToString() + "/0";
        }
    }
}
