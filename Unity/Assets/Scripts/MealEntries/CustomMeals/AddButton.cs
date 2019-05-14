using TMPro;
using UnityEngine;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class AddButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private TextMeshProUGUI _errorText = default;

        public void TryAddingInputFields() {
            if (_scrollView.HasInputFields()) {
                _errorText.text = "Input Fields Already Active";
                _errorText.gameObject.SetActive(true);
                return;
            }

            _errorText.gameObject.SetActive(false);
            _scrollView.AddInputFields();
        }
    }
}