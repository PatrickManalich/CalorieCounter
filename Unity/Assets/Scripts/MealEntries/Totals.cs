using CalorieCounter.TargetEntries;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class Totals : MonoBehaviour {

        public event EventHandler TargetAttained;

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
        private MealProportionsScrollViewDictionary _mealProportionsScrollViewDictionary = default;

        public float TotalFat { get; private set; } = 0;
        public float TotalCarbs { get; private set; } = 0;
        public float TotalProtein { get; private set; } = 0;
        public float TotalCalories { get; private set; } = 0;

        private TargetEntry _targetEntry;
        private Color _originalColor;
        private Dictionary<TextMeshProUGUI, bool> _targetsAttainedDictionary = new Dictionary<TextMeshProUGUI, bool>();

        private void Start()
        {
            _originalColor = _fatTotalText.color;

            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_CurrentDayTypeChanged;
            foreach (var scrollView in _mealProportionsScrollViewDictionary.Values)
            {
                scrollView.MealProportionModified += ScrollView_OnMealProportionModified;
            }

            foreach (var scrollView in _mealProportionsScrollViewDictionary.Values)
            {
                foreach(var mealProportion in scrollView.MealProportions)
                {
                    TotalFat += mealProportion.Fat;
                    TotalCarbs += mealProportion.Carbs;
                    TotalProtein += mealProportion.Protein;
                    TotalCalories += mealProportion.Calories;
                }
            }

            _targetsAttainedDictionary.Add(_fatTotalText, false);
            _targetsAttainedDictionary.Add(_carbsTotalText, false);
            _targetsAttainedDictionary.Add(_proteinTotalText, false);
            _targetsAttainedDictionary.Add(_caloriesTotalText, false);
            RefreshTargetEntry();
        }

        private void OnDestroy()
        {
            foreach (var scrollView in _mealProportionsScrollViewDictionary.Values)
            {
                scrollView.MealProportionModified -= ScrollView_OnMealProportionModified;
            }
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_CurrentDayTypeChanged;
        }

        private void DayTypeDropdown_CurrentDayTypeChanged(object sender, System.EventArgs e)
        {
            RefreshTargetEntry();
        }

        private void ScrollView_OnMealProportionModified(object sender, MealProportionsScrollView.MealProportionModifiedEventArgs e)
        {
            if(e.MealProportionModifiedType == MealProportionModifiedType.Added)
            {
                TotalFat += e.MealProportion.Fat;
                TotalCarbs += e.MealProportion.Carbs;
                TotalProtein += e.MealProportion.Protein;
                TotalCalories += e.MealProportion.Calories;
                RefreshTexts();
            }
            else if(e.MealProportionModifiedType == MealProportionModifiedType.Removed)
            {
                TotalFat -= e.MealProportion.Fat;
                TotalCarbs -= e.MealProportion.Carbs;
                TotalProtein -= e.MealProportion.Protein;
                TotalCalories -= e.MealProportion.Calories;
                RefreshTexts();
            }
        }

        private void RefreshTargetEntry()
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
                targetEntryFat = _targetEntry.RestDayFat;
                targetEntryCarbs = _targetEntry.RestDayCarbs;
                targetEntryProtein = _targetEntry.RestDayProtein;
                targetEntryCalories = _targetEntry.RestDayCalories;
            } else if (_dayTypeDropdown.CurrentDayType == DayType.Training) {
                targetEntryFat = _targetEntry.TrainingDayFat;
                targetEntryCarbs = _targetEntry.TrainingDayCarbs;
                targetEntryProtein = _targetEntry.TrainingDayProtein;
                targetEntryCalories = _targetEntry.TrainingDayCalories;
            }

            RefreshText(_fatTotalText, TotalFat, targetEntryFat);
            RefreshText(_carbsTotalText, TotalCarbs, targetEntryCarbs);
            RefreshText(_proteinTotalText, TotalProtein, targetEntryProtein);
            RefreshText(_caloriesTotalText, TotalCalories, targetEntryCalories);

        }

        private void RefreshText(TextMeshProUGUI text, float total, float target) {
            if (target == 0)
            {
                text.color = _originalColor;
            }
            else if(total < target)
            {
                text.color = _belowTargetColor;
                if (_targetsAttainedDictionary[text])
                {
                    _targetsAttainedDictionary[text] = false;
                }
            }
            else
            {
                text.color = _aboveTargetColor;
                if (!_targetsAttainedDictionary[text])
                {
                    _targetsAttainedDictionary[text] = true;
                    TargetAttained?.Invoke(this, EventArgs.Empty);
                }
            }
            text.text = total + " / " + target;
        }
    }
}
