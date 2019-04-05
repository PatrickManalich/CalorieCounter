using TMPro;
using UnityEngine;

namespace CalorieCounter {

    public class SubmitButton : MonoBehaviour {
        
        [SerializeField]
        private MealDropdown _mealDropdown;

        [SerializeField]
        private ServingDropdown _amountDropdown;

        [SerializeField]
        private MealScrollView _mealScrollView;

        [SerializeField]
        private GameObject _error;

        public void SubmitMeal() {
            if (_amountDropdown.SelectedServing <= 0) {
                _error.GetComponent<TextMeshProUGUI>().text = "Select Valid Amount";
                _error.SetActive(true);
                return;
            } else if (_mealDropdown.SelectedMeal == default) {
                _error.GetComponent<TextMeshProUGUI>().text = "Select Valid Meal";
                _error.SetActive(true);
                return;
            }

            _error.SetActive(false);
            _mealScrollView.AddMeal(_amountDropdown.SelectedServing, _mealDropdown.SelectedMeal);
            _mealDropdown.ResetDropdown();
            _amountDropdown.ResetDropdown();
        }

        private void Start() {
            _error.SetActive(false);
        }
    }
}
