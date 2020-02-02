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
        private CustomMealsScrollView _customMealsScrollView = default;

        [SerializeField]
        private CustomMealProportionInputFields _customMealProportionInputFields = default;

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
            _customMealProportionInputFields.Show();
        }

        private void SubmitButton_OnClick()
        {
            if (_customMealProportionInputFields.Shown)
            {
                var customMealProportion = _customMealProportionInputFields.CustomMealProportion;
                _customMealProportionInputFields.Hide();
                _customMealsScrollView.AddMealProportion(customMealProportion);
            }
        }

        private void CancelButton_OnClick()
        {
            if (_customMealProportionInputFields.Shown)
            {
                _customMealProportionInputFields.Hide();
            }
        }

    }
}
