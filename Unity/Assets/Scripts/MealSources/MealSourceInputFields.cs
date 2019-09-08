using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourceInputFields : MonoBehaviour {

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private GameObject _blank = default;

        [SerializeField]
        private MealSourcesScrollView _mealSourcesScrollView = default;

        private static readonly char[] ValidSpecialChars = new char[]
        {
                    '-', '\'', '&', '.', ' ', '/', '%',
        };

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count + 1; i++) {
                if (i == 5) {
                    _mealSourcesScrollView.AddToScrollView(_blank.transform);
                    _blank.SetActive(true);
                    _blank.transform.SetAsLastSibling();
                } else {
                    TMP_InputField inputField = i == 6 ? _inputFields[i - 1] : _inputFields[i];
                    _mealSourcesScrollView.AddToScrollView(inputField.transform);
                    inputField.gameObject.SetActive(true);
                    inputField.transform.SetAsLastSibling();
                }
            }
            _inputFields.First().ActivateInputField();
            _mealSourcesScrollView.ScrollToBottom();
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform);
            }
            _blank.SetActive(false);
            _blank.transform.SetParent(transform);
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

        public NamedMealSource GetNamedMealSourceFromInputFields() {
            var mealSourceCount = _mealSourcesScrollView.MealSources.Count;
            var name = _inputFields[0].text;
            var servingSize = _inputFields[1].text;
            var fat = float.Parse(_inputFields[2].text);
            var carbs = float.Parse(_inputFields[3].text);
            var protein = float.Parse(_inputFields[4].text);
            var description = _inputFields[5].text;
            var mealSource = new MealSource(mealSourceCount, servingSize, fat, carbs, protein, description, _mealSourceType);
            return new NamedMealSource(name, mealSource);
        }

        private void Awake()
        {
            for (int i = 0; i < _inputFields.Count; i++) {
                if (i == 0 || i == 1 || i == 5)
                {
                    _inputFields[i].onValidateInput = ValidateNonDecimalInput;
                }
            }
        }

        private static char ValidateNonDecimalInput(string text, int charIndex, char addedChar)
        {
            return char.IsLetterOrDigit(addedChar) || ValidSpecialChars.Contains(addedChar) ? addedChar : '\0';
        }
    }
}
