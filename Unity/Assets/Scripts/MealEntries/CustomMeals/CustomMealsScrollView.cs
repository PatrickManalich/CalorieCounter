using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class CustomMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private MealEntryHandler _mealEntryHandler = default;

        [SerializeField]
        private GameObject _scrollViewBlankPrefab = default;

        [SerializeField]
        private GameObject _scrollViewInputFieldPrefab = default;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        [SerializeField]
        private GameObject _deleteButtonContainerPrefab = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        private List<TMP_InputField> _inputFields = new List<TMP_InputField>();

        private List<MealProportion> _mealProportions = new List<MealProportion>();

        public void AddInputFields(Selectable lastSelectable) {
            GameObject previous = null;
            for (int i = 0; i < _content.constraintCount; i++) {
                if (i == 0) {
                    previous = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<TMP_InputField>().ActivateInputField();
                    _inputFields.Add(previous.GetComponent<TMP_InputField>());
                } else if(i >= 2 && i <= 4) {
                    GameObject current = Instantiate(_scrollViewInputFieldPrefab, _content.transform);
                    previous.GetComponent<Tabbable>().NextSelectable = current.GetComponent<Selectable>();
                    previous = current;
                    _inputFields.Add(previous.GetComponent<TMP_InputField>());
                } else {
                    Instantiate(_scrollViewBlankPrefab, _content.transform);
                }
            }
            _inputFields[_inputFields.Count - 1].GetComponent<Tabbable>().NextSelectable = lastSelectable;
        }

        public void DeleteInputFields() {
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = (_mealProportions.Count * _content.constraintCount) + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
            _inputFields.Clear();
        }

        public void AddCustomMealFromInputFields() {
            MealSource customMealSource = MealSource.CreateCustomMealSource(float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text));
            MealProportion mealProportion = new MealProportion(float.Parse(_inputFields[0].text), customMealSource);
            AddMealProportion(mealProportion);
            _mealEntryHandler.AddMealProportion(mealProportion);
        }

        public override void AddMealProportion(MealProportion mealProportion) {
            foreach (Transform child in _content.transform) {
                if (child.GetComponent<TMP_InputField>() != null) {
                    DeleteInputFields();
                    continue;
                }
            }

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
