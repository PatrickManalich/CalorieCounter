using CalorieCounter.Globals;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntryHandler : MonoBehaviour {

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

        private float _totalFat = 0;
        private float _totalCarbs = 0;
        private float _totalProtein = 0;
        private float _totalCalories = 0;
        private Dictionary<MealTypes, List<MealProportion>> _mealProportionsDict = new Dictionary<MealTypes, List<MealProportion>>() {
            { MealTypes.Small, new List<MealProportion>() },
            { MealTypes.Large, new List<MealProportion>() },
        };

        public MealEntry GetMealEntry() {
            return new MealEntry(_date.CurrentDate, _totalFat, _totalCarbs, _totalProtein, _totalCalories, _mealProportionsDict);
        }

        public string GetMealEntryPath() {
            string mealEntryFileDate = "-" + _date.CurrentDate.Year + "-" + _date.CurrentDate.Month + "-" + _date.CurrentDate.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.MealEntryFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDir, mealEntryFileName);
        }

        public void AddMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.Source.MealType].Add(mealProportion);
            _totalFat += mealProportion.Fat;
            _totalCarbs += mealProportion.Carbs;
            _totalProtein += mealProportion.Protein;
            _totalCalories += mealProportion.Calories;
            Refresh();
        }

        public void SubtractMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.Source.MealType].Remove(mealProportion);
            _totalFat -= mealProportion.Fat;
            _totalCarbs -= mealProportion.Carbs;
            _totalProtein -= mealProportion.Protein;
            _totalCalories -= mealProportion.Calories;
            Refresh();
        }

        private void Awake() {
            Refresh();
        }

        private void Refresh() {
            _totalFat = GlobalMethods.Round(_totalFat);
            _totalCarbs = GlobalMethods.Round(_totalCarbs);
            _totalProtein = GlobalMethods.Round(_totalProtein);
            _totalCalories = GlobalMethods.Round(_totalCalories);
            _fatText.text = _totalFat.ToString() + "/0";
            _carbsText.text = _totalCarbs.ToString() + "/0";
            _proteinText.text = _totalProtein.ToString() + "/0";
            _caloriesText.text = _totalCalories.ToString() + "/0";
        }
    }
}
