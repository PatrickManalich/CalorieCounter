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
        private MealProportionsScrollView _mealProportionsScrollView = default;

        [SerializeField]
        private CustomMealProportionInputFields _customMealProportionInputFields = default;

        [SerializeField]
        private Date _date = default;

        private void Start()
        {
            _addButton.onClick.AddListener(AddButton_OnClick);
            _submitButton.onClick.AddListener(SubmitButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
            _date.CurrentDateTimeChanged += Date_OnCurrentDateTimeChanged;
        }

        private void OnDestroy()
        {
            _date.CurrentDateTimeChanged -= Date_OnCurrentDateTimeChanged;
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
            if (_customMealProportionInputFields.IsShown)
            {
                var customMealProportion = _customMealProportionInputFields.CustomMealProportion;
                _customMealProportionInputFields.Hide();
                _mealProportionsScrollView.AddMealProportion(customMealProportion);
            }
        }

        private void CancelButton_OnClick()
        {
            if (_customMealProportionInputFields.IsShown)
            {
                _customMealProportionInputFields.Hide();
            }
        }

        private void Date_OnCurrentDateTimeChanged()
        {
            _customMealProportionInputFields.Hide();
        }
    }
}
