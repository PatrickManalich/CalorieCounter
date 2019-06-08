using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class CustomMealInputFields : MonoBehaviour {

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private List<GameObject> _blanks = default;

        [SerializeField]
        private Transform _contentTransform = default;

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count + _blanks.Count; i++) {
                if (i == 0 || i >= 2 && i <= 4) {
                    TMP_InputField inputField = i == 0 ? _inputFields[i] : _inputFields[i - 1];
                    inputField.transform.SetParent(_contentTransform);
                    inputField.gameObject.SetActive(true);
                    inputField.transform.SetSiblingIndex(i);
                } else {
                    GameObject blank = i == 1 ? _blanks[i - 1] : _blanks[i - 4];
                    blank.transform.SetParent(_contentTransform);
                    blank.SetActive(true);
                    blank.transform.SetSiblingIndex(i);
                }
            }
            _inputFields.First().ActivateInputField();
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform);
            }
            foreach (var blank in _blanks) {
                blank.SetActive(false);
                blank.transform.SetParent(transform);
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
            MealSource customMealSource = MealSource.CreateCustomMealSource(float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text));
            return new MealProportion(float.Parse(_inputFields[0].text), customMealSource);
        }
    }
}
