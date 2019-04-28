using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries {

    public class SubmitButton : MonoBehaviour {

        [SerializeField]
        private ServingAmountDropdown _servingAmountDropdown = default;

        [SerializeField]
        private MealSourceDropdown _mealSourceDropdown = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private Button _exportButton = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void TryAddingMealProportion() {
            if (_servingAmountDropdown.SelectedServingAmount <= 0) {
                _errorText.text = "Select Valid Amount";
                _errorText.gameObject.SetActive(true);
                return;
            } else if (_mealSourceDropdown.SelectedMealSource == default) {
                _errorText.text = "Select Valid Meal";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            MealProportion mealProportion = new MealProportion(_servingAmountDropdown.SelectedServingAmount, _mealSourceDropdown.SelectedMealSource);
            _scrollView.AddMealProportion(mealProportion);
            _exportButton.gameObject.SetActive(true); 
            _mealSourceDropdown.ResetDropdown();
            _servingAmountDropdown.ResetDropdown();
        }

        private void Start() {
            _errorText.gameObject.SetActive(false);
        }
    }
}
