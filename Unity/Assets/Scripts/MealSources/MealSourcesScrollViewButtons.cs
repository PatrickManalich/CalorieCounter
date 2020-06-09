using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealSources
{
    public class MealSourcesScrollViewButtons : MonoBehaviour
    {
        [SerializeField]
        private Button _addButton = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private Button _cancelButton = default;

        [SerializeField]
        private MealSourcesScrollView _mealSourcesScrollView = default;

        [SerializeField]
        private MealSourceInputFields _mealSourceInputFields = default;

        [SerializeField]
        private MealSourceRenameField _mealSourceRenameField = default;

        private void Start()
        {
            _addButton.onClick.AddListener(AddButton_OnClick);
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
            _mealSourceInputFields.ValidityChanged += MealSourceInputFields_OnValidityChanged;

            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void OnDestroy()
        {
            _mealSourceInputFields.ValidityChanged -= MealSourceInputFields_OnValidityChanged;
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _addButton.onClick.RemoveListener(AddButton_OnClick);
        }

        private void AddButton_OnClick()
        {
            _mealSourceInputFields.Show();
            _addButton.interactable = false;
            _submitButton.interactable = false;
            _cancelButton.interactable = true;
        }

        private void SubmitButton_OnClick()
        {
            if (_mealSourceInputFields.IsShown)
            {
                var namedMealSource = _mealSourceInputFields.NamedMealSource;
                _mealSourceInputFields.Hide();
                _mealSourcesScrollView.AddNamedMealSource(namedMealSource);
            }
            if(_mealSourceRenameField.IsShown)
            {
                var oldNamedMealSource = _mealSourceRenameField.OldNamedMealSource;
                var newNamedMealSource = _mealSourceRenameField.NewNamedMealSource;
                _mealSourceRenameField.Hide();
                _mealSourcesScrollView.RenameNamedMealSource(oldNamedMealSource, newNamedMealSource);
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void CancelButton_OnClick()
        {
            if (_mealSourceInputFields.IsShown)
            {
                _mealSourceInputFields.Hide();
            }
            if (_mealSourceRenameField.IsShown)
            {
                _mealSourceRenameField.Hide();
            }
            _addButton.interactable = true;
            _submitButton.interactable = false;
            _cancelButton.interactable = false;
        }

        private void MealSourceInputFields_OnValidityChanged()
        {
            _submitButton.interactable = _mealSourceInputFields.IsValid;
        }
    }
}
