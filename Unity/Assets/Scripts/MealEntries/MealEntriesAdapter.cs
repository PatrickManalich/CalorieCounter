using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntriesAdapter : AbstractAdapter {

        [Serializable]
        private class MealProportionsScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealProportionsScrollView> { }

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private MealProportionsScrollViewDictionary _mealProportionsScrollViewDictionary = default;

        public override void Export()
        {
            GameManager.MealEntriesManager.ExportMealEntry(GetScrollViewsMealEntry(), _date.CurrentDateTime);
        }

        public override bool DoDifferencesExist()
        {
            var importedMealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            var scrollViewsMealEntry = GetScrollViewsMealEntry();
            var doMealEntriesDiffer = importedMealEntry != scrollViewsMealEntry;
            return doMealEntriesDiffer;
        }

        public void Export(MealEntry mealEntry, DateTime dateTime)
        {
            GameManager.MealEntriesManager.ExportMealEntry(mealEntry, dateTime);
            Refresh();
        }

        public DayType GetMealEntryDayType()
        {
            var mealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            return mealEntry.DayType;
        }

        private void Start() {
            _date.CurrentDateTimeChanged += Refresh;
            Refresh();
        }
        private void OnDestroy()
        {
            _date.CurrentDateTimeChanged -= Refresh;
        }

        private void Refresh()
        {
            foreach (var mealSourceType in _mealProportionsScrollViewDictionary.Keys)
            {
                _mealProportionsScrollViewDictionary[mealSourceType].ClearMealProportions();
            }

            MealEntry mealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDateTime);
            foreach (var mealSourceType in mealEntry.MealProportionsDictionary.Keys)
            {
                MealProportionsScrollView scrollView = _mealProportionsScrollViewDictionary[mealSourceType];
                foreach (var mealProportion in mealEntry.MealProportionsDictionary[mealSourceType])
                {
                    scrollView.AddMealProportion(mealProportion);
                }
                scrollView.ScrollViewAssistant.ScrollToTop();
            }
        }

        private MealEntry GetScrollViewsMealEntry()
        {
            var mealProportionsDictionary = new Dictionary<MealSourceType, List<MealProportion>>
            {
                { MealSourceType.Small, _mealProportionsScrollViewDictionary[MealSourceType.Small].MealProportions },
                { MealSourceType.Large, _mealProportionsScrollViewDictionary[MealSourceType.Large].MealProportions },
                { MealSourceType.Custom, _mealProportionsScrollViewDictionary[MealSourceType.Custom].MealProportions }
            };
            return new MealEntry(_date.CurrentDateTime, _dayTypeDropdown.CurrentDayType, mealProportionsDictionary);
        }
    }
}
