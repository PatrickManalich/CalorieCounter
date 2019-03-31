using TMPro;
using UnityEngine;

namespace CalorieCounter {

    public class SubmitButton : MonoBehaviour {
        
        [SerializeField]
        private MealDropdown _mealDropdown;

        [SerializeField]
        private GameObject _error;

        public void OnSubmit() {
            if(_mealDropdown.SelectedMeal == Meal.NullMeal) {
                _error.GetComponent<TextMeshProUGUI>().text = "Select Valid Meal";
                _error.SetActive(true);
                return;
            }

            _error.SetActive(false);
        }

        private void Start() {
            _error.SetActive(false);
        }
    }
}
