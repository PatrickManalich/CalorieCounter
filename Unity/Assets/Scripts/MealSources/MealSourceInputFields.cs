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
        private Transform _contentTransform = default;

        public void ShowInputFields() {
            for (int i = 0; i < _inputFields.Count; i++) {
                TMP_InputField inputField = _inputFields[i];
                inputField.transform.SetParent(_contentTransform);
                inputField.gameObject.SetActive(true);
                inputField.transform.SetSiblingIndex(i);
            }
            _inputFields.First().ActivateInputField();
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform);
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

        public MealSource GetMealSourceFromInputFields() {
            return  new MealSource(_inputFields[0].text, _inputFields[1].text, float.Parse(_inputFields[2].text), float.Parse(_inputFields[3].text),
                float.Parse(_inputFields[4].text), _inputFields[5].text, MealSourceType);
        }
    }
}
