using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.ScaleEntries
{

    public class ScaleEntryDateField : MonoBehaviour
    {
        public DateTime CurrentDate { get; private set; } = DateTime.Today;

        [SerializeField]
        private Button _oneDayBackwardButton = default;

        [SerializeField]
        private TextMeshProUGUI _currentDateText = default;

        [SerializeField]
        private Button _oneDayForwardButton = default;

        private void Start()
        {
            _oneDayBackwardButton.onClick.AddListener(OneDayBackwardButton_OnClick);
            _oneDayForwardButton.onClick.AddListener(OneDayForwardButton_OnClick);

            Refresh();
        }

        private void OnDestroy()
        {
            _oneDayForwardButton.onClick.RemoveListener(OneDayForwardButton_OnClick);
            _oneDayBackwardButton.onClick.RemoveListener(OneDayBackwardButton_OnClick);
        }

        private void OneDayBackwardButton_OnClick()
        {
            AddDays(-1);
        }

        private void OneDayForwardButton_OnClick()
        {
            AddDays(1);
        }

        private void AddDays(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
            Refresh();
        }

        private void Refresh()
        {
            _currentDateText.text = CurrentDate.ToShortDateString();
        }
    }
}
