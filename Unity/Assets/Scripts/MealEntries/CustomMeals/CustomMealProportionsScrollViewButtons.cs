using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{
    public class CustomMealProportionsScrollViewButtons : MonoBehaviour
    {
        [SerializeField]
        private Button _addButton = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private Button _cancelButton = default;

        [SerializeField]
        private MealProportionsScrollView _mealProportionsScrollView = default;

        [SerializeField]
        private CustomMealProportionInputFields _customMealProportionInputFields = default;

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        private void Start()
        {
            _addButton.onClick.AddListener(AddButton_OnClick);
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_CurrentDayTypeChanged;
            _customMealProportionInputFields.ValidityChanged += CustomMealProportionInputFields_OnValidityChanged;

            _addButton.interactable = _dayTypeDropdown.IsCurrentDayTypeRestOrTraining;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void OnDestroy()
        {
            _customMealProportionInputFields.ValidityChanged -= CustomMealProportionInputFields_OnValidityChanged;
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_CurrentDayTypeChanged;
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _addButton.onClick.RemoveListener(AddButton_OnClick);
        }

        private void AddButton_OnClick()
        {
            _customMealProportionInputFields.Show();
            _addButton.interactable = false;
            _submitButton.interactable = false;
            _cancelButton.interactable = true;
        }

        private void SubmitButton_OnClick()
        {
            if (_customMealProportionInputFields.IsShown)
            {
                var customMealProportion = _customMealProportionInputFields.CustomMealProportion;
                _customMealProportionInputFields.Hide();
                _mealProportionsScrollView.AddMealProportion(customMealProportion);
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void CancelButton_OnClick()
        {
            if (_customMealProportionInputFields.IsShown)
            {
                _customMealProportionInputFields.Hide();
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void Date_OnCurrentDateTimeChanged()
        {
            _customMealProportionInputFields.Hide();
        }

        private void DayTypeDropdown_CurrentDayTypeChanged()
        {
            var value = _dayTypeDropdown.IsCurrentDayTypeRestOrTraining;
            _addButton.interactable = value;
        }

        private void CustomMealProportionInputFields_OnValidityChanged()
        {
            _submitButton.interactable = _customMealProportionInputFields.IsValid;
        }
    }
}
