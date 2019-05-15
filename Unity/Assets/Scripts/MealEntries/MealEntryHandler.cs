using CalorieCounter.Globals;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntryHandler : MonoBehaviour {

        [System.Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealTypes, AbstractScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDict = default;

        [SerializeField]
        private Totals _totals = default;

        private Dictionary<MealTypes, List<MealProportion>> _mealProportionsDict = new Dictionary<MealTypes, List<MealProportion>>() {
            { MealTypes.Small, new List<MealProportion>() },
            { MealTypes.Large, new List<MealProportion>() },
            { MealTypes.Custom, new List<MealProportion>() },
        };

        public void AddMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.Source.MealType].Add(mealProportion);
            _totals.AddToTotals(mealProportion);
        }

        public void SubtractMealProportion(MealProportion mealProportion) {
            _mealProportionsDict[mealProportion.Source.MealType].Remove(mealProportion);
            _totals.RemoveFromTotals(mealProportion);
        }

        public void ExportMealEntry() {
            MealEntry currentMealEntry = new MealEntry(_date.CurrentDate, _totals.TotalFat, _totals.TotalCarbs, _totals.TotalProtein, 
                _totals.TotalCalories, _mealProportionsDict);
            JsonUtility.Export(currentMealEntry, GetMealEntryPath());
        }

        public void Refresh() {
            foreach (var key in _mealProportionsDict.Keys) {
                _mealProportionsDict[key].Clear();
                _scrollViewDict[key].ClearMealProportions();
            }
            _totals.Refresh();

            MealEntry importedMealEntry = JsonUtility.Import<MealEntry>(GetMealEntryPath());
            if (importedMealEntry != default) {
                foreach (var key in importedMealEntry.MealProportionsDict.Keys) {
                    AbstractScrollView scrollView = _scrollViewDict[key];
                    foreach (var mealProportion in importedMealEntry.MealProportionsDict[key]) {
                        AddMealProportion(mealProportion);
                        scrollView.AddMealProportion(mealProportion);
                    }
                }
            }
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
