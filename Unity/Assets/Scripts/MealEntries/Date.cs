using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries {

    public class Date : MonoBehaviour {

        public Action CurrentDateTimeChanged;

        public DateTime CurrentDateTime {
            get {
                return _currentDateTime;
            }
            private set {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    CurrentDateTimeChanged?.Invoke();
                }
            }
        }

        [SerializeField]
        private Button _oneMonthBackwardButton = default;

        [SerializeField]
        private Button _oneDayBackwardButton = default;

        [SerializeField]
        private TextMeshProUGUI _text = default;

        [SerializeField]
        private Button _oneDayForwardButton = default;

        [SerializeField]
        private Button _oneMonthForwardButton = default;

        private DateTime _currentDateTime = DateTime.Today;

        private void Start()
        {
            _oneMonthBackwardButton.onClick.AddListener(() => AddMonths(-1));
            _oneDayBackwardButton.onClick.AddListener(() => AddDays(-1));
            _oneDayForwardButton.onClick.AddListener(() => AddDays(1));
            _oneMonthForwardButton.onClick.AddListener(() => AddMonths(1));
            Refresh();
        }

        private void OnDestroy()
        {
            _oneMonthForwardButton.onClick.RemoveListener(() => AddMonths(1));
            _oneDayForwardButton.onClick.RemoveListener(() => AddDays(1));
            _oneDayBackwardButton.onClick.RemoveListener(() => AddDays(-1));
            _oneMonthBackwardButton.onClick.RemoveListener(() => AddMonths(-1));
        }

        private void AddDays(int days)
        {
            CurrentDateTime = CurrentDateTime.AddDays(days);
            Refresh();
        }

        private void AddMonths(int months)
        {
            CurrentDateTime = CurrentDateTime.AddMonths(months);
            Refresh();
        }

        private void Refresh()
        {
            _text.text = $"{CurrentDateTime.DayOfWeek.ToString()}, {CurrentDateTime.ToShortDateString()}";
        }
    }
}
