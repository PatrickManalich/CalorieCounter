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
        private Button _submitButton = default;

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _nonarchivedNamedMealSourceDropdown = default;

        [SerializeField]
        private MealProportionsScrollView _mealProportionsScrollView = default;

        private void Start()
        {
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _nonarchivedNamedMealSourceDropdown.ValidityChanged += Dropdown_OnValidityChanged;
        }

        private void OnDestroy()
        {
            _nonarchivedNamedMealSourceDropdown.ValidityChanged -= Dropdown_OnValidityChanged;
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
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

            _servingAmountDropdown.Reset();
            _nonarchivedNamedMealSourceDropdown.Reset();
        }
    }
}
