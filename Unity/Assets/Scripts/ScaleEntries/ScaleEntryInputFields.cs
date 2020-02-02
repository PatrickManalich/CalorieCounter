using System.Linq;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryInputFields : InputFields {

        [SerializeField]
        private ScaleEntryDateField _scaleEntryDateField = default;

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public override void Show() {
            base.Show();
            _scaleEntriesScrollView.AddToScrollView(_scaleEntryDateField.transform);
            _scaleEntryDateField.gameObject.SetActive(true);
            for(int i = 0; i < _inputFields.Count; i++) {
                var inputField = _inputFields[i];
                _scaleEntriesScrollView.AddToScrollView(inputField.transform);
                inputField.gameObject.SetActive(true);
            }
            _inputFields.First().ActivateInputField();
            _scaleEntriesScrollView.ScrollToBottom();
        }

        public override void Hide() {
            base.Hide();
            foreach (var inputField in _inputFields) {
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform, false);
            }
            _scaleEntryDateField.gameObject.SetActive(false);
            _scaleEntryDateField.transform.SetParent(transform, false);
        }

        public ScaleEntry GetScaleEntryFromInputFields() {
            return new ScaleEntry(_scaleEntryDateField.CurrentDate, float.Parse(_inputFields[0].text), float.Parse(_inputFields[1].text), float.Parse(_inputFields[2].text),
                float.Parse(_inputFields[3].text), float.Parse(_inputFields[4].text), float.Parse(_inputFields[5].text));     
        }
    }
}
