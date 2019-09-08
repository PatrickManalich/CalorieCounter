using CalorieCounter.MealSources;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class CommonMealsScrollView : AbstractMealsScrollView {

        public override event TextAddedEventHandler TextAddedEvent;

        protected override ScrollViewRowHighlighter ScrollViewRowHighlighter { get { return _scrollViewRowHighlighter; } }

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default; 

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private NamedMealSourceDropdown _namedMealSourceDropdown = default;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private ScrollViewRowHighlighter _scrollViewRowHighlighter = default;

        private List<MealProportion> _mealProportions = new List<MealProportion>();

        public void AddCommonMealProportionFromDropdowns() {
            var mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, _namedMealSourceDropdown.SelectedNamedMealSource.mealSource);
            AddMealProportion(mealProportion);
            _totals.AddToTotals(mealProportion);
        }
        public override List<MealProportion> GetMealProportions()
        {
            return _mealProportions;
        }

        public override void AddMealProportion(MealProportion mealProportion) {
            _mealProportions.Add(mealProportion);

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

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.servingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = _mealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
            fatText.GetComponent<TextMeshProUGUI>().text = mealProportion.fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = mealProportion.carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = mealProportion.protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = mealProportion.calories.ToString();

            ScrollToBottom();
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
            foreach(Transform child in _content.transform) {
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
