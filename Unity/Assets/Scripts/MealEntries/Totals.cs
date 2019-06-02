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
        private DayTypeDropdown _dayTypeDropdown = default;

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

        public void ResetTotals() {
            TotalFat = 0;
            TotalCarbs = 0;
            TotalProtein = 0;
            TotalCalories = 0;
        }

        public void Refresh() {
            _targetEntry = _targetEntryHandler.GetLatestTargetEntry(_date.CurrentDate);
            UpdateTexts();
        }

        private void UpdateTexts() {
            TotalFat = GlobalMethods.Round(TotalFat);
            TotalCarbs = GlobalMethods.Round(TotalCarbs);
            TotalProtein = GlobalMethods.Round(TotalProtein);
            TotalCalories = GlobalMethods.Round(TotalCalories);

            float targetEntryFat = 0;
            float targetEntryCarbs = 0;
            float targetEntryProtein = 0;
            float targetEntryCalories = 0;
            if (_dayTypeDropdown.DayType == DayType.Rest) {
                targetEntryFat = _targetEntry.RestDayFat;
                targetEntryCarbs = _targetEntry.RestDayCarbs;
                targetEntryProtein = _targetEntry.RestDayProtein;
                targetEntryCalories = _targetEntry.RestDayCalories;
            } else if (_dayTypeDropdown.DayType == DayType.Training) {
                targetEntryFat = _targetEntry.TrainingDayFat;
                targetEntryCarbs = _targetEntry.TrainingDayCarbs;
                targetEntryProtein = _targetEntry.TrainingDayProtein;
                targetEntryCalories = _targetEntry.TrainingDayCalories;
            }

            _fatText.text = TotalFat.ToString() + " / " + targetEntryFat;
            _carbsText.text = TotalCarbs.ToString() + " / " + targetEntryCarbs;
            _proteinText.text = TotalProtein.ToString() + " / " + targetEntryProtein;
            _caloriesText.text = TotalCalories.ToString() + " / " + targetEntryCalories;
        }
    }
}
