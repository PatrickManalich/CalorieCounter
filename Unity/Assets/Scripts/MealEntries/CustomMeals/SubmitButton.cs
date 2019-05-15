using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class SubmitButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private Button _exportButton = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void TryAddingMealProportionFromInputFields() {
            if (!_scrollView.AllInputFieldsFilled()) {
                _errorText.text = "Fill All Input Fields";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            MealProportion customMealProportion = _scrollView.GetCustomMealProportionFromInputFields();
            _scrollView.AddMealProportion(customMealProportion);
            _exportButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
