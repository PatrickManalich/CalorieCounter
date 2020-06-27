using CalorieCounter.TargetEntries;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class Totals : MonoBehaviour {

        [Serializable]
        private class ScrollViewDictionary : SerializableDictionaryBase<MealSourceType, MealProportionsScrollView> { }

        [SerializeField]
        private TargetEntriesAdapter _targetEntriesAdapter = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        [SerializeField]
        private TextMeshProUGUI _fatTotalText = default;

        [SerializeField]
        private TextMeshProUGUI _carbsTotalText = default;

        [SerializeField]
        private TextMeshProUGUI _proteinTotalText = default;

        [SerializeField]
        private TextMeshProUGUI _caloriesTotalText = default;

        [SerializeField]
        private Color _aboveTargetColor = default;

        [SerializeField]
        private Color _belowTargetColor = default;

        [SerializeField]
        private ScrollViewDictionary _scrollViewDictionary = default;

        public float TotalFat { get; private set; } = 0;
        public float TotalCarbs { get; private set; } = 0;
        public float TotalProtein { get; private set; } = 0;
        public float TotalCalories { get; private set; } = 0;

        private TargetEntry _targetEntry;
        private Color _originalColor;

        private void Start()
        {
            _originalColor = _fatTotalText.color;

            _dayTypeDropdown.CurrentDayTypeChanged += Refresh;
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealProportionModified += ScrollView_OnMealProportionModified;
            }

            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                foreach(var mealProportion in scrollView.MealProportions)
                {
                    TotalFat += mealProportion.fat;
                    TotalCarbs += mealProportion.carbs;
                    TotalProtein += mealProportion.protein;
                    TotalCalories += mealProportion.calories;
                }
            }

            Refresh();
        }

        private void OnDestroy()
        {
            foreach (var scrollView in _scrollViewDictionary.Values)
            {
                scrollView.MealProportionModified -= ScrollView_OnMealProportionModified;
            }
            _dayTypeDropdown.CurrentDayTypeChanged -= Refresh;
        }

        private void ScrollView_OnMealProportionModified(object sender, MealProportionsScrollView.MealProportionModifiedEventArgs e)
        {
            if(e.MealProportionModifiedType == MealProportionModifiedType.Added)
            {
                TotalFat += e.MealProportion.fat;
                TotalCarbs += e.MealProportion.carbs;
                TotalProtein += e.MealProportion.protein;
                TotalCalories += e.MealProportion.calories;
                RefreshTexts();
            }
            else if(e.MealProportionModifiedType == MealProportionModifiedType.Removed)
            {
                TotalFat -= e.MealProportion.fat;
                TotalCarbs -= e.MealProportion.carbs;
                TotalProtein -= e.MealProportion.protein;
                TotalCalories -= e.MealProportion.calories;
                RefreshTexts();
            }
        }

        private void Refresh()
        {
            _targetEntry = _targetEntriesAdapter.GetLatestTargetEntry();
            RefreshTexts();
        }

        private void RefreshTexts() {
            TotalFat = GlobalMethods.Round(TotalFat);
            TotalCarbs = GlobalMethods.Round(TotalCarbs);
            TotalProtein = GlobalMethods.Round(TotalProtein);
            TotalCalories = GlobalMethods.Round(TotalCalories);

            float targetEntryFat = 0;
            float targetEntryCarbs = 0;
            float targetEntryProtein = 0;
            float targetEntryCalories = 0;
            if (_dayTypeDropdown.CurrentDayType == DayType.Rest) {
                targetEntryFat = _targetEntry.restDayFat;
                targetEntryCarbs = _targetEntry.restDayCarbs;
                targetEntryProtein = _targetEntry.restDayProtein;
                targetEntryCalories = _targetEntry.restDayCalories;
            } else if (_dayTypeDropdown.CurrentDayType == DayType.Training) {
                targetEntryFat = _targetEntry.trainingDayFat;
                targetEntryCarbs = _targetEntry.trainingDayCarbs;
                targetEntryProtein = _targetEntry.trainingDayProtein;
                targetEntryCalories = _targetEntry.trainingDayCalories;
            }

            RefreshText(_fatTotalText, TotalFat, targetEntryFat);
            RefreshText(_carbsTotalText, TotalCarbs, targetEntryCarbs);
            RefreshText(_proteinTotalText, TotalProtein, targetEntryProtein);
            RefreshText(_caloriesTotalText, TotalCalories, targetEntryCalories);

        }

        private void RefreshText(TextMeshProUGUI text, float total, float target) {
            if (target == 0) {
                text.color = _originalColor;
            } else if(total < target) {
                text.color = _belowTargetColor;
            } else {
                text.color = _aboveTargetColor;
            }
            text.text = total + " / " + target;
        }
    }
}
