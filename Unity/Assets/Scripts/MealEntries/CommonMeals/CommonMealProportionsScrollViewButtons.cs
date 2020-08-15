using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{
    public class CommonMealProportionsScrollViewButtons : MonoBehaviour
    {
        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _nonarchivedNamedMealSourceDropdown = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private MealProportionsScrollView _mealProportionsScrollView = default;

        [SerializeField]
        private Date _date = default;

        [SerializeField]
        private DayTypeDropdown _dayTypeDropdown = default;

        private void Start()
        {
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _nonarchivedNamedMealSourceDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
            _dayTypeDropdown.CurrentDayTypeChanged += DayTypeDropdown_CurrentDayTypeChanged;

            _servingAmountDropdown.SetInteractable(_dayTypeDropdown.IsCurrentDayTypeRestOrTraining);
            _nonarchivedNamedMealSourceDropdown.SetInteractable(_dayTypeDropdown.IsCurrentDayTypeRestOrTraining);
            _submitButton.interactable = false;
        }

        private void OnDestroy()
        {
            _dayTypeDropdown.CurrentDayTypeChanged -= DayTypeDropdown_CurrentDayTypeChanged;
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _nonarchivedNamedMealSourceDropdown.ValidityChanged -= Dropdown_OnValidityChanged;
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
        }

        private void Dropdown_OnValidityChanged(object sender, System.EventArgs e)
        {
            _submitButton.interactable = _servingAmountDropdown.IsValid && _nonarchivedNamedMealSourceDropdown.IsValid;
        }

        private void SubmitButton_OnClick()
        {
            var mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, 
                _nonarchivedNamedMealSourceDropdown.SelectedNamedMealSource.MealSource);
            _mealProportionsScrollView.AddMealProportion(mealProportion);

            ClearAll();
            _submitButton.interactable = false;
        }

        private void Date_OnCurrentDateTimeChanged(object sender, System.EventArgs e)
        {
            ClearAll();
        }

        private void DayTypeDropdown_CurrentDayTypeChanged(object sender, System.EventArgs e)
        {
            var value = _dayTypeDropdown.IsCurrentDayTypeRestOrTraining;
            _servingAmountDropdown.SetInteractable(value);
            _nonarchivedNamedMealSourceDropdown.SetInteractable(value);
            ClearAll();
        }

        private void ClearAll()
        {
            _servingAmountDropdown.Clear();
            _nonarchivedNamedMealSourceDropdown.Clear();
        }
    }
}
