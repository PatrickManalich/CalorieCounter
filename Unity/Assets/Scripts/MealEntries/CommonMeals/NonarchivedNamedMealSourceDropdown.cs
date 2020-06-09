using UnityEngine;
using TMPro;
using System.Collections.Generic;
using CalorieCounter.MealSources;
using System.Linq;
using System;

namespace CalorieCounter.MealEntries {

    [RequireComponent(typeof(TMP_Dropdown))]
    public class NonarchivedNamedMealSourceDropdown : MonoBehaviour {

        public event Action ValidityChanged;

        public bool IsValid => SelectedNamedMealSource != null;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        public NamedMealSource SelectedNamedMealSource { get; private set; }
        
        private TMP_Dropdown _dropdown;
        private List<NamedMealSource> _nonarchivedNamedMealSources;

        public void Reset() {
            _dropdown.value = 0;
            SelectedNamedMealSource = null;
        }

        public void SetInteractable(bool value)
        {
            _dropdown.interactable = value;
        }

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _nonarchivedNamedMealSources = MealSourcesAdapter.GetNamedMealSources(_mealSourceType).Where(n => !n.mealSource.archived).ToList();
            _dropdown.ClearOptions();
            var options = new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData("") };
            foreach (var nonarchivedNamedMealSource in _nonarchivedNamedMealSources)
            {
                options.Add(new TMP_Dropdown.OptionData($"{nonarchivedNamedMealSource.name} (per {nonarchivedNamedMealSource.mealSource.servingSize.ToLower()})"));
            }
            _dropdown.AddOptions(options);
            Reset();
        }

        private void Start() {
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
