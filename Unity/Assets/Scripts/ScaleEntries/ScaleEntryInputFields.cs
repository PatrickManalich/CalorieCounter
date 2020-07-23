using System.Collections.Generic;
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
            var transforms = new List<Transform>
            {
                _scaleEntryDateField.transform,
            };
            transforms.AddRange(_inputFields.Select(i => i.transform));
            _scaleEntriesScrollView.AddToScrollView(transforms);
            _inputFields.First().ActivateInputField();
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
