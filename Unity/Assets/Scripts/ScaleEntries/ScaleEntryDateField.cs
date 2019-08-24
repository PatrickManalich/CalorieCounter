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

         void Start()
        {
            _oneDayBackwardButton.onClick.AddListener(() => OnDayButtonClicked(-1));
            _oneDayForwardButton.onClick.AddListener(() => OnDayButtonClicked(1));
        }

        public void OnDayButtonClicked(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
            Refresh();
        }

        private void Awake()
        {
            Refresh();
        }

        private void Refresh()
        {
            _currentDateText.text = CurrentDate.ToShortDateString();
        }
    }
}
