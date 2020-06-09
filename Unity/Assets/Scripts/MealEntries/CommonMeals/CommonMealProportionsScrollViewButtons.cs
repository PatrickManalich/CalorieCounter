using CalorieCounter.Utilities;
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

        private void Start()
        {
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _nonarchivedNamedMealSourceDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
        }

        private void OnDestroy()
        {
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _nonarchivedNamedMealSourceDropdown.ValidityChanged -= Dropdown_OnValidityChanged;
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
        }

        private void Dropdown_OnValidityChanged()
        {
            if (_servingAmountDropdown.IsValid && _nonarchivedNamedMealSourceDropdown.IsValid)
            {
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            }
            else
            {
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }

        private void SubmitButton_OnClick()
        {
            var mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, 
                _nonarchivedNamedMealSourceDropdown.SelectedNamedMealSource.mealSource);
            _mealProportionsScrollView.AddMealProportion(mealProportion);

            ResetDropdowns();
        }

        private void Date_OnCurrentDateTimeChanged()
        {
            ResetDropdowns();
        }

        private void ResetDropdowns()
        {
            _servingAmountDropdown.Reset();
            _nonarchivedNamedMealSourceDropdown.Reset();
        }
    }
}
