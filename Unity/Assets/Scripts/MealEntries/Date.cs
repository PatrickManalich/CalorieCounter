using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries {

    public class Date : MonoBehaviour {

        public event EventHandler CurrentDateTimeChanged;

        public DateTime CurrentDateTime {
            get {
                return _currentDateTime;
            }
            private set {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    Refresh();
                    CurrentDateTimeChanged?.Invoke(this, EventArgs.Empty);
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

        public void ResetDate()
        {
            CurrentDateTime = DateTime.Today;
        }

        private void Start()
        {
            _oneMonthBackwardButton.onClick.AddListener(OneMonthBackwardButton_OnClick);
            _oneDayBackwardButton.onClick.AddListener(OneDayBackwardButton_OnClick);
            _oneDayForwardButton.onClick.AddListener(OneDayForwardButton_OnClick);
            _oneMonthForwardButton.onClick.AddListener(OneMonthForwardButton_OnClick);
            Refresh();
        }

        private void OnDestroy()
        {
            _oneMonthForwardButton.onClick.RemoveListener(OneMonthForwardButton_OnClick);
            _oneDayForwardButton.onClick.RemoveListener(OneDayForwardButton_OnClick);
            _oneDayBackwardButton.onClick.RemoveListener(OneDayBackwardButton_OnClick);
            _oneMonthBackwardButton.onClick.RemoveListener(OneMonthBackwardButton_OnClick);
        }

        private void OneMonthBackwardButton_OnClick()
        {
            CurrentDateTime = CurrentDateTime.AddMonths(-1);
        }

        private void OneDayBackwardButton_OnClick()
        {
            CurrentDateTime = CurrentDateTime.AddDays(-1);
        }

        private void OneDayForwardButton_OnClick()
        {
            CurrentDateTime = CurrentDateTime.AddDays(1);
        }

        private void OneMonthForwardButton_OnClick()
        {
            CurrentDateTime = CurrentDateTime.AddMonths(1);
        }

        private void Refresh()
        {
            _text.text = $"{CurrentDateTime.DayOfWeek.ToString()}, {CurrentDateTime.ToShortDateString()}";
        }
    }
}
