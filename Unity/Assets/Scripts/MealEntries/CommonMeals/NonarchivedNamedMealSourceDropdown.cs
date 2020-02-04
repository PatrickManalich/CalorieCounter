using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.MealSources;
using System.Linq;
using System;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class NonarchivedNamedMealSourceDropdown : MonoBehaviour {

        public Action ValidityChanged;

        public bool IsValid => SelectedNamedMealSource != default;

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        public NamedMealSource SelectedNamedMealSource { get; private set; }
        
        private TMP_Dropdown _dropdown;
        private List<NamedMealSource> _nonarchivedNamedMealSources;

        public void Reset() {
            _dropdown.value = 0;
            SelectedNamedMealSource = default;
        }

        private void Start() {
            _dropdown = GetComponent<TMP_Dropdown>();
            _nonarchivedNamedMealSources = _mealSourcesAdapter.GetNamedMealSources(_mealSourceType).Where(n => !n.mealSource.archived).ToList();
            _dropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData("") };
            foreach (var nonarchivedNamedMealSource in _nonarchivedNamedMealSources) {
                options.Add(new TMP_Dropdown.OptionData($"{nonarchivedNamedMealSource.name} (per {nonarchivedNamedMealSource.mealSource.servingSize.ToLower()})"));
            }
            _dropdown.AddOptions(options);
            SelectedNamedMealSource = default;
            _dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
        }

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(Dropdown_OnValueChanged);
        }

        private void Dropdown_OnValueChanged(int value)
        {
            var oldIsValid = IsValid;
            SelectedNamedMealSource = value > 0 ? _nonarchivedNamedMealSources[value - 1] : default;
            if(IsValid != oldIsValid)
            {
                ValidityChanged?.Invoke();
            }
        }
    }
}
