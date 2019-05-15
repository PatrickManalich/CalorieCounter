using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class CancelButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private Button _submitButton = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void CancelAddingMealProportion() {
            _errorText.gameObject.SetActive(false);
            _scrollView.DeleteInputFields();
            gameObject.SetActive(false);
            _submitButton.gameObject.SetActive(false);
        }
    }
}
