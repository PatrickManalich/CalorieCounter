using System.Collections;
using System.Collections.Generic;
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
            _mealSourcesScrollView.AddNamedMealSource(_mealSourceInputFields.GetNamedMealSourceFromInputFields());
            _mealSourceInputFields.HideInputFields();
        }

        private void OnCancelButtonClicked()
        {
            _mealSourceInputFields.HideInputFields();
        }

    }
}
