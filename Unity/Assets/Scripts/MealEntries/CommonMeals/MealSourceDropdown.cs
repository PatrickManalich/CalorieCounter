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

        public MealSource SelectedMealSource { get; private set; }
        
        private TMP_Dropdown _dropdown;
        private List<MealSource> _mealSources;
        private List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        public void RefreshSelectedMealSource(int index) {
            if (_mealSources.Exists(x => x.Name == _optionDataList[index].text)) {
                SelectedMealSource = _mealSources.Find(x => x.Name == _optionDataList[index].text);
                if (_servingAmountDropdown.SelectedServingAmount != 0)
                    FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            } else {
                SelectedMealSource = default;
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }

        public void ResetDropdown() {
            _dropdown.value = 0;
            SelectedMealSource = default;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _mealSources = _mealSourceAdapter.GetMealSources(_mealSourceType);
            _dropdown.ClearOptions();
            _optionDataList.Add(new TMP_Dropdown.OptionData(""));
            foreach (var meal in _mealSources) {
                _optionDataList.Add(new TMP_Dropdown.OptionData(meal.Name));
            }
            _dropdown.AddOptions(_optionDataList);
            SelectedMealSource = default;
        }        
    }
}
