using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries {

    public class SubmitButton : MonoBehaviour {
        
        [SerializeField]
        private MealDropdown _mealDropdown = default;

        [SerializeField]
        private ServingDropdown _amountDropdown = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private Button _exportButton = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void TryAddingMealProportion() {
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
            _scrollView.AddMealProportion(_amountDropdown.SelectedServing, _mealDropdown.SelectedMeal);
            _exportButton.gameObject.SetActive(true); 
            _mealDropdown.ResetDropdown();
            _amountDropdown.ResetDropdown();
        }

        private void Start() {
            _errorText.gameObject.SetActive(false);
        }
    }
}
