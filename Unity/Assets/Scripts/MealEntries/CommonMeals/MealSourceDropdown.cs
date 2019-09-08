using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class MealSourceDropdown : MonoBehaviour {

        [SerializeField]
        private MealSourcesAdapter _mealSourceAdapter = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        public NamedMealSource SelectedNamedMealSource { get; private set; }
        
        private TMP_Dropdown _dropdown;
        private List<NamedMealSource> _namedMealSources;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedMealSource(int index) {
            if (index > 0) {
                SelectedNamedMealSource = _namedMealSources[index-1];
                if (_servingAmountDropdown.SelectedServingAmount != 0)
                    FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            } else {
                SelectedNamedMealSource = default;
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedNamedMealSource = default;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _namedMealSources = _mealSourceAdapter.GetNamedMealSources(_mealSourceType);
            _dropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var namedMealSource in _namedMealSources) {
                _optionDataList.Add(new TMP_Dropdown.OptionData($"{namedMealSource.name} (per {namedMealSource.mealSource.servingSize.ToLower()})"));
            }
            _dropdown.AddOptions(_optionDataList);
            SelectedNamedMealSource = default;
        }
    }
}
