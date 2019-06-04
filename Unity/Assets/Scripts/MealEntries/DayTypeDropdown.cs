using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Globals;
using System;
using System.Linq;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DayTypeDropdown : MonoBehaviour {

        public DayType DayType { get; private set; }

        private TMP_Dropdown _dropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedDayType(int index) {
            DayType = (DayType)index;
            if (DayType != DayType.None)
                FindObjectOfType<InteractableHandler>()?.SetSourceAndTargetsInteractable(gameObject);
            else
                FindObjectOfType<InteractableHandler>()?.ResetTargetsInteractable(gameObject);
        }

        public void HardSetDayType(DayType dayType) {
            _dropdown.value = (int)dayType;
            DayType = dayType;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();
            List<string> dayTypeList = Enum.GetValues(typeof(DayType)).Cast<DayType>().Select(v => v.ToString()).ToList();
            foreach (var dayType in dayTypeList) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(dayType));
            }
            _dropdown.AddOptions(_optionDataList);
            DayType = default;
        }

    }
}
