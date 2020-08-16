using CalorieCounter.Managers;
using CalorieCounter.MealSources;
using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter.MealEntries
{
    [RequireComponent(typeof(TMP_InputField))]
    public class NamedMealSourceSearchInputField : MonoBehaviour
    {
        public event EventHandler ValidityChanged;

        public bool IsValid => _nonarchivedNamedMealSources.Exists(n => n.Name.ToLower() == _inputField.text.ToLower());

        [SerializeField]
        private TMP_Dropdown _dropdown = default;

        [SerializeField]
        private MealSourceType _mealSourceType = default;

        public NamedMealSource SelectedNamedMealSource { get; private set; }

        private const int MaxSearchResult = 5;

        private TMP_InputField _inputField;
        private List<NamedMealSource> _nonarchivedNamedMealSources = new List<NamedMealSource>();
        private List<NamedMealSource> _searchResults = new List<NamedMealSource>();
        private bool _oldIsValid;

        public void Clear()
        {
            _inputField.text = string.Empty;
            _dropdown.Hide();
            SelectedNamedMealSource = null;
        }

        public void SetInteractable(bool value)
        {
            _inputField.interactable = value;
            _dropdown.Hide();
        }

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValidateInput = InputUtilities.ValidateNonDecimalInput;
        }

        private void Start()
        {
            _nonarchivedNamedMealSources = MealSourcesAdapter.GetNamedMealSources(_mealSourceType).Where(n => !n.MealSource.Archived).ToList();

            _inputField.onValueChanged.AddListener(InputField_OnValueChanged);
            _dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
            _dropdown.onValueChanged.RemoveListener(Dropdown_OnValueChanged);
            _inputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
        }

        private void InputField_OnValueChanged(string value)
        {
            _searchResults.Clear();
            if (value != string.Empty)
            {
                var firstCharMatches = _nonarchivedNamedMealSources.Where(n => n.Name.ToLower()[0] == value.ToLower()[0]);
                _searchResults = firstCharMatches.OrderBy(n => MathUtilities.GetLevenshteinDistance(n.Name.ToLower(), value.ToLower())).ToList();
                if (_searchResults.Count > MaxSearchResult)
                {
                    _searchResults.RemoveRange(MaxSearchResult, _searchResults.Count - MaxSearchResult);
                }
            }

            var options = new List<TMP_Dropdown.OptionData> { new TMP_Dropdown.OptionData(string.Empty) };
            foreach (var searchResult in _searchResults)
            {
                options.Add(new TMP_Dropdown.OptionData($"{searchResult.Name} (per {searchResult.MealSource.ServingSize.ToLower()})"));
            }
            _dropdown.ClearOptions();
            _dropdown.Hide();
            _dropdown.AddOptions(options);
            _dropdown.Show();
            _inputField.Select();

            if (IsValid)
            {
                SelectedNamedMealSource = _nonarchivedNamedMealSources.First(n => n.Name.ToLower() == _inputField.text.ToLower());
            }

            if (IsValid != _oldIsValid)
            {
                ValidityChanged?.Invoke(this, EventArgs.Empty);
                _oldIsValid = IsValid;
            }
        }

        private void Dropdown_OnValueChanged(int value)
        {
            if (value == 0)
            {
                _inputField.text = string.Empty;
            }
            else
            {
                // Account for the empty option
                _inputField.text = _searchResults[value - 1].Name;
            }
        }

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (e.InputKeyCode == InputKeyCode.SelectNext && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if(_dropdown.options.Count > 1)
                {
                    _inputField.text = _searchResults[0].Name;
                    _inputField.MoveToEndOfLine(false, false);
                    _dropdown.Hide();
                }
            }
        }
    }
}