using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources {

    public class MealSourceInputFields : MonoBehaviour {

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
    }
}
