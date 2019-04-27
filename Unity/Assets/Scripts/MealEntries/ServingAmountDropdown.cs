﻿using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class ServingAmountDropdown : MonoBehaviour {
        
        public float SelectedServingAmount { get; private set; }
        
        private static readonly float[] ServingAmounts = new float[] { 0.25f, 0.33f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5 };

        private TMP_Dropdown _dropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedServingAmount(int index) {
            float parsedFloat;
            if (float.TryParse(_optionDataList[index].text, out parsedFloat)) {
                SelectedServingAmount = parsedFloat;
            } else {
                SelectedServingAmount = 0;
            }
        }

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedServingAmount = 0;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var amount in ServingAmounts) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(amount.ToString()));
            }
            _dropdown.AddOptions(_optionDataList);
            SelectedServingAmount = 0;
        }

    }
}
