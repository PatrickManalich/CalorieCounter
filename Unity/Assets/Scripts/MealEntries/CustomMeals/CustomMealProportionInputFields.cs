using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class CustomMealProportionInputFields : MonoBehaviour {

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private List<GameObject> _blanks = default;

        [SerializeField]
        private CustomMealsScrollView _customMealScrollView = default;

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count + _blanks.Count; i++) {
                if (i == 0 || i >= 2 && i <= 4) {
                    TMP_InputField inputField = i == 0 ? _inputFields[i] : _inputFields[i - 1];
                    _customMealScrollView.AddToScrollView(inputField.transform);
                    inputField.gameObject.SetActive(true);
                } else {
                    GameObject blank = i == 1 ? _blanks[i - 1] : _blanks[i - 4];
                    _customMealScrollView.AddToScrollView(blank.transform);
                    blank.SetActive(true);
                }
            }
            _inputFields.First().ActivateInputField();
            _customMealScrollView.ScrollToBottom();
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform, false);
            }
            foreach (var blank in _blanks) {
                blank.SetActive(false);
                blank.transform.SetParent(transform, false);
            }
        }

        public void CheckIfInputFieldsAreFilled() {
            foreach (var inputField in _inputFields) {
                if (inputField.text == "") {
                    FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
                    return;
                }
            }
            FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
        }

        public MealProportion GetCustomMealProportionFromInputFields() {
            var servingAmount = float.Parse(_inputFields[0].text);
            var fat = float.Parse(_inputFields[1].text);
            var carbs = float.Parse(_inputFields[2].text);
            var protein = float.Parse(_inputFields[3].text);
            MealSource customMealSource = MealSource.CreateCustomMealSource(fat, carbs, protein);
            return new MealProportion(servingAmount, customMealSource);
        }
    }
}
