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
            _mealSourceInputFields.ShowInputFields();
        }

        private void OnSubmitButtonClicked()
        {
            if (_mealSourceInputFields.Shown)
            {
                _mealSourcesScrollView.AddNamedMealSource(_mealSourceInputFields.NamedMealSource);
                _mealSourceInputFields.HideInputFields();
            }
            if(_mealSourceRenameField.Shown)
            {
                _mealSourcesScrollView.RenameNamedMealSource(_mealSourceRenameField.OldNamedMealSource, _mealSourceRenameField.NewNamedMealSource);
                _mealSourceRenameField.HideRenameField();
            }
        }

        private void OnCancelButtonClicked()
        {
            if (_mealSourceInputFields.Shown)
            {
                _mealSourceInputFields.HideInputFields();
            }
            if (_mealSourceRenameField.Shown)
            {
                _mealSourceRenameField.HideRenameField();
            }
        }

    }
}
