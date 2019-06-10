using CalorieCounter.Globals;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntryHandler : MonoBehaviour {

        [System.Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, AbstractMealsScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDict = default;

        private Dictionary<MealSourceType, List<MealProportion>> _mealProportionsDict = new Dictionary<MealSourceType, List<MealProportion>>() {
            { MealSourceType.Small, new List<MealProportion>() },
            { MealSourceType.Large, new List<MealProportion>() },
            { MealSourceType.Custom, new List<MealProportion>() },
        };

        public void AddMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.MealSource.MealSourceType].Add(mealProportion);
            _totals.AddToTotals(mealProportion);
        }

        public void SubtractMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.MealSource.MealSourceType].Remove(mealProportion);
            _totals.RemoveFromTotals(mealProportion);
        }

        public void ExportMealEntry() {
            MealEntry currentMealEntry = new MealEntry(_date.CurrentDate, _dayTypeDropdown.DayType, _totals.TotalFat, _totals.TotalCarbs, _totals.TotalProtein, 
                _totals.TotalCalories, _mealProportionsDict);
            JsonConverter.Export(currentMealEntry, GetMealEntryPath());
        }

        public void Refresh() {
            foreach (var key in _mealProportionsDict.Keys) {
                _mealProportionsDict[key].Clear();
                _scrollViewDict[key].ClearMealProportions();
            }
            _totals.Refresh();

            MealEntry importedMealEntry = JsonConverter.Import<MealEntry>(GetMealEntryPath());
            if (importedMealEntry != default) {
                foreach (var key in importedMealEntry.MealProportionsDict.Keys) {
                    AbstractMealsScrollView scrollView = _scrollViewDict[key];
                    foreach (var mealProportion in importedMealEntry.MealProportionsDict[key]) {
                        AddMealProportion(mealProportion);
                        scrollView.AddMealProportion(mealProportion);
                    }
                }
            }
            _dayTypeDropdown.HardSetDayType(importedMealEntry.DayType);
        }

        private void Start() {
            Refresh();
        }

        private string GetMealEntryPath() {
            string mealEntryFileDate = "-" + _date.CurrentDate.Year + "-" + _date.CurrentDate.Month + "-" + _date.CurrentDate.Day;
            string mealEntryFileName = GlobalPaths.MealEntryFilePrefix + mealEntryFileDate + GlobalPaths.MealEntryFileExtension;
            return Path.Combine(GlobalPaths.MealEntriesDir, mealEntryFileName);
        }
    }
}
