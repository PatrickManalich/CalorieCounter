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
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_OnCurrentDayTypeChanged;
        }

        protected override void OnDestroy()
		{
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_OnCurrentDayTypeChanged;
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
            base.OnDestroy();
		}
        private void Date_OnCurrentDateTimeChanged(object sender, System.EventArgs e)
        {
            RefreshButtonInteractability();
        }

        private void DayTypeDropdown_OnCurrentDayTypeChanged(object sender, System.EventArgs e)
        {
            RefreshButtonInteractability();
        }
    }
}

