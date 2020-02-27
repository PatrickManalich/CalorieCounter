using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntriesAdapter : AbstractAdapter {

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealProportionsScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        public override void Export() {
            var mealProportionsDictionary = new Dictionary<MealSourceType, List<MealProportion>>
            {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].MealProportions },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].MealProportions },
                { MealSourceType.Custom, _scrollViewDictionary[MealSourceType.Custom].MealProportions }
            };

            MealEntry currentMealEntry = new MealEntry(_date.CurrentDateTime, _dayTypeDropdown.CurrentDayType, mealProportionsDictionary);
            GameManager.MealEntriesManager.ExportMealEntry(currentMealEntry, _date.CurrentDateTime);
        }

        public DayType GetMealEntryDayType()
        {
            MealEntry mealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            return mealEntry.dayType;
        }

        public void Refresh() {
            foreach (var mealSourceType in _scrollViewDictionary.Keys) {
                _scrollViewDictionary[mealSourceType].ClearMealProportions();
            }

            MealEntry mealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            if (mealEntry != default) {
                foreach (var mealSourceType in mealEntry.mealProportionsDictionary.Keys) {
                    MealProportionsScrollView scrollView = _scrollViewDictionary[mealSourceType];
                    foreach (var mealProportion in mealEntry.mealProportionsDictionary[mealSourceType]) {
                        scrollView.AddMealProportion(mealProportion);
                    }
                    scrollView.ScrollToTop();
                }
            }
        }

        private void Start() {
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
            Refresh();
        }
        private void OnDestroy()
        {
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
        }

        private void Date_OnCurrentDateTimeChanged()
        {
            Refresh();
        }
    }
}
