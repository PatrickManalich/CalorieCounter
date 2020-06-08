using UnityEngine;

namespace CalorieCounter.MealEntries
{
	public class MealEntriesSaveButton : SaveButton
	{
        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        protected override void Start()
		{
            base.Start();
            _date.CurrentDateTimeChanged += RefreshButtonInteractability;
            _dayTypeDropdown.CurrentDayTypeChanged += RefreshButtonInteractability;
        }

        protected override void OnDestroy()
		{
            _dayTypeDropdown.CurrentDayTypeChanged -= RefreshButtonInteractability;
            _date.CurrentDateTimeChanged -= RefreshButtonInteractability;
            base.OnDestroy();
		}
    }
}

