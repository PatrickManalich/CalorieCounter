using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries.CustomMeals {

    public class AddButton : MonoBehaviour {

        [SerializeField]
        private ScrollView _scrollView = default;

        [SerializeField]
        private Button _submitButton = default;

        public void TryAddingInputFields() {
            _scrollView.AddInputFields(_submitButton.GetComponent<Selectable>());
        }
    }
}