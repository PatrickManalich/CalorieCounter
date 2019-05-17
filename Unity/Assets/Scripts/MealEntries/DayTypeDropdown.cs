using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Globals;
using System;
using System.Linq;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DayTypeDropdown : MonoBehaviour {

        public DayTypes DayType { get; private set; }

        private TMP_Dropdown _dropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedDayType(int index) {
            DayType = (DayTypes)index;
        }

        public void HardSetDayType(DayTypes dayType) {
            _dropdown.value = (int)dayType;
            DayType = dayType;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();
            List<string> dayTypeList = Enum.GetValues(typeof(DayTypes)).Cast<DayTypes>().Select(v => v.ToString()).ToList();
            foreach (var dayType in dayTypeList) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(dayType));
            }
            _dropdown.AddOptions(_optionDataList);
            DayType = default;
        }

    }
}
