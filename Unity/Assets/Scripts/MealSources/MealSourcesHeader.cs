using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealSources
{
    public class MealSourcesHeader : MonoBehaviour
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
            _addButton.onClick.AddListener(() => OnAddButtonClicked());
            _submitButton.onClick.AddListener(() => OnSubmitButtonClicked());
            _cancelButton.onClick.AddListener(() => OnCancelButtonClicked());
        }

        private void OnDestroy()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _submitButton.onClick.RemoveAllListeners();
            _addButton.onClick.RemoveAllListeners();
        }

        private void OnAddButtonClicked()
        {
            _mealSourceInputFields.Show();
        }

        private void OnSubmitButtonClicked()
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

        private void OnCancelButtonClicked()
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
