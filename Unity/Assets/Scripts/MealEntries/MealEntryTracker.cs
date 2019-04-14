using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntryTracker : MonoBehaviour {

        public MealEntry CurrentMealEntry { get; private set; }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private TextMeshProUGUI _fatText = default;

        [SerializeField]
        private TextMeshProUGUI _carbsText = default;

        [SerializeField]
        private TextMeshProUGUI _proteinText = default;

        [SerializeField]
        private TextMeshProUGUI _caloriesText = default;

        private List<Meal> _mealProportions = new List<Meal>();
        private Meal _totalMeal = default;

        public void AddMealProportion(Meal mealProportion) {
            _mealProportions.Add(mealProportion);
            _totalMeal += mealProportion;
            CurrentMealEntry = new MealEntry(_date.CurrentDate, _totalMeal, _mealProportions);
            RefreshText();
        }

        public void SubtractMealProportion(Meal mealProportion) {
            _mealProportions.Remove(mealProportion);
            _totalMeal -= mealProportion;
            CurrentMealEntry = new MealEntry(_date.CurrentDate, _totalMeal, _mealProportions);
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
