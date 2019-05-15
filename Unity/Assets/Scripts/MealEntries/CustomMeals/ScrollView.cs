using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class ScrollView : MonoBehaviour {

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

        public void AddInputFields() {
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
        }

        public void DeleteInputFields() {
            for (int i = 0; i < _content.constraintCount; i++) {
                int childIndex = (_mealProportions.Count * _content.constraintCount) + i;
                Destroy(_content.transform.GetChild(childIndex).gameObject);
            }
            _inputFields.Clear();
        }

        public MealProportion GetCustomMealProportionFromInputFields() {
            MealSource customMealSource = MealSource.CreateCustomMealSource(float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text));
            return new MealProportion(float.Parse(_inputFields[0].text), customMealSource);
        }

        public void AddMealProportion(MealProportion customMealProportion) {
            DeleteInputFields();

            GameObject servingAmountText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject nameText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject fatText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject carbText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject proteinText = Instantiate(_scrollViewTextPrefab, _content.transform);
            GameObject calorieText = Instantiate(_scrollViewTextPrefab, _content.transform);

            DeleteButton deleteButton = Instantiate(_deleteButtonContainerPrefab, _content.transform).GetComponentInChildren<DeleteButton>();
            deleteButton.RemovableGameObjects.InsertRange(0, new List<GameObject> { servingAmountText, nameText, fatText, carbText, proteinText, calorieText });
            deleteButton.MealProportion = customMealProportion;

            servingAmountText.GetComponent<TextMeshProUGUI>().text = customMealProportion.ServingAmount.ToString();
            nameText.GetComponent<TextMeshProUGUI>().text = customMealProportion.Source.Name;
            fatText.GetComponent<TextMeshProUGUI>().text = customMealProportion.Fat.ToString();
            carbText.GetComponent<TextMeshProUGUI>().text = customMealProportion.Carbs.ToString();
            proteinText.GetComponent<TextMeshProUGUI>().text = customMealProportion.Protein.ToString();
            calorieText.GetComponent<TextMeshProUGUI>().text = customMealProportion.Calories.ToString();

            _mealProportions.Add(customMealProportion);
        }

        public bool HasInputFields() {
            foreach (Transform child in _content.transform) {
                if (child.GetComponent<TMP_InputField>() != null) {
                    return true;
                }
            }
            return false;
        }

        public bool AllInputFieldsFilled() {
            foreach (var inputField in _inputFields) {
                if (inputField.text == "") {
                    return false;
                }
            }
            return true;
        }
    }
}
