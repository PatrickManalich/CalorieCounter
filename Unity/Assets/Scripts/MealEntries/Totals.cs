using CalorieCounter.Globals;
using CalorieCounter.TargetEntries;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class Totals : MonoBehaviour {

        [SerializeField]
        private TargetEntryHandler _targetEntryHandler = default;

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

        public float TotalFat { get; private set; } = 0;
        public float TotalCarbs { get; private set; } = 0;
        public float TotalProtein { get; private set; } = 0;
        public float TotalCalories { get; private set; } = 0;

        private TargetEntry _targetEntry;

        public void AddToTotals(MealProportion mealProportion) {
            TotalFat += mealProportion.Fat;
            TotalCarbs += mealProportion.Carbs;
            TotalProtein += mealProportion.Protein;
            TotalCalories += mealProportion.Calories;
            UpdateTexts();
        }

        public void RemoveFromTotals(MealProportion mealProportion) {
            TotalFat -= mealProportion.Fat;
            TotalCarbs -= mealProportion.Carbs;
            TotalProtein -= mealProportion.Protein;
            TotalCalories -= mealProportion.Calories;
            UpdateTexts();
        }

        public void Refresh() {
            TotalFat = 0;
            TotalCarbs = 0;
            TotalProtein = 0;
            TotalCalories = 0;
            _targetEntry = _targetEntryHandler.GetLatestTargetEntry(_date.CurrentDate);
            UpdateTexts();
        }

        private void UpdateTexts() {
            TotalFat = GlobalMethods.Round(TotalFat);
            TotalCarbs = GlobalMethods.Round(TotalCarbs);
            TotalProtein = GlobalMethods.Round(TotalProtein);
            TotalCalories = GlobalMethods.Round(TotalCalories);

            _fatText.text = TotalFat.ToString() + " / " + _targetEntry.TrainingDayFat;
            _carbsText.text = TotalCarbs.ToString() + " / " + _targetEntry.TrainingDayCarbs;
            _proteinText.text = TotalProtein.ToString() + " / " + _targetEntry.TrainingDayProtein;
            _caloriesText.text = TotalCalories.ToString() + " / " + _targetEntry.TrainingDayCalories;
        }
    }
}
