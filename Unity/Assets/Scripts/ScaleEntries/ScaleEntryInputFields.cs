using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryInputFields : MonoBehaviour {

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private GameObject _blank = default;

        [SerializeField]
        private Transform _contentTransform = default;

        public void ShowInputFields() {
            _blank.transform.SetParent(_contentTransform);
            _blank.SetActive(true);
            _blank.transform.SetSiblingIndex(0);
            for(int i = 0; i < _inputFields.Count; i++) {
                var inputField = _inputFields[i];
                inputField.transform.SetParent(_contentTransform);
                inputField.gameObject.SetActive(true);
                inputField.transform.SetSiblingIndex(i + 1);
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

        public ScaleEntry GetScaleEntryFromInputFields() {
            return new ScaleEntry(DateTime.Today, float.Parse(_inputFields[0].text), float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text),
                float.Parse(_inputFields[3].text), float.Parse(_inputFields[4].text), float.Parse(_inputFields[5].text));     
        }
    }
}
