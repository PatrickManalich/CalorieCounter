using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.Utilities;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class ServingAmountDropdown : MonoBehaviour {

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _namedMealSourceDropdown = default;

        public float SelectedServingAmount { get; private set; }
        
        private static readonly float[] ServingAmounts = new float[] { 0.1f, 0.25f, 0.33f, 0.5f, 0.75f, 1, 1.5f, 2, 3, 4, 5, 6 };

        private TMP_Dropdown _dropdown;

        public void RefreshSelectedServingAmount(int index) {
            if (index > 0)
            {
                SelectedServingAmount = ServingAmounts[index - 1];
                if (_namedMealSourceDropdown.SelectedNamedMealSource != default)
                    FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            }
            else
            {
                SelectedServingAmount = 0;
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedServingAmount = 0;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();

            _dropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData("") };
            foreach (var servingAmount in ServingAmounts) {
                options.Add(new TMP_Dropdown.OptionData(servingAmount.ToString()));
            }
            _dropdown.AddOptions(options);
            SelectedServingAmount = 0;
        }

    }
}
