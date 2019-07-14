using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourceInputFields : MonoBehaviour {

        [SerializeField]
        private MealSourceType MealSourceType = default;

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private GameObject _blank = default;

        [SerializeField]
        private Transform _contentTransform = default;

        private static readonly char[] ValidSpecialChars = new char[]
        {
                    '-', '\'', '&', '.', ' ', '/', '%',
        };

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count + 1; i++) {
                if (i >= 0 && i <= 4 || i == 6) {
                    TMP_InputField inputField = i >= 0 && i <= 4 ? _inputFields[i] : _inputFields[i - 1];
                    inputField.transform.SetParent(_contentTransform);
                    inputField.gameObject.SetActive(true);
                    inputField.transform.SetSiblingIndex(i);
                } else {
                    _blank.transform.SetParent(_contentTransform);
                    _blank.SetActive(true);
                    _blank.transform.SetSiblingIndex(i);
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

        public MealSource GetMealSourceFromInputFields() {
            return  new MealSource(_inputFields[0].text, _inputFields[1].text, float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text),
                float.Parse(_inputFields[4].text), _inputFields[5].text, MealSourceType);
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
