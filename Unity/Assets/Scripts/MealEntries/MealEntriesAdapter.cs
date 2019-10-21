using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntriesAdapter : AbstractAdapter {

        [System.Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, AbstractMealsScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        public override void Export() {
            var mealProportionsDictionary = new Dictionary<MealSourceType, List<MealProportion>>
            {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealProportions },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealProportions },
                { MealSourceType.Custom, _scrollViewDictionary[MealSourceType.Custom].MealProportions }
            };

            MealEntry currentMealEntry = new MealEntry(_date.CurrentDateTime, _dayTypeDropdown.DayType, _totals.TotalFat, _totals.TotalCarbs, _totals.TotalProtein, 
                _totals.TotalCalories, mealProportionsDictionary);
            GameManager.MealEntriesManager.ExportMealEntry(currentMealEntry, _date.CurrentDateTime);
        }

        public void Refresh() {
            foreach (var key in _scrollViewDictionary.Keys) {
                _scrollViewDictionary[key].ClearMealProportions();
            }
            _totals.ResetTotals();
            _totals.Refresh();

            MealEntry mealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            if (mealEntry != default) {
                foreach (var key in mealEntry.mealProportionsDictionary.Keys) {
                    AbstractMealsScrollView scrollView = _scrollViewDictionary[key];
                    foreach (var mealProportion in mealEntry.mealProportionsDictionary[key]) {
                        scrollView.AddMealProportion(mealProportion);
                        _totals.AddToTotals(mealProportion);
                    }
                    scrollView.ScrollToTop();
                }
            }
            _dayTypeDropdown.HardSetDayType(mealEntry.dayType);
        }

        private void Start() {
            Refresh();
        }
    }
}
