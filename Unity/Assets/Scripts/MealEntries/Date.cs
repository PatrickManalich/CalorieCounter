using System;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Date : MonoBehaviour {

        public class DateChangedEvent : EventArgs
        {
            public DateTime CurrentDateTime { get; private set; }

            public DateChangedEvent(DateTime currentDateTime)
            {
                CurrentDateTime = currentDateTime;
            }
        }

        public delegate void DateChangedEventHandler(object sender, DateChangedEvent e);
        public event DateChangedEventHandler DateChanged;

        public DateTime CurrentDateTime { get; private set; } = DateTime.Today;

        private TextMeshProUGUI _text;

        public void MoveBackwardOneDay() {
            CurrentDateTime = CurrentDateTime.AddDays(-1);
            Refresh();
        }

        public void MoveForwardOneDay() {
            CurrentDateTime = CurrentDateTime.AddDays(1);
            Refresh();
        }

        public void MoveBackwardOneMonth() {
            CurrentDateTime = CurrentDateTime.AddMonths(-1);
            Refresh();
        }

        public void MoveForwardOneMonth() {
            CurrentDateTime = CurrentDateTime.AddMonths(1);
            Refresh();
        }

        private void Awake() {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            Refresh();
        }

        private void Refresh() {
            _text.text = CurrentDateTime.ToShortDateString();
            DateChanged?.Invoke(this, new DateChangedEvent(CurrentDateTime));
        }

    }

}
