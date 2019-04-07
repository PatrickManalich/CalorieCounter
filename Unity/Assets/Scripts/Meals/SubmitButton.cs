using TMPro;
using UnityEngine;

namespace CalorieCounter.Meals {

    public class SubmitButton : MonoBehaviour {
        
        [SerializeField]
        private MealDropdown _mealDropdown;

        [SerializeField]
        private ServingDropdown _amountDropdown;

        [SerializeField]
        private ScrollView _scrollView;

        [SerializeField]
        private TextMeshProUGUI _errorText;

        public void SubmitMeal() {
            if (_amountDropdown.SelectedServing <= 0) {
                _errorText.text = "Select Valid Amount";
                _errorText.gameObject.SetActive(true);
                return;
            } else if (_mealDropdown.SelectedMeal == default) {
                _errorText.text = "Select Valid Meal";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _scrollView.AddMeal(_amountDropdown.SelectedServing, _mealDropdown.SelectedMeal);
            _mealDropdown.ResetDropdown();
            _amountDropdown.ResetDropdown();
        }

        private void Start() {
            _errorText.gameObject.SetActive(false);
        }
    }
}
