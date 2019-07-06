﻿using CalorieCounter.Managers;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class MealEntriesAdapter : MonoBehaviour {

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

        public void ExportMealEntry() {
            var mealProportionsDictionary = new Dictionary<MealSourceType, List<MealProportion>>
            {
                { MealSourceType.Small, _scrollViewDictionary[MealSourceType.Small].GetMealProportions() },
                { MealSourceType.Large, _scrollViewDictionary[MealSourceType.Large].GetMealProportions() },
                { MealSourceType.Custom, _scrollViewDictionary[MealSourceType.Custom].GetMealProportions() }
            };

            MealEntry currentMealEntry = new MealEntry(_date.CurrentDate, _dayTypeDropdown.DayType, _totals.TotalFat, _totals.TotalCarbs, _totals.TotalProtein, 
                _totals.TotalCalories, mealProportionsDictionary);
            GameManager.MealEntriesManager.ExportMealEntry(currentMealEntry, _date.CurrentDate);
        }

        public void Refresh() {
            foreach (var key in _scrollViewDictionary.Keys) {
                _scrollViewDictionary[key].ClearMealProportions();
            }
            _totals.ResetTotals();
            _totals.Refresh();

            MealEntry importedMealEntry = GameManager.MealEntriesManager.ImportMealEntry(_date.CurrentDate);
            if (importedMealEntry != default) {
                foreach (var key in importedMealEntry.MealProportionsDict.Keys) {
                    AbstractMealsScrollView scrollView = _scrollViewDictionary[key];
                    foreach (var mealProportion in importedMealEntry.MealProportionsDict[key]) {
                        scrollView.AddMealProportion(mealProportion);
                        _totals.AddToTotals(mealProportion);
                    }
                }
            }
            _dayTypeDropdown.HardSetDayType(importedMealEntry.DayType);
        }

        private void Start() {
            Refresh();
        }
    }
}