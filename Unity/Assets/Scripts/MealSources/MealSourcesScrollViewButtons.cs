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
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
            _submitButton.onClick.RemoveListener(SubmitButton_OnClick);
            _addButton.onClick.RemoveListener(AddButton_OnClick);
        }

        private void AddButton_OnClick()
        {
            _mealSourceInputFields.Show();
        }

        private void SubmitButton_OnClick()
        {
            if (_mealSourceInputFields.Shown)
            {
                var namedMealSource = _mealSourceInputFields.NamedMealSource;
                _mealSourceInputFields.Hide();
                _mealSourcesScrollView.AddNamedMealSource(namedMealSource);
            }
            if(_mealSourceRenameField.Shown)
            {
                var oldNamedMealSource = _mealSourceRenameField.OldNamedMealSource;
                var newNamedMealSource = _mealSourceRenameField.NewNamedMealSource;
                _mealSourceRenameField.Hide();
                _mealSourcesScrollView.RenameNamedMealSource(oldNamedMealSource, newNamedMealSource);
            }
        }

        private void CancelButton_OnClick()
        {
            if (_mealSourceInputFields.Shown)
            {
                _mealSourceInputFields.Hide();
            }
            if (_mealSourceRenameField.Shown)
            {
                _mealSourceRenameField.Hide();
            }
        }

    }
}
