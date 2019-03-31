using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace CalorieCounter {

    public class AmountDropdown : MonoBehaviour {
        
        public float SelectedAmount { get; private set; }
        
        private static readonly float[] Amounts = new float[] { 0.25f, 0.33f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5 };

        private TMP_Dropdown _amountDropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void DropdownIndexChanged(int index) {
            float parsedFloat;
            if (float.TryParse(_optionDataList[index].text, out parsedFloat)) {
                SelectedAmount = parsedFloat;
            } else {
                SelectedAmount = 0;
            }
        }

        public void ResetDropdown() {
            _amountDropdown.value = 0;
            SelectedAmount = 0;
        }

        private void Start() {
            _amountDropdown = GetComponent<TMP_Dropdown>();

            _amountDropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var amount in Amounts) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(amount.ToString()));
            }
            _amountDropdown.AddOptions(_optionDataList);
            SelectedAmount = 0;
        }

    }
}
