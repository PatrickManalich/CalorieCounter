using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class ServingAmountDropdown : MonoBehaviour {

        public Action ValidityChanged;

        public bool IsValid => SelectedServingAmount != default;

        public float SelectedServingAmount { get; private set; }
        
        private static readonly float[] ServingAmounts = new float[] { 0.1f, 0.25f, 0.33f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5, 6 };

        private TMP_Dropdown _dropdown;

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedServingAmount = default;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData("") };
            foreach (var servingAmount in ServingAmounts) {
                options.Add(new TMP_Dropdown.OptionData(servingAmount.ToString()));
            }
            _dropdown.AddOptions(options);
            SelectedServingAmount = default;

            _dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
        }

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(Dropdown_OnValueChanged);
        }

        private void Dropdown_OnValueChanged(int value)
        {
            var oldIsValid = IsValid;
            SelectedServingAmount = value > 0 ? ServingAmounts[value - 1] : default;
            if (IsValid != oldIsValid)
            {
                ValidityChanged?.Invoke();
            }
        }
    }
}
