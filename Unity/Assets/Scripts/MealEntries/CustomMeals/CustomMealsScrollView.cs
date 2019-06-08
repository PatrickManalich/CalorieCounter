using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class CustomMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GameObject _deleteButtonContainerPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private CustomMealInputFields _customMealInputFields = default;

        private List<MealProportion> _mealProportions = new List<MealProportion>();

        public void AddCustomMealProportionFromInputFields() {
            MealProportion mealProportion = _customMealInputFields.GetCustomMealProportionFromInputFields();
            AddMealProportion(mealProportion);
            _mealEntryHandler.AddMealProportion(mealProportion);
        }

        public override void AddMealProportion(MealProportion mealProportion) {
            GameObject servingAmountText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject nameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);

            Button deleteButton = Instantiate(_deleteButtonContainerPrefab, _content.transform).GetComponentInChildren<Button>();
            deleteButton.onClick.AddListener(delegate { SubtractMealProportion(mealProportion); });
            deleteButton.onClick.AddListener(delegate { _mealEntryHandler.SubtractMealProportion(mealProportion); });

            servingAmountText.GetComponent<TextMeshProUGUI>().text = mealProportion.ServingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = mealProportion.Source.Name;
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
    }
}
