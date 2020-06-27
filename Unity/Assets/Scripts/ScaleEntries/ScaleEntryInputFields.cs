using System.Linq;
using UnityEngine;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntryInputFields : InputFields {

        public ScaleEntry ScaleEntry {
            get {
                var date = _scaleEntryDateField.CurrentDate;
                var weight = float.Parse(_inputFields[0].text);
                var bodyFat = float.Parse(_inputFields[1].text);
                var bodyWater = float.Parse(_inputFields[2].text);
                var muscleMass = float.Parse(_inputFields[3].text);
                var boneMass = float.Parse(_inputFields[4].text);
                var bmi = float.Parse(_inputFields[5].text);
                return new ScaleEntry(date, weight, bodyFat, bodyWater, muscleMass, boneMass, bmi);
            }
        }

        [SerializeField]
        private ScaleEntryDateField _scaleEntryDateField = default;

        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

        public override void Show() {
            base.Show();
            _scaleEntriesScrollView.ScrollViewAssistant.AddToScrollView(_scaleEntryDateField.transform);
            _scaleEntryDateField.gameObject.SetActive(true);
            for(int i = 0; i < _inputFields.Count; i++) {
                var inputField = _inputFields[i];
                _scaleEntriesScrollView.ScrollViewAssistant.AddToScrollView(inputField.transform);
                inputField.gameObject.SetActive(true);
            }
            _inputFields.First().ActivateInputField();
            _scaleEntriesScrollView.ScrollViewAssistant.ScrollToBottom();
        }

        public override void Hide() {
            base.Hide();
            foreach (var inputField in _inputFields) {
                inputField.text = string.Empty;
                inputField.gameObject.SetActive(false);
                inputField.transform.SetParent(transform, false);
            }
            _scaleEntryDateField.gameObject.SetActive(false);
            _scaleEntryDateField.transform.SetParent(transform, false);
        }
    }
}
