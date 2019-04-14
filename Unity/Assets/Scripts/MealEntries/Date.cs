using System;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Date : MonoBehaviour {

        private TextMeshProUGUI _text;

        private DateTime _date = DateTime.Today;

        public void MoveBackward() {
            _date = _date.AddDays(-1);
            RefreshText();
        }

        public void MoveForward() {
            _date = _date.AddDays(1);
            RefreshText();
        }

        private void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
            RefreshText();
        }

        private void RefreshText() {
            _text.text = _date.ToShortDateString();
        }

    }

}
