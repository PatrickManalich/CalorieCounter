using System.Collections.Generic;
using System.IO;
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

        private const string MealEntriesDir = @"MealEntries";
        private const string MealEntryFilePrefix = @"MealEntry";
        private const string MealEntryFileExtension = @".json";
        private Dictionary<MealTypes, List<MealSource>> _mealProportionsDict = new Dictionary<MealTypes, List<MealSource>>() {
            { MealTypes.Small, new List<MealSource>() },
            { MealTypes.Large, new List<MealSource>() },
        };
        private MealSource _totalMeal = default;

        public string GetCurrentMealEntryPath() {
            string mealEntryFileDate = "-" + CurrentMealEntry.Date.Year + "-" + CurrentMealEntry.Date.Month + "-" + CurrentMealEntry.Date.Day;
            string mealEntryFileName = MealEntryFilePrefix + mealEntryFileDate + MealEntryFileExtension;
            return Path.Combine(MealEntriesDir, mealEntryFileName);
        }

        public void AddMealProportion(MealSource mealProportion) {
            _mealProportionsDict[mealProportion.MealType].Add(mealProportion);
            _totalMeal += mealProportion;
            Refresh();
        }

        public void SubtractMealProportion(MealSource mealProportion) {
            _mealProportionsDict[mealProportion.MealType].Remove(mealProportion);
            _totalMeal -= mealProportion;
            Refresh();
        }

        private void Awake() {
            Refresh();
        }

        private void Refresh() {
            CurrentMealEntry = new MealEntry(_date.CurrentDate, _totalMeal, _mealProportionsDict);
            _fatText.text = _totalMeal.Fat.ToString() + "/0";
            _carbsText.text = _totalMeal.Carbs.ToString() + "/0";
            _proteinText.text = _totalMeal.Protein.ToString() + "/0";
            _caloriesText.text = _totalMeal.Calories.ToString() + "/0";
        }
    }
}
