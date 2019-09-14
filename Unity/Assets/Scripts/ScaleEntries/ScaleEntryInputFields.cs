using CalorieCounter.Utilities;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryInputFields : MonoBehaviour {

        [SerializeField]
        private ScaleEntryDateField _scaleEntryDateField = default;

        [SerializeField]
        private List<TMP_InputField> _inputFields = default;

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public void ShowInputFields() {
            _scaleEntriesScrollView.AddToScrollView(_scaleEntryDateField.transform);
            _scaleEntryDateField.gameObject.SetActive(true);
            _scaleEntryDateField.transform.SetAsLastSibling();
            for(int i = 0; i < _inputFields.Count; i++) {
                var inputField = _inputFields[i];
                _scaleEntriesScrollView.AddToScrollView(inputField.transform);
                inputField.gameObject.SetActive(true);
                inputField.transform.SetAsLastSibling();
            }
            _inputFields.First().ActivateInputField();
            _scaleEntriesScrollView.ScrollToBottom();
        }

        public void HideInputFields() {
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform, false);
            }
            _scaleEntryDateField.gameObject.SetActive(false);
            _scaleEntryDateField.transform.SetParent(transform, false);
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
            return new ScaleEntry(_scaleEntryDateField.CurrentDate, float.Parse(_inputFields[0].text), float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text),
                float.Parse(_inputFields[3].text), float.Parse(_inputFields[4].text), float.Parse(_inputFields[5].text));     
        }
    }
}
