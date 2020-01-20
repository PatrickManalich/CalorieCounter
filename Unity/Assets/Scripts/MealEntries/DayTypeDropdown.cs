using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Utilities;
using System;
using System.Linq;
using CalorieCounter.MealEntries.MealPatterns;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class DayTypeDropdown : MonoBehaviour {

        public DayType DayType { get; private set; }

        [SerializeField]
        private Totals _totals = default;

        [SerializeField]
        private MealPatternHandler _mealPatternHandler = default;

        private TMP_Dropdown _dropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void HardSetDayType(DayType dayType) {
            _dropdown.value = (int)dayType;
            DayType = dayType;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.ClearOptions();
            List<string> dayTypeList = Enum.GetValues(typeof(DayType)).Cast<DayType>().Select(v => v.ToString()).ToList();
            foreach (var dayType in dayTypeList) {
                if (dayType == DayType.None.ToString()) {
                    _optionDataList.Add(new TMP_Dropdown.OptionData(""));
                } else {
                    _optionDataList.Add(new TMP_Dropdown.OptionData(dayType));
                }
            }
            _dropdown.AddOptions(_optionDataList);
            DayType = default;
            _dropdown.onValueChanged.AddListener(i => Refresh(i));
        }

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(i => Refresh(i));
        }

        public void Refresh(int index)
        {
            DayType = (DayType)index;
            if (DayType != DayType.None)
            {
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
                _mealPatternHandler.AddMealSuggestions();
            }
            else
            {
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
            _totals.Refresh();
        }

    }
}
