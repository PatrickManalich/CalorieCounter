using System;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Date : MonoBehaviour {

        private TextMeshProUGUI _text;

        public DateTime CurrentDate { get; private set; } = DateTime.Today;

        public void MoveBackward() {
            CurrentDate = CurrentDate.AddDays(-1);
            RefreshText();
        }

        public void MoveForward() {
            CurrentDate = CurrentDate.AddDays(1);
            RefreshText();
        }

        private void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
            RefreshText();
        }

        private void RefreshText() {
            _text.text = CurrentDate.ToShortDateString();
        }

    }

}
