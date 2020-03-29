using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Utilities;
using System;
using System.Linq;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DayTypeDropdown : MonoBehaviour {

        public Action CurrentDayTypeChanged;

        public DayType CurrentDayType {
            get {
                return _dayType;
            }
            private set {
                if (_dayType != value)
                {
                    _dayType = value;
                    CurrentDayTypeChanged?.Invoke();
                }
            }
        }

        public bool IsCurrentDayTypeRestOrTraining => CurrentDayType == DayType.Rest || CurrentDayType == DayType.Training;

        [SerializeField]
        private MealEntriesAdapter _mealEntriesAdapter = default;

        [SerializeField]
        private Date _date = default;

        private DayType _dayType;
        private TMP_Dropdown _dropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.ClearOptions();
            List<string> dayTypeList = Enum.GetValues(typeof(DayType)).Cast<DayType>().Select(v => v.ToString()).ToList();
            foreach (var dayType in dayTypeList)
            {
                if (dayType == DayType.None.ToString())
                {
                    _optionDataList.Add(new TMP_Dropdown.OptionData(""));
                }
                else
                {
                    _optionDataList.Add(new TMP_Dropdown.OptionData(dayType));
                }
            }
            _dropdown.AddOptions(_optionDataList);
            Refresh();
        }

        private void Start() {
            _dropdown.onValueChanged.AddListener(i => Dropdown_OnValueChanged(i));
            _date.CurrentDateTimeChanged += Refresh;
        }

        private void OnDestroy()
        {
            _date.CurrentDateTimeChanged += Refresh;
            _dropdown.onValueChanged.RemoveListener(i => Dropdown_OnValueChanged(i));
        }

        public void Dropdown_OnValueChanged(int index)
        {
            CurrentDayType = (DayType)index;
            if (IsCurrentDayTypeRestOrTraining)
            {
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            }
            else
            {
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }

        private void Refresh()
        {
            var currentDayType = _mealEntriesAdapter.GetMealEntryDayType();
            _dropdown.value = (int)currentDayType;
            CurrentDayType = currentDayType;
        }
    }
}
