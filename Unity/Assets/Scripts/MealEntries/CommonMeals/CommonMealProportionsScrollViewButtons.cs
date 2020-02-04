using CalorieCounter.Utilities;
using UnityEngine;

namespace CalorieCounter.MealEntries
{
    public class CommonMealProportionsScrollViewButtons : MonoBehaviour
    {
        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private NonarchivedNamedMealSourceDropdown _nonarchivedNamedMealSourceDropdown = default;

        private void Start()
        {
            _servingAmountDropdown.ValidityChanged += Dropdown_OnValidityChanged;
            _nonarchivedNamedMealSourceDropdown.ValidityChanged += Dropdown_OnValidityChanged;
        }

        private void OnDestroy()
        {
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
    }
}
