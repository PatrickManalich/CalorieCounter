using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{
	public class VacationDayCreator : MonoBehaviour
	{
        public event EventHandler VacationDaysCreated;

        [SerializeField]
        private MealEntriesAdapter _mealEntriesAdapter = default;

        [SerializeField]
        private GameObject _canvas = default;

        [SerializeField]
        private Date _startDate = default;

        [SerializeField]
        private Date _endDate = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private Button _cancelButton = default;

        public void Show()
        {
            _startDate.ResetDate();
            _endDate.ResetDate();
            _submitButton.interactable = false;
            _cancelButton.interactable = true;
            _canvas.SetActive(true);
        }

        private void Start()
        {
            _startDate.CurrentDateTimeChanged += RefreshSubmitButtonInteractability;
            _endDate.CurrentDateTimeChanged += RefreshSubmitButtonInteractability;
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _cancelButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveListener(Hide);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _endDate.CurrentDateTimeChanged -= RefreshSubmitButtonInteractability;
            _startDate.CurrentDateTimeChanged -= RefreshSubmitButtonInteractability;
        }

        private void RefreshSubmitButtonInteractability(object sender, EventArgs e)
        {
            var totalDaysGreaterThanZero = (_endDate.CurrentDateTime - _startDate.CurrentDateTime).TotalDays > 0;
            var startDateBeforeEndDate = _startDate.CurrentDateTime < _endDate.CurrentDateTime;
            _submitButton.interactable = totalDaysGreaterThanZero && startDateBeforeEndDate;
        }

        private void SubmitButton_OnClick()
        {
            var emptyMealProportionsDictionary = new Dictionary<MealSourceType, List<MealProportion>>
            {
                { MealSourceType.Small, new List<MealProportion>() },
                { MealSourceType.Large, new List<MealProportion>() },
                { MealSourceType.Custom, new List<MealProportion>() },
            };

            var dateTime = _startDate.CurrentDateTime;
            while(dateTime <= _endDate.CurrentDateTime)
            {
                var mealEntry = new MealEntry(dateTime, DayType.Vacation, emptyMealProportionsDictionary);
                _mealEntriesAdapter.Export(mealEntry, dateTime);
                dateTime = dateTime.AddDays(1);
            }
            VacationDaysCreated?.Invoke(this, EventArgs.Empty);
            Hide();
        }

        private void Hide()
        {
            _canvas.SetActive(false);
        }
    }
}

