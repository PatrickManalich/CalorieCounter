using CalorieCounter.MealEntries;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealSources {

    public class CustomMealsScrollView : AbstractMealsScrollView {

        public override event TextAddedEventHandler TextAddedEvent;

        protected override GridLayoutGroup Content { get { return _content; } }

        protected override ScrollViewRowHighlighter ScrollViewRowHighlighter { get { return _scrollViewRowHighlighter; } }

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private CustomMealProportionInputFields _customMealInputFields = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private ScrollViewRowHighlighter _scrollViewRowHighlighter = default;

        public List<MealProportion> _mealProportions { get; private set; } = new List<MealProportion>();

        public void AddCustomMealProportionFromInputFields() {
            MealProportion mealProportion = _customMealInputFields.GetCustomMealProportionFromInputFields();
            AddMealProportion(mealProportion);
            _totals.AddToTotals(mealProportion);
        }

        public override List<MealProportion> GetMealProportions()
        {
            return _mealProportions;
        }
        public override void AddMealProportion(MealProportion mealProportion) {
            GameObject servingAmountText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject nameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);

            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(servingAmountText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(nameText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(fatText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(carbText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(proteinText.GetComponent<ScrollViewText>()));
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(calorieText.GetComponent<ScrollViewText>()));

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.ServingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = mealProportion.MealSource.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.Calories.ToString();

            _mealProportions.Add(mealProportion);
        }

        public override void SubtractMealProportion(MealProportion mealProportion) {
            int mealProportionIndex = _mealProportions.FindIndex(x => x == mealProportion);
            _mealProportions.RemoveAt(mealProportionIndex);

            int childStartIndex = mealProportionIndex * _content.constraintCount;
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = childStartIndex + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
        }

        public override void ClearMealProportions() {
            foreach (Transform child in _content.transform) {
                Destroy(child.gameObject);
            }
            _mealProportions.Clear();
        }

        protected override void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e)
        {
            var destroyedMealProportion = _mealProportions[e.DestroyedRowIndex];
            SubtractMealProportion(destroyedMealProportion);
            _totals.RemoveFromTotals(destroyedMealProportion);
        }
    }
}
