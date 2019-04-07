using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace CalorieCounter.Meals {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class ServingDropdown : MonoBehaviour {
        
        public float SelectedServing { get; private set; }
        
        private static readonly float[] Servings = new float[] { 0.25f, 0.33f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5 };

        private TMP_Dropdown _servingDropdown;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedServing(int index) {
            float parsedFloat;
            if (float.TryParse(_optionDataList[index].text, out parsedFloat)) {
                SelectedServing = parsedFloat;
            } else {
                SelectedServing = 0;
            }
        }

        public void ResetDropdown() {
            _servingDropdown.value = 0;
            SelectedServing = 0;
        }

        private void Start() {
            _servingDropdown = GetComponent<TMP_Dropdown>();

            _servingDropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var amount in Servings) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(amount.ToString()));
            }
            _servingDropdown.AddOptions(_optionDataList);
            SelectedServing = 0;
        }

    }
}
